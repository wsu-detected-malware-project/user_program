using func.Invest;

namespace func.Controller {
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
    

        public int get_system_drive_totalfile() { 
            return F_invest.TotalFile();
        }

    }
}