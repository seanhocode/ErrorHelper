using System.Diagnostics;
using System.Text.Json;

namespace ErrorHelper.Model.Common.Config
{
    public class AppSettings
    {
        public static ElmahSetting Elmah { get; private set; } = new ElmahSetting();
        public static BackupSetting Backup { get; private set; } = new BackupSetting("Appsetting");

        static AppSettings()
        {
            try
            {
                // 取得目前正在執行的程式(EXE)檔案所在完整路徑下的appsettings.json
                string exePath = Process.GetCurrentProcess().MainModule!.FileName;
                string exeDir = Path.GetDirectoryName(exePath)!;
                string path = Path.Combine(exeDir, "appsettings.json");

                string jsonString = File.ReadAllText(path);

                using JsonDocument doc = JsonDocument.Parse(jsonString);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("AppSettings", out JsonElement appSettingsElement))
                {
                    if(appSettingsElement.TryGetProperty("Elmah", out JsonElement elmahElement))
                    {
                        Elmah = JsonSerializer.Deserialize<ElmahSetting>(elmahElement.GetRawText()) ?? new ElmahSetting();
                    }
                    else
                    {
                        Elmah = new ElmahSetting();
                    }

                    if (appSettingsElement.TryGetProperty("Backup", out JsonElement backupElement))
                    {
                        Backup = JsonSerializer.Deserialize<BackupSetting>(backupElement.GetRawText()) ?? new BackupSetting();
                    }
                    else
                    {
                        Backup = new BackupSetting();
                    }
                }
            }
            catch (Exception ex)
            {
                // 讀取失敗時給預設物件，或可加錯誤處理
                Elmah = new ElmahSetting();
                Debug.WriteLine($"讀取 appsettings.json 發生錯誤: {ex.Message}");
            }
        }
    }
}
