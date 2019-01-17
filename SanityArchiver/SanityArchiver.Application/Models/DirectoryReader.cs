using System.IO;

namespace SanityArchiver.Application.Models
{
    class DirectoryReader
    {
        public DirectoryInfo[] ReadDirectory(DirectoryInfo directoryToRead)
        {
            var nextDirectories = directoryToRead.GetDirectories();
            return nextDirectories;
        }

        public FileInfo[] ReadFilesInDirectory(DirectoryInfo directoryToRead)
        {
            var filesInDirectory = directoryToRead.GetFiles();
            return filesInDirectory;
        }
    }
}
