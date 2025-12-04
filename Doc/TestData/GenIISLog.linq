<Query Kind="Program">
  <Namespace>System.IO</Namespace>
  <Namespace>System.Text</Namespace>
</Query>

void Main()
{
	string output = @"C:\Temp\u_ex[yymmdd].log";
    const int lines = 1_000_000_0;
    var sb = new StringBuilder(200);

    var rand = new Random();
    DateTime baseTime = DateTime.Now.Date;
	output = output.Replace("[yymmdd]", baseTime.ToString("yyMMdd"));

    using (var fs = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None, 64 * 1024))
    using (var sw = new StreamWriter(fs))
    {
        // ---- IIS Header ----
        sw.WriteLine("#Software: Microsoft Internet Information Services 10.0");
        sw.WriteLine("#Version: 1.0");
        sw.WriteLine($"#Date: {baseTime.ToString("yyyy-MM-dd HH:mm:ss")}");
        sw.WriteLine("#Fields: date time cs-method cs-uri-stem sc-status sc-bytes cs-bytes time-taken");

        var currentTime = baseTime;

        for (int i = 0; i < lines; i++)
        {
            // ---- 模式 B：遞增時間 + 隨機 0~50ms（類比負載） ----
            currentTime = currentTime.AddMilliseconds(rand.Next(0, 50));

            sb.Clear();
            sb.Append(currentTime.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append(" GET /page?id=");
            sb.Append(i);
            sb.Append(" 200 1024 512 ");
            sb.Append(rand.Next(1, 200)); // time-taken

            sw.WriteLine(sb);
        }
    }
}

// Define other methods and classes here