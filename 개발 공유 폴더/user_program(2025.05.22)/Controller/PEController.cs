using user_program.Invest;

namespace user_program.Controller {
    public class PEController {

        #region 싱글톤
        static public PEController Singleton { get; private set; }
        static PEController()
        {
            Singleton = new PEController();
        }
        #endregion
        
        public void get_f_invest() {
            F_invest.F_Read();
        }

        public void get_p_invest() {
            P_invest.P_Read();
        }        

        public int get_system_drive_totalfile() { 
            return F_invest.TotalFile();
        }

        public void get_d_invest() {
            D_invest.D_Read();
        }

        public int get_drive_totalfile() { 
            return D_invest.TotalFile();
        }

        public static List<String> Get_B_Invest_LastScan() {
            return B_invest.GetNewPEFilesSinceLastScan();
        }

        public static string Get_Analyze(List<string> peFiles) {
            return B_invest.AnalyzeAndSaveToCSV(peFiles);
        }

    }
}