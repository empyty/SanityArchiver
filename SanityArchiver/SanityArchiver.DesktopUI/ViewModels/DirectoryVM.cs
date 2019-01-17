using SanityArchiver.Application.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace SanityArchiver.DesktopUI.ViewModels
{
    class DirectoryVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        DirectoryList directoryList { get; } = new DirectoryList();
        
        public ObservableCollection<TreeViewItem> Directories
        {
            get
            {
                directoryList.LoadDrives();
                return directoryList.MyDirectories;
            }
        }

        public ObservableCollection<FileSystemInfo> Files
        {
            get
            {
                return directoryList.MyFiles;
            }
        }

        public string FolderName
        {
            get
            {
                return directoryList.FolderName;
            }
        }

        public DirectoryVM()
        {
            directoryList.PropertyChanged += DirectoryList_PropertyChanged;
        }

        private void DirectoryList_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("FolderName");
        }
    }
}
