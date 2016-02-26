using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace ImageManagementTool
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private string[] _supportExts = { ".jpg", ".bmp", ".png", ".tiff", ".gif" };
        public string[] SupportExts
        {
            get { return _supportExts; }
        }
        public static new App Current
        {
            get { return Application.Current as App; }
        }
    }
}
