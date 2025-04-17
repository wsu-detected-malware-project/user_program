using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using user_program.util;

namespace user_program.controller
{
    internal class Controller
    {
        public Form1 form1 = null;
        public Form2 form2 = null;
        public Form3 form3 = null;

        #region 싱글톤
        static public Controller Singleton { get; private set; }
        static Controller()
        {
            Singleton = new Controller();
        }
        #endregion

        #region Form1 라벨 수정
        //보안 상태가 안전합니다
        public void C_Security_State(string s_state)
        {
            Action<string> del = (Version) => { form1.Update_Version = s_state; };
            form1.Invoke(del, new object[] { s_state });
        }

        //최근 업데이트
        public void C_Recent_Update(string r_update)
        {
            Action<string> del = (Version) => { form1.Update_Version = r_update; };
            form1.Invoke(del, new object[] { r_update });
        }

        //최근 검사
        public void C_Recent_Test(string r_test)
        {
            Action<string> del = (Version) => { form1.Update_Version = r_test; };
            form1.Invoke(del, new object[] { r_test });
        }

        //현재 버전
        public void C_Update_Version_1(string u_version)
        {
            Action<string> del = (Version) => { form1.Update_Version = u_version; };
            form1.Invoke(del, new object[] { u_version });
        }
        #endregion

        #region Form2 버전
        //현재 버전
        public void C_Update_Version_2(string u_version)
        {
            Action<string> del = (Version) => { form2.Update_Version = u_version; };
            form2.Invoke(del, new object[] { u_version });
        }
        #endregion

        public void get_f_invest()
        {
            F_invest.F_Read();
        }

        public void get_p_invest()
        {
            P_invest.P_Read();
        }

        //Form2 실시간 검사 항목 출력
        public void C_Print_List(string msg)
        {
            // form2가 null일 때만 초기화
            if (form2 == null)
            {
                // Form2가 이미 열려있는지 확인
                form2 = Application.OpenForms.OfType<Form2>().FirstOrDefault();

                if (form2 == null)  // form2가 열린 인스턴스가 없다면 새로 생성
                {
                    form2 = new Form2();
                    Controller.Singleton.form2 = form2;
                    form2.Show();  // Form2를 화면에 표시
                }
            }

            // form2가 초기화되었으면 Print_List 호출
            form2.Print_List(msg);
        }

        //Form3 검사 내역 출력
        public void C_Test_Print_List(List<string> msg)
        {
            form3.Test_Print_List(msg);
        }

        public void C_Print_Image_Protect()
        {
            form3.LoadImage_Protect();
        }
        public void C_Print_Image_Malware()
        {
            form3.LoadImage_Malware();
        }

        public async Task<bool> network(){
            try
            {
                await Net.RunAsync();  // result.csv 저장까지 처리됨
                SaveMalwareStatusToEnv();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 통신 중 오류 발생: " + ex.Message);
                return false;
            }
        }
        
        public int testcode_c()
        {
            return F_invest.testcode();
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
