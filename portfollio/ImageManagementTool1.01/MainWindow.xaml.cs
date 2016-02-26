using System.ComponentModel;
using System.Windows;

namespace ImageManagementTool
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public ImageManagementModel Model
        {
            get { return DataContext as ImageManagementModel; }
        }
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
  
        }
        private void TreeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
            TreeViewClass t = TreeView1.SelectedItem as TreeViewClass;
            this.Model.SelectedImage(t.directoryPath);
            
        }


        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            this.Model.OpenDirectory();
        }

        private void MovieMaking_Click(object sender, RoutedEventArgs e)
        {
            this.Model.OpenMovieMaking();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
