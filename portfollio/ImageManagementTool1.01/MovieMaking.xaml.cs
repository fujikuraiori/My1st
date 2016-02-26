using System.Collections.Generic;
using System.Windows;

namespace ImageManagementTool
{
    /// <summary>
    /// MovieMaking.xaml の相互作用ロジック
    /// </summary>
    public partial class MovieMaking : Window
    {
        public MovieMakingModel Model = new MovieMakingModel();
        public MovieMaking(IList<ImageInfo> Images)
        {
            InitializeComponent();
            this.Model.SetImages(Images);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Model.MovieMaking(VideoName.Text,int.Parse(Rate.Text),int.Parse(Digit.Text));
        }
    }
}
