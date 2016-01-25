using System.Collections.ObjectModel;

namespace Tsuchidasan
{
    public class TreeViewClass
    {
        /// <summary>
        /// TreeViewに項目を表示させるためのクラス
        /// 階層構造にしたい場合addChildで階層構造に出来る
        /// </summary>
        public string PrefectureName{ get; set; }
        private ObservableCollection<TreeViewClass> children_ = new ObservableCollection<TreeViewClass>();
        public ObservableCollection<TreeViewClass> Children
        {
            get { return children_; }
        }
      
        public TreeViewClass()
        {
            PrefectureName = "NoData";
        }
        public TreeViewClass(string _PrefectureName)
        {
            PrefectureName = _PrefectureName;
        }
    
        public void AddChild(TreeViewClass child)
        {
            children_.Add(child);
        }
        public void ClearChild(TreeViewClass child)
        {
            children_.Remove(child);
        }
        public void ClearChild()
        {
            children_.Clear();
        }
    }
}
