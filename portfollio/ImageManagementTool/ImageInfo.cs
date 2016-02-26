using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImageManagementTool
{
    public static class ImageUtils
    {
        public static IList<ImageInfo> GetImages(string directory, string[] supportExts)
        {
            if (!Directory.Exists(directory))
            {
                return new List<ImageInfo>();
            }
            var dirInfo = new DirectoryInfo(directory);
            return dirInfo.GetFiles().Where(f => supportExts.Contains(f.Extension)).Select(f => new ImageInfo { Path = f.FullName }).ToList();
        }
    }
    public class ImageInfo
    {
        public string Path { get; set; }
    }
}
