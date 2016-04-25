using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanTester
{
    public class ImageDatas
    {
        public string imgName { get; set; }
        public int imgWidth { get; set; }
        public int imgHeight { get; set; }
        public double horizontalResolution { get; set; }
        public double verticalResolution { get; set; }
        public double widError { get; set; }
        public double heiError { get; set; }
    }
}
