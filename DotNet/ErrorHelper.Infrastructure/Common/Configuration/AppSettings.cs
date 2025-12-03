using ErrorHelper.Core.Common.Configuration;
using ErrorHelper.Core.Model.Common.Configuration.AppSettings;
using System.Diagnostics;
using System.Text.Json;
using SeanTool.CSharp.Net8;

namespace ErrorHelper.Infrastructure.Common.Configuration
{
    public sealed class AppSettings : IAppSettings
    {
        public static BackupSetting BackupSetting { get; private set; } = new BackupSetting();
        public static LogSetting LogSetting { get; private set; } = new LogSetting();
        public static SystemSetting SystemSetting { get; private set; } = new SystemSetting();

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
                    if (appSettingsElement.TryGetProperty("LogSetting", out JsonElement logSettingElement))
                    {
                        LogSetting = JsonSerializer.Deserialize<LogSetting>(logSettingElement.GetRawText()) ?? new LogSetting();
                    }
                    else
                    {
                        LogSetting = new LogSetting();
                    }

                    if (appSettingsElement.TryGetProperty("BackupSetting", out JsonElement backupSettingElement))
                    {
                        BackupSetting = JsonSerializer.Deserialize<BackupSetting>(backupSettingElement.GetRawText()) ?? new BackupSetting();
                    }
                    else
                    {
                        BackupSetting = new BackupSetting();
                    }

                    if (appSettingsElement.TryGetProperty("SystemSetting", out JsonElement systemSettingElement))
                    {
                        SystemSetting = JsonSerializer.Deserialize<SystemSetting>(systemSettingElement.GetRawText()) ?? new SystemSetting();
                    }
                    else
                    {
                        SystemSetting = new SystemSetting();
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
