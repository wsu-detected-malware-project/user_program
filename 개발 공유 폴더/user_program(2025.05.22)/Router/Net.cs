using DotNetEnv;
using System.Net.Http.Headers;
using System.Text;
using user_program.Util;



namespace user_program.Network{
    public class Net{
        public static async Task RunAsync() {
            string envPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
            if (!File.Exists(envPath)) {
                MessageBox.Show(".env 파일을 찾을 수 없습니다.\n" + envPath);
                return;
            }

            Env.Load(envPath);
            string filePath = Env.GetString("FILEPATH", "default_filepath");
            string filterPath = Env.GetString("FILTERPATH", "whitelist.csv");
            string url = Env.GetString("URL", "default_url");
            string savePath = Env.GetString("RESULT_PATH", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv"));

            if (!File.Exists(filePath)) {
                MessageBox.Show("검사 대상 파일이 존재하지 않습니다.", "전송 불가");
                return;
            }

            string[] headers;
            var filteredRows = WhitelistFilter.ApplyWhitelistFilter(filePath, filterPath, out headers);

            if (filteredRows == null || filteredRows.Count == 0)
            {
                MessageBox.Show("전송할 데이터가 없습니다.");
                return;
            }

            string tempCsvPath = Path.Combine(Path.GetTempPath(), $"filtered_{Guid.NewGuid()}.csv");

            try
            {
                using (var writer = new StreamWriter(tempCsvPath))
                {
                    writer.WriteLine(string.Join(",", headers));
                    foreach (var row in filteredRows)
                        writer.WriteLine(string.Join(",", row));
                }

                using (var client = new HttpClient())
                using (var form = new MultipartFormDataContent())
                using (var fileStream = File.OpenRead(tempCsvPath))
                using (var fileContent = new StreamContent(fileStream))
                {
                    client.Timeout = TimeSpan.FromMinutes(60);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    form.Add(fileContent, "file", Path.GetFileName(tempCsvPath));

                    HttpResponseMessage response = await client.PostAsync(url+"/upload", form);

                    if (response.IsSuccessStatusCode)
                    {
                        var resultBytes = await response.Content.ReadAsByteArrayAsync();
                        File.WriteAllBytes(savePath, resultBytes);

                        FilePath.AppendPathsToResult(savePath);
                    }
                }
            }
            finally
            {
                if (File.Exists(tempCsvPath))
                {
                    try { File.Delete(tempCsvPath); }
                    catch { }
                }
            }
        }
    }
}