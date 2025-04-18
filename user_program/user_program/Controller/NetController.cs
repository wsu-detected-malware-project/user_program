using user_program.Util;
using user_program.Network;

namespace user_program.Controller {
    public class NetController {

        #region 싱글톤
        static public NetController Singleton { get; private set; }
        static NetController()
        {
            Singleton = new NetController();
        }
        #endregion
        public async Task<bool> network() {
            try {
                await Net.RunAsync();  // result.csv 저장까지 처리됨
                SaveMalwareStatusToEnv();
                return true;
            }
            catch (Exception ex) {
                MessageBox.Show("서버 통신 중 오류 발생: " + ex.Message);
                return false;
            }
        }

        private void SaveMalwareStatusToEnv()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv");
            if (!File.Exists(path)) return;

            string[] lines = File.ReadAllLines(path);
            bool hasMalware = lines.Length > 1; // 헤더 외에 한 줄 이상 있으면 악성 있음

            EnvManager.SetMalwareStatus(hasMalware);
        }
    }
}