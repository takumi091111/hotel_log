using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Codeplex.Data;

namespace hotel_log {
  class LogDAO
    {
      public static async Task<dynamic> findAll(string id, string password)
      {
        const string configFilePath = "./config.json";
        string API_BASE_URL;

        if (File.Exists(configFilePath))
        {
          // config.jsonの設定を読み込む
          string config = File.ReadAllText(configFilePath);
          var json = DynamicJson.Parse(config);
          API_BASE_URL = json["apiBaseUrl"];
        }
        else
        {
          // デフォルト値を使用
          API_BASE_URL = "http://localhost:8080";
        }
        
        string url = $"{API_BASE_URL}/logs/list";
        var request = new HttpRequestMessage
        {
          Method = HttpMethod.Get,
          RequestUri = new Uri(url)
        };
        request.Headers.Authorization = new AuthenticationHeaderValue(
          "Basic",
          Convert.ToBase64String(Encoding.ASCII.GetBytes($"{id}:{password}"))
        );

        using (HttpClient httpClient = new HttpClient())
        {
          try
          {
            var response = await httpClient.SendAsync(request);
            var json = DynamicJson.Parse(await response.Content.ReadAsStringAsync());
            return json;
          }
          catch(HttpRequestException)
          {
            throw new Exception("サーバへの接続に失敗しました。");
          }
          catch(FormatException)
          {
            throw new Exception("IDまたはパスワードが違う、もしくはアクセス権限がありません。");
          }
        }
      }
    }
}
