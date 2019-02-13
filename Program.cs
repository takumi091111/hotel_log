using System;
using System.IO;
using System.Net.Http;
using Codeplex.Data;
using Newtonsoft.Json;

namespace hotel_log
{
  class Program {
    static void Main(string[] args) {
      // ログの保存先パス
      const string path = "./logs";
      // ログのファイル名
      string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + ".json";

      // ID, パスワードの入力
      Console.Write("従業員IDを入力してください: ");
      string id = Console.ReadLine();
      Console.Write("パスワードを入力してください: ");
      string password = Console.ReadLine();
      Console.WriteLine();

      // データ取得
      var json = LogDAO.findAll(id, password).Result;

      // 取得したデータをコンソールへ出力
      foreach (var item in json)
      {
        Console.WriteLine($"ログID: {item["id"]}");
        Console.WriteLine($"操作対象: {item["operation"]}");
        Console.WriteLine($"操作タイプ: {item["operation_type"]}");
        var empId = item["employee"] != null ? item["employee"]["id"] : "null";
        Console.WriteLine($"従業員ID: {empId}");
        Console.WriteLine($"作成日時: {item["created_at"]}");
        Console.WriteLine();
      }

      // JSONの整形
      var parsedJson = JsonConvert.DeserializeObject(json.ToString());
      string formattedJson = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);

      // 保存先フォルダが存在しない場合は、作成する
      if (!Directory.Exists(path)) Directory.CreateDirectory(path);

      // ファイルへ保存
      File.WriteAllText($"{path}/{fileName}", formattedJson);
      // ログファイルの絶対パスを取得
      string fullFilePath = Path.GetFullPath($"{path}/{fileName}");

      Console.WriteLine("以下のファイルへログの書き出しを行いました。");
      Console.WriteLine(fullFilePath);

      // キー入力待ち
      Console.ReadKey();
    }
  }
}
