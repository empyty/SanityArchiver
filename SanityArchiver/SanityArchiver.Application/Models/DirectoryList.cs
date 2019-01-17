using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SanityArchiver.Application.Models
{
    public class DirectoryList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TreeViewItem> MyDirectories { get; } = new ObservableCollection<TreeViewItem>();
        public ObservableCollection<FileSystemInfo> MyFiles { get; } = new ObservableCollection<FileSystemInfo>();

        private string folderName = "Main Folder";
        public string FolderName
        {
            get
            {
                return folderName;
            }
            set
            {
                folderName = value;
                NotifyPropertyChanged();
            }
        }

        public void LoadDrives()
        {
            foreach (var drive in new BasicDirectory().DrivesDirectory)
            {
                MyDirectories.Add(GetItem(drive));
            }
        }

        private TreeViewItem GetItem(DriveInfo drive)
        {
            var item = new TreeViewItem
            {
                Header = drive.Name,
                Tag = drive
            };
            AddDummy(item);
            item.Expanded += Item_Expanded;
            return item;
        }

        private TreeViewItem GetItem(FileSystemInfo item)
        {
            var newItem = new TreeViewItem
            {
                Header = item.Name,
                Tag = item
            };
            AddDummy(newItem);
            newItem.Expanded += Item_Expanded;
            newItem.Selected += new RoutedEventHandler(Item_Focused);
            return newItem;
        }

        private void AddDummy(TreeViewItem item)
        {
            item.Items.Add(new DummyTreeViewItem());
        }
        private bool HasDummy(TreeViewItem item)
        {
            return item.HasItems && (item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is DummyTreeViewItem).Count > 0);
        }
        private void RemoveDummy(TreeViewItem item)
        {
            var dummies = item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is DummyTreeViewItem);
            foreach (var dummy in dummies)
            {
                item.Items.Remove(dummy);
            }
        }

        private void ExploreDirectories(TreeViewItem item)
        {
            var directoryInfo = (DirectoryInfo)null;
            if (item.Tag is DriveInfo)
            {
                directoryInfo = ((DriveInfo)item.Tag).RootDirectory;
            }
            else if (item.Tag is DirectoryInfo)
            {
                directoryInfo = (DirectoryInfo)item.Tag;
            }
            if (ReferenceEquals(directoryInfo, null)) return;
            AddDataToCollection(directoryInfo.GetDirectories(), item, "TreeView");
        }

        private void ExploreFiles(TreeViewItem item)
        {
            var directoryInfo = (DirectoryInfo)null;
            if (item.Tag is DirectoryInfo)
            {
                directoryInfo = (DirectoryInfo)item.Tag;
            }
            if (ReferenceEquals(directoryInfo, null)) return;
            MyFiles.Clear();
            FolderName = directoryInfo.Name;
            AddDataToCollection(directoryInfo.GetFiles(), item, "");
            AddDataToCollection(directoryInfo.GetDirectories(), item, "");
        }

        private void AddDataToCollection(FileSystemInfo[] collection, TreeViewItem item, string target)
        {
            foreach (var data in collection)
            {
                var isHidden = (data.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                var isSystem = (data.Attributes & FileAttributes.System) == FileAttributes.System;
                if (!isHidden && !isSystem)
                {
                    if (target.Equals("TreeView"))
                    {
                        AddFilesToTreeViewItem(item, data);
                    }
                    else
                    {
                        AddFilesToMyFilesList(data);
                    }
                }
            }
        }
        private void AddFilesToMyFilesList(FileSystemInfo item)
        {
            MyFiles.Add(item);
        }
        private void AddFilesToTreeViewItem(TreeViewItem baseItem, FileSystemInfo addedItem)
        {
            baseItem.Items.Add(GetItem(addedItem));
        }

        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (HasDummy(item))
            {
                item.Cursor = Cursors.Wait;
                RemoveDummy(item);
                ExploreDirectories(item);
                item.Cursor = Cursors.Arrow;
            }
        }

        private void Item_Focused(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)e.Source;
            ExploreFiles(item);
        }
    }
}
