using ErrorHelper.Core.Common.Configuration;
using ErrorHelper.Core.Model.Common.Configuration.AppSettings;
using ErrorHelper.Tool;
using System.Diagnostics;
using System.Text.Json;

namespace ErrorHelper.Infrastructure.Common.Configuration
{
    public sealed class AppSettings : IAppSettings
    {
        public static BackupSetting BackupSetting { get; private set; } = new BackupSetting();
        public static LogSetting LogSetting { get; private set; } = new LogSetting();
        static AppSettings()
        {
            try
            {
                // 取得目前正在執行的程式(EXE)檔案所在完整路徑下的appsettings.json
                string path = Path.Combine(FileTool.ThisExeDir, "Config", "AppSettings.json");

                string jsonString = File.ReadAllText(path);

                using JsonDocument doc = JsonDocument.Parse(jsonString);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("AppSettings", out JsonElement appSettingsElement))
                {
                    if (appSettingsElement.TryGetProperty("LogSetting", out JsonElement elmahElement))
                    {
                        LogSetting = JsonSerializer.Deserialize<LogSetting>(elmahElement.GetRawText()) ?? new LogSetting();
                    }
                    else
                    {
                        LogSetting = new LogSetting();
                    }

                    if (appSettingsElement.TryGetProperty("BackupSetting", out JsonElement backupElement))
                    {
                        BackupSetting = JsonSerializer.Deserialize<BackupSetting>(backupElement.GetRawText()) ?? new BackupSetting();
                    }
                    else
                    {
                        BackupSetting = new BackupSetting();
                    }
                }
            }
            catch (Exception ex)
            {
                // 讀取失敗時給預設物件，或可加錯誤處理
                LogSetting = new LogSetting();
                BackupSetting = new BackupSetting();

                Debug.WriteLine($"讀取 appsettings.json 發生錯誤: {ex.Message}");
            }
        }
    }
}
