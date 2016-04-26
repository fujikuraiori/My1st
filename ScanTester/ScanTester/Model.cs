using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

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
                    string[] files = System.IO.Directory.GetFiles(dialog.SelectedPath, "*.jpg", System.IO.SearchOption.TopDirectoryOnly);
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
        public ImageErrors ErrorCheck(IList<ImageDatas> myList ,ImageErrors errors)
        {
            double aveWid = 0.0d;
            double aveHei = 0.0d;
            foreach(ImageDatas iD in myList)
            {
                aveWid += Math.Abs(iD.widError);
                aveHei += Math.Abs(iD.heiError);
                if (Math.Abs(errors.maxWidError) < Math.Abs(iD.widError)) { errors.maxWidError = iD.widError; }
                if (Math.Abs(errors.maxHeiError) < Math.Abs(iD.heiError)) { errors.maxHeiError = iD.heiError; }
            }
            errors.aveWidError = aveWid / myList.Count;
            errors.aveHeiError = aveHei / myList.Count;
            return errors;
        }
        public void ResultSave(IList<ImageDatas> myList)
        {
            if (myList != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                //はじめのファイル名を指定する
                sfd.FileName = "save.dat";
                //はじめに表示されるフォルダを指定する
                sfd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                //[ファイルの種類]に表示される選択肢を指定する
                sfd.Filter = "datファイル(*.dat)|*.dat;|すべてのファイル(*.*)|*.*";
                //タイトルを設定する
                sfd.Title = "保存先のファイルを選択してください";
                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                sfd.RestoreDirectory = true;
                //ダイアログを表示する
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.Stream stream;
                    stream = sfd.OpenFile();
                    if (stream != null)
                    {
                        //ファイルに書き込む
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                        foreach (ImageDatas iD in myList)
                        {
                            sw.Write(iD.widError.ToString() + "  " + iD.heiError.ToString()+"\n");
                        }
                        //閉じる
                        sw.Close();
                        stream.Close();
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("保存するデータがありません", "error");
            }
        }
    }
}
