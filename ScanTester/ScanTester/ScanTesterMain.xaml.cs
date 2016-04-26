using System;
using System.Windows;
using System.Windows.Forms;

namespace ScanTester
{
    /// <summary>
    /// ScanTesterMain.xaml の相互作用ロジック
    /// </summary>
    public partial class ScanTesterMain : Window
    {
        public ScanTesterMain()
        {
            InitializeComponent();
        }
        public ViewModel viewModel
        {
            get { return DataContext as ViewModel; }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Start();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Start();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveStart();
        }
    }
}
