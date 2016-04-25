using System;
using System.Windows;
/*
 * MVVMを理解すると共にプロセスの実行を理解する
 */
namespace ProcessTest
{
    /// <summary>
    /// ProcessTestMain.xaml の相互作用ロジック
    /// </summary>
    public partial class ProcessTestMain : Window
    {
        public ProcessTestMain()
        {
            InitializeComponent();
        }
        public ProcessTestMain(string[] arg)
        {
            InitializeComponent();
            viewModel.setArg(arg);
        }
        public ViewModel viewModel
        {
            get
            {
                return DataContext as ViewModel;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OpenFile();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            viewModel.Start();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }
    }
}
