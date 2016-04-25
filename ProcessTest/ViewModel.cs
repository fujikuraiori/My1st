namespace ProcessTest
{
    public class ViewModel : ViewModelBase
    {
        Model model = new Model();
        // 絶対パスを格納する
        private string _path;
        private string _state;
        private string _arg;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        public string Arg
        {
            get { return _arg; }
            set
            {
                _arg = value;
                OnPropertyChanged("Arg");
            }
        }
        public void setArg(string[] arg)
        {
            string str = "";
            foreach (string s in arg)
            {
                str += s + " ";
            }
            Arg = str;
        }
        /// <summary>
        /// ファイル指定ダイアログを表示する
        /// </summary>
        public void OpenFile()
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                dialog.Filter = "実行ファイル(*.exe;)|*.exe|すべてのファイル(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Path = dialog.FileName;
                    return;
                }
            }
        }
        /// <summary>
        /// パスをモデルに送る
        /// </summary>
        public void Start()
        {
            State = "実行しています";
            State = model.ProcessStart(Path,Arg);
        }
    }
}
