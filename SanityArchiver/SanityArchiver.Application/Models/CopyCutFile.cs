using System;
using System.IO;

namespace SanityArchiver.Application.Models
{
    class CopyCutFile
    {
        string myFilePath;
        string myFileName;

        public void CopyOrMoveFile(FileInfo file)
        {
            myFilePath = file.DirectoryName;
            myFileName = file.Name;
        }

        public void PasteFile(DirectoryInfo newDirectory, string typeOfAction)
        {
            var action1 = CreatePath(myFilePath);
            var action2 = CreatePath(newDirectory.Name);
            switch (typeOfAction)
            {
                case "copy":
                    File.Copy(action1, action2);
                    break;
                case "move":
                    File.Move(action1, action2);
                    break;
                default:
                    break;
            }
            try
            {
                File.Copy(Path.Combine(myFilePath + myFileName), Path.Combine(newDirectory.Name + myFileName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private string CreatePath(string directory)
        {
            var newPath = Path.Combine(directory + myFileName);
            return newPath;
        }
    }
}
