using System.Diagnostics;
using System.Text.Json;

namespace ErrorHelper.Model.Common.Config
{
    public class AppSettings
    {
        public static ElmahSetting Elmah { get; private set; } = new ElmahSetting();

        static AppSettings()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
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
