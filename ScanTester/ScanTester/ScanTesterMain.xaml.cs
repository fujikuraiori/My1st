﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
    }
}