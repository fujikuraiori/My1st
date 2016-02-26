using System.Collections.ObjectModel;
namespace ImageManagementTool
{
    public class TreeViewClass
    {
        /// <summary>
        /// TreeViewに項目を表示させるためのクラス
        /// 階層構造にしたい場合addChildで階層構造に出来る
        /// </summary>
        public string directoryPath;
        public string folderName { get; set; }
        private ObservableCollection<TreeViewClass> children_ = new ObservableCollection<TreeViewClass>();
        public ObservableCollection<TreeViewClass> Children
        {
            get { return children_; }
        }
      
        public TreeViewClass()
        {
            directoryPath = "NoData";
        }
        public TreeViewClass(string _path)
        {
            directoryPath = _path;
            string[] path = _path.Split('\\');
            folderName = path[path.Length - 1];
            
        }
        public void AddChild(TreeViewClass child)
        {
            children_.Add(child);
        }
    }
}
