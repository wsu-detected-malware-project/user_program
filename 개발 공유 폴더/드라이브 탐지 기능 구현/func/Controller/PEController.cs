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
        
        public void get_d_invest() {
            D_invest.D_Read();
        }
    

        public int get_drive_totalfile() { 
            return D_invest.TotalFile();
        }

    }
}