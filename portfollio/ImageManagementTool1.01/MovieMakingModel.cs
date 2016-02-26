using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ImageManagementTool
{
    public class MovieMakingModel
    {
        private IList<ImageInfo> _images;

        public IList<ImageInfo> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
            }
        }
        public void SetImages(IList<ImageInfo> images) 
        {
            this.Images = images;
        }
        public void MovieMaking(String name,int rate,int dig)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            String currentDirectory = System.IO.Directory.GetCurrentDirectory();
            String imageSource = Images[0].Path;
            imageSource = imageSource.Substring(0, imageSource.Length - (4+dig));
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            //出力を読み取れるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            //ウィンドウを表示しないようにする
            p.StartInfo.CreateNoWindow = true;
            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            p.StartInfo.Arguments = @"/c cd " + currentDirectory + " &cores\\ffmpeg.exe -f image2 -r " + rate.ToString() + " -i " + imageSource + "%0" + dig.ToString() + "d.jpg -r " + rate.ToString() + " -an -vcodec libx264 -pix_fmt yuv420p " +name+ ".mp4";
            //起動
            p.Start();

            //出力を読み取る
            string results = p.StandardOutput.ReadToEnd();

            //プロセス終了まで待機する
            //WaitForExitはReadToEndの後である必要がある
            //(親プロセス、子プロセスでブロック防止のため)
            p.WaitForExit();
            p.Close();
            Mouse.OverrideCursor = null;
        }
    }
}
