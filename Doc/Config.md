# AppSettings
## LogSetting
### DefaultLogFolderPath
- 預設Log資料夾路徑
### DefaultLogQueryDays
- 預設查詢幾天資料(EndDate會自動帶入StartDate後幾天)
- ``-1``代表不處理
### ElmahFileNamePattern
- 解析Elmah檔案名稱的RegexPattern
- Regex說明
    1. 時間戳記：4位數年份-2位月-2位日 + 6位時分秒 + 'Z'
    2. GUID：標準 8-4-4-4-12 格式的 UUID
### ElmahFileTimeFormatPattern
- 解析Elmah檔案時間的Pattern