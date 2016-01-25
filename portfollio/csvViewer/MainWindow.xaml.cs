using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
/***************************************************************
 * 学籍番号1323060　藤倉伊織
 * last update 2015/12/17
 * ************************************************************/
namespace Tsuchidasan
{
    
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///  root_はtreeViewにバインドされており、これに子項目を追加することで項目を作成している
        ///  selected~は何をKeyにしてリストが作成されたかを保持する
        /// </summary>
        TreeViewClass root_;
        string db_file = "csvDataTable.db";
        string SaveDataPath = "saveTreeViewData.xml";
        string selectedKey = "";
        string selectedCategory = "";
        public MainWindow()
        {   
            InitializeComponent();
            List<CsvDataTable> myList = new List<CsvDataTable>() { };
            ///起動時にデータベースがあるかどうか。無ければ作成する
            if (System.IO.File.Exists(db_file)){}
            else
            {
                createDataBase();
            }
            ///起動時にXMLがあるかどうか。あれば読み込む
            if (System.IO.File.Exists(SaveDataPath))
            {
                //XmlSerializerオブジェクトを作成
                System.Xml.Serialization.XmlSerializer serializer =new System.Xml.Serialization.XmlSerializer(typeof(TreeViewClass));
                //読み込むファイルを開く
                System.IO.StreamReader sr = new System.IO.StreamReader(SaveDataPath, new System.Text.UTF8Encoding(false));
                //XMLファイルから読み込み、逆シリアル化する
                root_ = (TreeViewClass)serializer.Deserialize(sr);
                //ファイルを閉じる
                sr.Close();
                ///ステータスバーに次の指示を描写
                StatusLabel.Content = "TreeViewの項目をクリックすることでCSVDataViewにデータを表示することが出来ます";
            }else{
                root_ = new TreeViewClass("root");
            }
            ///root_をXAMLから読み込ませるためにDataContextを使用
            DataContext = root_;
            dataGrid1.ItemsSource = GetDataBaseTable(myList).DefaultView;
        }
        /// <summary>
        /// Windowが消されたときにroot_を保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            //保存するクラス(SampleClass)のインスタンスを作成
            //XmlSerializerオブジェクトを作成
            //オブジェクトの型を指定する
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(TreeViewClass));
            //書き込むファイルを開く（UTF-8 BOM無し）
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                SaveDataPath, false, new System.Text.UTF8Encoding(false));
            //シリアル化し、XMLファイルに保存する
            serializer.Serialize(sw, root_);
            //ファイルを閉じる
            sw.Close();
        }
        /// <summary>
        /// TreeViewにドラッグ＆ドロップされたときにコピーされたファイルを読み込み、データベースに保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_Drop(object sender, DragEventArgs e)
        {
            List<CsvDataTable> myList = new List<CsvDataTable>() { };
            //mList.Clear();
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            string content = "";
            string line = "";
            foreach (String s in files)
            {

                if (!File.Exists(s))
                {
                    string[] folder = System.IO.Directory.GetFiles(s, "*.CSV", System.IO.SearchOption.AllDirectories);
                    foreach (String S in folder)
                    {
                        content = File.ReadAllText(S, Encoding.GetEncoding("Shift_JIS"));
                        using (StreamReader sr = new StreamReader(S, Encoding.GetEncoding("Shift_JIS")))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                CsvDataTable addCsvDataTable = new CsvDataTable(line);
                                myList.Add(addCsvDataTable);
                            }
                        }
                        
                    }
                    
                }
                else
                {
                    if (s.EndsWith(".csv") || s.EndsWith(".CSV"))
                    {
                        content = File.ReadAllText(s, Encoding.GetEncoding("Shift_JIS"));
                        using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding("Shift_JIS")))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                CsvDataTable addCsvDataTable = new CsvDataTable(line);
                                myList.Add(addCsvDataTable);
                            }
                        }
                    }
                }
            }
            Insert(db_file, myList);
            createFromDataBaseToTreeView(db_file);
        }
        /// <summary>
        /// ドラッグされてきたときにファイルを読み込み、コピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
            e.Handled = true;
        }
        /// <summary>
        /// TreeViewの中の項目が選択されたときに発生するイベント
        /// 郵便番号データの県名（漢字）から選ばれた県名を検索する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (TreeView1.SelectedItem != null)
            {
                List<CsvDataTable> myList = new List<CsvDataTable>() { };
                TreeViewClass p = (TreeView1.SelectedItem) as TreeViewClass;
                selectedKey = p.PrefectureName;
                selectedCategory = "PrefecturesKANZI";
                myList = select(db_file, selectedCategory, selectedKey);
                dataGrid1.ItemsSource = GetDataBaseTable(myList).DefaultView;
                StatusLabel.Content = "Shift + S で編集内容を保存できます";
            }
        }
        /// <summary>
        /// CsvDataTableのリストをDataTableに変換する
        /// </summary>
        /// <param name="_myDataTable">DataTableに変換されるCsvDataTableリスト</param>
        /// <returns></returns>
        private DataTable GetDataBaseTable(List<CsvDataTable> _myDataTable) 
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("全国地方公共団体コード", typeof(String));
            dt.Columns.Add("（旧）郵便番号", typeof(String));
            dt.Columns.Add("郵便番号", typeof(String));
            dt.Columns.Add("都道府県名(カナ)", typeof(String));
            dt.Columns.Add("市区町村名(カナ)", typeof(String));
            dt.Columns.Add("町域名(カナ)", typeof(String));
            dt.Columns.Add("都道府県名", typeof(String));
            dt.Columns.Add("市区町村名", typeof(String));
            dt.Columns.Add("町域名", typeof(String));
            dt.Columns.Add("一町域が二以上の郵便番号で表される場合の表示", typeof(String));
            dt.Columns.Add("小字毎に番地が起番されている町域の表示", typeof(String));
            dt.Columns.Add("丁目を有する町域の場合の表示", typeof(String));
            dt.Columns.Add("一つの郵便番号で二以上の町域を表す場合の表示", typeof(String));
            dt.Columns.Add("更新の表示", typeof(String));
            dt.Columns.Add("変更理由", typeof(String));
            foreach (CsvDataTable list in _myDataTable)
            {
                DataRow dr = dt.NewRow();
                dr[0] = list.NationalLocalGovernmentCode;
                dr[1] = list.OldPostalCode;
                dr[2] = list.PostalCode;
                dr[3] = list.Prefectures;
                dr[4] = list.City;
                dr[5] = list.TownArea;
                dr[6] = list.PrefecturesKANZI;
                dr[7] = list.CityKANZI;
                dr[8] = list.TownAreaKANZI;
                dr[9] = list.Case1;
                dr[10] = list.Case2;
                dr[11] = list.Case3;
                dr[12] = list.Case4;
                dr[13] = list.Case5;
                dr[14] = list.ReasonForChange;
                dt.Rows.Add(dr);
            }
            return dt;
        }
      /// <summary>
      /// Shift + Sが押されたときに発生するイベント
      /// 現在表示されているリストを保存する
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Shift) > 0 && Key.S == e.Key)
            {
                MessageBoxResult result = MessageBox.Show("変更を保存しますか？", "編集中のCSVデータをデータベースに保存します。", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    //「はい」が選択された時
                    List<CsvDataTable> saveList = new List<CsvDataTable>();
                    CsvDataTable addList = new CsvDataTable();
                    DataView ddt = dataGrid1.ItemsSource as DataView;
                        foreach (DataRowView row in ddt)
                        {
                            addList.NationalLocalGovernmentCode = row[0] as string;
                            addList.OldPostalCode = row[1] as string;
                            addList.PostalCode = row[2] as string;
                            addList.Prefectures = row[3] as string;
                            addList.City = row[4] as string;
                            addList.TownArea = row[5] as string;
                            addList.PrefecturesKANZI = row[6] as string;
                            addList.CityKANZI = row[7] as string;
                            addList.TownAreaKANZI = row[8] as string;
                            addList.Case1 = row[9] as string;
                            addList.Case2 = row[10] as string;
                            addList.Case3 = row[11] as string;
                            addList.Case4 = row[12] as string;
                            addList.Case5 = row[13] as string;
                            addList.ReasonForChange = row[14] as string;
                            saveList.Add(new CsvDataTable(addList));
                        } 
                    Update(db_file,selectedKey,selectedCategory,saveList);
                }
            }  
        }
        /// <summary>
        /// データを更新させる。表示されていたデータをデータベースから削除し、
        /// 現在表示されているデータを入れ直すことで更新を行う
        /// </summary>
        /// <param name="_db_file"></param>
        /// <param name="_selectedKey"></param>
        /// <param name="_selectedCategory"></param>
        /// <param name="_myDataTable"></param>
        private void Update(string _db_file,string _selectedKey,string _selectedCategory, List<CsvDataTable> _myDataTable)
        {
            using (var conn = new SQLiteConnection("Data Source= " + _db_file))
            {
                conn.Open();
                using (SQLiteTransaction sqlt = conn.BeginTransaction())
                {
                    using (SQLiteCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "delete from CsvDatas where "+ _selectedCategory +" = '" + _selectedKey + "'";
                        command.ExecuteNonQuery();
                    }
                    sqlt.Commit();
                }
                conn.Close();
            }
            Insert(_db_file,_myDataTable);
        }
        /// <summary>
        /// CsvDataTableのリストをデータテーブルに保存する
        /// BeginTransactionをすることで早く処理をすることが出来るらしい
        /// </summary>
        /// <param name="_db_file"></param>
        /// <param name="_myDataTable"></param>
        private void Insert(string _db_file, List<CsvDataTable> _myDataTable)
        {
            using (var conn = new SQLiteConnection("Data Source=" + _db_file))
            {
                conn.Open();
                using (SQLiteTransaction sqlt = conn.BeginTransaction())
                {
                    using (SQLiteCommand command = conn.CreateCommand())
                    {
                        foreach (CsvDataTable m in _myDataTable)
                        {
                            command.CommandText = "insert into CsvDatas(NationalLocalGovernmentCode,OldPostalCode,PostalCode,Prefectures,City,TownArea,PrefecturesKANZI,CityKANZI,TownAreaKANZI,Case1,Case2,Case3,Case4,Case5,ReasonForChange) values('" + m.NationalLocalGovernmentCode.ToString() + "', '" + m.OldPostalCode + "','" + m.PostalCode + "', '" + m.Prefectures + "','" + m.City + "', '" + m.TownArea + "','" + m.PrefecturesKANZI + "', '" + m.CityKANZI + "','" + m.TownAreaKANZI + "', '" + m.Case1 + "','" + m.Case2 + "', '" + m.Case3 + "','" + m.Case4 + "','" + m.Case5 + "','" + m.ReasonForChange + "')";
                            command.ExecuteNonQuery();
                        }
                    }
                    sqlt.Commit();
                }
                conn.Close();
            }
        }
        /// <summary>
        /// 選ばれた項目から選ばれた名前を探す
        /// 項目が選ばれなかった（all）場合すべての項目から探す
        /// 結果はCsvDataTableのリストにして帰ってくる
        /// </summary>
        /// <param name="_db_file"></param>
        /// <param name="csvItem"></param>
        /// <param name="selectCsvItem"></param>
        /// <returns></returns>
        private List<CsvDataTable> select(string _db_file, string csvItem,string selectCsvItem)
        {
            List<CsvDataTable> result = new List<CsvDataTable>();
            using (var conn = new SQLiteConnection("Data Source=" + _db_file))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    if (csvItem == "all")
                    {
                        command.CommandText = "SELECT * from CsvDatas WHERE (PrefecturesKANZI LIKE '%" + selectCsvItem + "%') OR (CityKANZI LIKE '%" + selectCsvItem + "%') OR (TownAreaKANZI LIKE '%" + selectCsvItem + "%') OR (PostalCode LIKE '%" + selectCsvItem + "%') OR (NationalLocalGovernmentCode LIKE '%" + selectCsvItem + "%')"; 
                    }
                    else
                    {
                        command.CommandText = "SELECT * from CsvDatas WHERE " + csvItem + " ='" + selectCsvItem + "'";
                    }
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string NationalLocalGovernmentCode = reader["NationalLocalGovernmentCode"].ToString() ;
                            string OldPostalCode = reader["OldPostalCode"].ToString();
                            string PostalCode = reader["PostalCode"].ToString();
                            string Prefectures = reader["Prefectures"].ToString();
                            string City = reader["City"].ToString();
                            string TownArea = reader["TownArea"].ToString();
                            string PrefecturesKANZI = reader["PrefecturesKANZI"].ToString();
                            string CityKANZI = reader["CityKANZI"].ToString();
                            string TownAreaKANZI = reader["TownAreaKANZI"].ToString();
                            string Case1 = reader["Case1"].ToString();
                            string Case2 = reader["Case2"].ToString();
                            string Case3 = reader["Case3"].ToString();
                            string Case4 = reader["Case4"].ToString();
                            string Case5 = reader["Case5"].ToString();
                            string ReasonForChange = reader["ReasonForChange"].ToString();

                            result.Add(new CsvDataTable( NationalLocalGovernmentCode, OldPostalCode, PostalCode, Prefectures, City,
                                TownArea, PrefecturesKANZI, CityKANZI, TownAreaKANZI, Case1, Case2, Case3, Case4, Case5, ReasonForChange));
                        }
                    }
                }
                conn.Close();
            }
            return result;
        }
        /// <summary>
        /// データベースからツリービューを作成する
        /// </summary>
        /// <param name="db_file"></param>
        private void createFromDataBaseToTreeView(string db_file)
        {
            ///rootに何も追加されていない状態にする
            root_.ClearChild();
            using (var conn = new SQLiteConnection("Data Source=" + db_file))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    ///DISTINCTで県名を一種類ずつ取り出す
                    command.CommandText = "SELECT DISTINCT PrefecturesKANZI from CsvDatas ";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ///取り出した県名をrootに追加する
                            root_.AddChild(new TreeViewClass(reader["PrefecturesKANZI"].ToString()));
                        }
                    }
                }
                conn.Close();
            }
        }
        /// <summary>
        /// データベースを作成する
        /// </summary>
        private void createDataBase() 
        {
            using (var conn = new SQLiteConnection("Data Source=" + db_file))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "create table CsvDatas(NationalLocalGovernmentCode TEXT ,OldPostalCode TEXT,PostalCode TEXT,Prefectures TEXT,City TEXT,TownArea TEXT,PrefecturesKANZI TEXT,CityKANZI TEXT,TownAreaKANZI TEXT,Case1 TEXT,Case2 TEXT,Case3 TEXT,Case4 TEXT,Case5 TEXT,ReasonForChange TEXT)";
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        /// <summary>
        /// ヘルプが押されたときにヘルプを表示させる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow hW = new HelpWindow();
            hW.ShowDialog();
        }
        /// <summary>
        /// 検索が押されたときに、テキストボックスの中身をkeyとして検索する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<CsvDataTable> myList = new List<CsvDataTable>() { };
            selectedKey = SearchText.Text;
            ///全ての項目から選ぶ
            selectedCategory = "all" ;
            myList = select(db_file, selectedCategory, selectedKey);
            dataGrid1.ItemsSource = GetDataBaseTable(myList).DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(textBox1 != null){
            DataView ddt = dataGrid1.ItemsSource as DataView;
                using (var sw = new System.IO.StreamWriter(textBox1.Text+".CSV", false, System.Text.Encoding.GetEncoding("Shift_JIS")))
                {
                    foreach (DataRowView row in ddt)
                    {
                     sw.WriteLine(row[0] as string +","+row[1] as string +","+row[2] as string +","+row[3] as string +","+row[4] as string +","+row[5] as string +","+row[6] as string +","+row[7] as string +","+row[8] as string +","+row[9] as string +","+row[10] as string +","+row[11] as string +","+row[12] as string +","+row[13] as string +","+row[14] as string );
                    }  
                }
            }
        } 
    }
}
