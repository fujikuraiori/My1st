using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
namespace ScanTester
{
    class Model
    {
        public IList<ImageDatas> Start()
        {
            IList<ImageDatas> myList = new List<ImageDatas>();
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    double widAve = 0.0d;
                    double heiAve = 0.0d;
                    string[] files = System.IO.Directory.GetFiles(dialog.SelectedPath, "*", System.IO.SearchOption.TopDirectoryOnly);
                    foreach (string basePath in files)
                    {
                        ImageDatas addImgData = new ImageDatas();
                        addImgData.imgName = basePath.Split('\\')[basePath.Split('\\').Length-1];
                        Bitmap bmpOrg = Bitmap.FromFile(basePath) as Bitmap;
                        addImgData.imgWidth = bmpOrg.Width;
                        widAve += bmpOrg.Width;
                        addImgData.imgHeight = bmpOrg.Height;
                        heiAve += bmpOrg.Height;
                        addImgData.horizontalResolution = bmpOrg.HorizontalResolution;
                        addImgData.verticalResolution = bmpOrg.VerticalResolution;
                        myList.Add(addImgData);
                        bmpOrg.Dispose();
                    }
                    widAve /= files.Length;
                    heiAve /= files.Length;
                    for (int i = 0; i < myList.Count; i++)
                    {
                        myList[i].widError = widAve - myList[i].imgWidth;
                        myList[i].heiError = heiAve - myList[i].imgHeight;
                    }
                }
            }
            return myList;
        }
        public void ErrorCheck(IList<ImageDatas> myList,ref double errorWid,ref double errorHei)
        {
            foreach(ImageDatas iD in myList)
            {
                if (errorWid < iD.widError) { errorWid = iD.widError; }
                if (errorHei < iD.heiError) { errorHei = iD.heiError; }
            }
        }
    }
}
