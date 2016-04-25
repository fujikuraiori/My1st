using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
namespace ProcessTest
{
    class Model
    {
        
        /// <summary>
        /// 絶対パスを引数にし、示されている実行ファイルを実行し、終了時異常終了か正常終了かを表示する。
        /// </summary>
        /// <param name="filePath"></param>
        public String ProcessStart(string filePath ,string arg)
        {
            string state;
            // 他のプロセスを起動している間操作できないことをマウスのアイコンで知らせる
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            // 指定された場所にファイルがあるか確認する
            if (File.Exists(filePath))
            {
                Process p = new Process();
                // 実行するファイルのパスを設定する
                p.StartInfo.FileName = filePath;
                
                    p.StartInfo.Arguments = arg;
                
                // 実行し、できなかった場合終了
                if (p.Start())
                {
                    // 終了するまで待機する 
                    while (p.HasExited == false)
                    { 
                        //p.WaitForExit();
                    }
                }
                else
                {
                    p.Close();
                    p.Dispose();
                    Mouse.OverrideCursor = null;
                    return "実行に失敗しました";
                }
              
               
                // 終了コードを取得する
                int iExitCode = p.ExitCode;
                // 終了コードから正常終了か異常終了かを判別して表示する
                if (iExitCode == 0)
                {
                    state = "正常終了しました";
                }
                else
                {
                    state = "異常終了しました エラーコード :" + iExitCode.ToString();
                }
                
                
            }
            else
            {
                state = "ファイルパスが間違っています";
                
            }
            Mouse.OverrideCursor = null;
            return state;
        }
    }
}
