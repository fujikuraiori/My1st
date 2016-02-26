using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace ImageManagementTool
{
    public class ImageManagementModel : INotifyPropertyChanged
    {
        private TreeViewClass _root = new TreeViewClass();
        private IList<ImageInfo> _images;
        public ImageManagementModel()
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

        /*****************************************************************************************************************************
         * 再帰を用いたNode作成
         * 必要なし。でもたまに使うから取っておく
         * ***************************************************************************************************************************/
        private TreeViewClass CreateNode(String selectedDirectory, int n)
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
        public void OpenMovieMaking()
        {
            if (Images != null && Images.Count != 0)
            {
                MovieMaking MMWindow = new MovieMaking(Images);
                MMWindow.ShowDialog();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
