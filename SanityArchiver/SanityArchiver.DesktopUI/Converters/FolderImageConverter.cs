using System;
using System.Globalization;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;

namespace SanityArchiver.DesktopUI.Converters
{
    class FolderImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var driveIcon = "driveIcon";
            var folderIcon = "folderIcon";
            var fileIcon = "fileIcon";
            var unknownFileIcon = "unknownFileIcon";
            var textFileIcon = "textFileIcon";

            var systemFile = value as TreeViewItem;
            if (systemFile.Tag is DriveInfo)
            {
                return driveIcon;
            }
            else if (systemFile.Tag is DirectoryInfo)
            {
                return folderIcon;
            }
            else if (systemFile.Tag is FileInfo)
            {
                return fileIcon;
            }
            return unknownFileIcon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
