using System.IO;

namespace SanityArchiver.Application.Models
{
    public class BasicDirectory
    {
        // Get basic infor about drives on PC
        public DriveInfo[] DrivesDirectory { get; } = DriveInfo.GetDrives();
    }
}
