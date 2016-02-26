using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace ImageManagementTool
{
    public class ImageModel : INotifyPropertyChanged
    {
        private TreeViewClass _root =new TreeViewClass();
        private IList<ImageInfo> _images;
        public ImageModel()
        {
            PropertyChanged += (sender, e) => { };
        }
        public TreeViewClass Root
        {
            get 
            {
                return _root;
            }
            set
            {
                _root = value;
                OnPropertyChanged("Root");
            }

        }
        public IList<ImageInfo> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                OnPropertyChanged("Images");
            }
        }
        public void SelectedImage(String _path)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.Images = ImageUtils.GetImages(_path,App.Current.SupportExts);
            Mouse.OverrideCursor = null;
        }
        public void OpenDirectory()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    Mouse.OverrideCursor = null;
                    return;
                }
                else
                {
                    this.Images = ImageUtils.GetImages(dialog.SelectedPath, App.Current.SupportExts);
                    this.Root.AddChild(CreateNodeLoop(dialog.SelectedPath, 0));
                    Mouse.OverrideCursor = null;
                }
            }
        }
        
        private TreeViewClass CreateNodeLoop(String selectedDirectory,int n)
        {
            TreeViewClass Node = new TreeViewClass(selectedDirectory);

            String[] subDirs = System.IO.Directory.GetDirectories(selectedDirectory,"*");
            
            foreach (String s in subDirs)
            {
                TreeViewClass addNode = new TreeViewClass(s);
                String[] subDirectorys2 = System.IO.Directory.GetDirectories(s,"*");
                foreach(String s2 in subDirectorys2)
                {
                    addNode.AddChild(new TreeViewClass(s2));
                }
                Node.AddChild(addNode);
                
            }
            return Node;
        }
        private TreeViewClass CreateNode(String selectedDirectory ,int n)
        {
            TreeViewClass Node = new TreeViewClass(selectedDirectory);
            String[] subDirectorys = System.IO.Directory.GetDirectories(selectedDirectory, "*");
            if (n > 5)
            {
                return Node;
            }
            foreach (String s in subDirectorys)
            {
                Node.AddChild(CreateNode(s, n + 1));
            }
            return Node;
        }
        /*
         * 作るべきコマンド
         * -f image2 -r 24 -i source%04d.jpg -r 24 -an -vcodec libx264 -pix_fmt yuv420p video.mp4
         */
        public void MovieMaking() 
        {
            String currentDirectory = System.IO.Directory.GetCurrentDirectory();
            String imageSource = Images[0].Path;
            imageSource = imageSource.Substring(0, imageSource.Length - 8);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            //出力を読み取れるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            //ウィンドウを表示しないようにする
            p.StartInfo.CreateNoWindow = true;
            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            p.StartInfo.Arguments = @"/c cd " + currentDirectory +" &cores\\ffmpeg.exe -f image2 -r 24 -i "+imageSource+"03%02d.jpg -r 24 -an -vcodec libx264 -pix_fmt yuv420p video.mp4";

            //起動
            p.Start();

            //出力を読み取る
            string results = p.StandardOutput.ReadToEnd();

            //プロセス終了まで待機する
            //WaitForExitはReadToEndの後である必要がある
            //(親プロセス、子プロセスでブロック防止のため)
            p.WaitForExit();
            p.Close();
            Console.WriteLine(results);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
