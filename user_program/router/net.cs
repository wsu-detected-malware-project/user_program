using DotNetEnv;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace user_program{
    public class Net{
        public static async Task RunAsync()
        {
            string envPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
            if (!File.Exists(envPath))
            {
                MessageBox.Show(".env 파일을 찾을 수 없습니다.\n" + envPath);
                return;
            }
            Env.Load(envPath);
            string filePath = Env.GetString("FILEPATH", "default_filepath");
            string url = Env.GetString("URL", "default_url");
            string savePath = Env.GetString("RESULT_PATH", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv"));
            if (!File.Exists(filePath))
            {
                MessageBox.Show("업로드할 CSV 파일이 존재하지 않습니다.\n" + filePath);
                return;
            }

            using(var client = new HttpClient())
            using(var form = new MultipartFormDataContent())
            using(var fileStream = File.OpenRead(filePath))
            using (var fileContent = new StreamContent(fileStream))
            {
                client.Timeout = TimeSpan.FromMinutes(60);// 넉넉한 타임아웃 설정
                
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                form.Add(fileContent, "file", Path.GetFileName(filePath));
                HttpResponseMessage response = await client.PostAsync(url, form);
                if (response.IsSuccessStatusCode)
                {
                    var resultBytes = await response.Content.ReadAsByteArrayAsync();
                    File.WriteAllBytes(savePath, resultBytes);               
                    Console.WriteLine("파일 저장 완료: " + savePath);
                }
            }
        }
    }
}