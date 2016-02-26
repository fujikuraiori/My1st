using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

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
        public ImageModel Model
        {
            get { return DataContext as ImageModel; }
        }
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
  
        }
        private void TreeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
            TreeViewClass t = TreeView1.SelectedItem as TreeViewClass;
            this.Model.SelectedImage(t.directoryPath);
            
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            this.Model.OpenDirectory();
        }

        private void MovieMaking_Click(object sender, RoutedEventArgs e)
        {
            this.Model.MovieMaking();
        }
    }
}
