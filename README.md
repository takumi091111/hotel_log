# ホテル宿泊管理API ログ書き出し

## 使い方

1. 実行する
2. 従業員IDとパスワードを入力する
3. `./logs/yyyy-MM-dd-HH:mm:ss.json`へログが書き出される
4. 終了

## 設定

config.jsonを適宜編集する。

```json
{
  // APIサーバのURLを入れる
  "apiBaseUrl": "http://localhost:8080"
}
```
