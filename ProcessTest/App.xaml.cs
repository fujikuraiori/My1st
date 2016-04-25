using System.Windows;

namespace ProcessTest
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var v = new ProcessTestMain(e.Args);
            v.ShowDialog();
        }
    }
}
