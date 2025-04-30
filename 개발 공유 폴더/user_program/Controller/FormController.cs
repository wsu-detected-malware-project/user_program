using DotNetEnv;
using System.Windows.Forms;
using user_program.FormAll;

namespace user_program.Controller{
    public class FormController{

        public Form1 form1 = null;
        public Form2 form2 = null;
        public Form3 form3 = null;
        public Form3 FormSetting = null;
        #region 싱글톤
        static public FormController Singleton { get; private set; }
        static FormController()
        {
            Singleton = new FormController();
        }
        #endregion

        #region Form1 라벨 수정
        //보안 상태가 안전합니다
        public void Security_State(string s_state) {
            Action<string> del = (Version) => { form1.Update_Version = s_state; };
            form1.Invoke(del, new object[] { s_state });
        }

        //최근 업데이트
        public void Recent_Update(string r_update) {
            Action<string> del = (Version) => { form1.Update_Version = r_update; };
            form1.Invoke(del, new object[] { r_update });
        }

        //최근 검사
        public void Recent_Test(string r_test) {
            Action<string> del = (Version) => { form1.Update_Version = r_test; };
            form1.Invoke(del, new object[] { r_test });
        }

        //현재 버전
        public void Update_Version_1(string u_version) {
            Action<string> del = (Version) => { form1.Update_Version = u_version; };
            form1.Invoke(del, new object[] { u_version });
        }
        #endregion

        #region Form2 버전
        //현재 버전
        public void Update_Version_2(string u_version) {
            Action<string> del = (Version) => { form2.Update_Version = u_version; };
            form2.Invoke(del, new object[] { u_version });
        }
        #endregion
        #region form1
        //-------------------------------------form1-------------------------------------
        public static void UpdateRealTimeScanStatus(bool isOn){
            Form1.UpdateRealTimeScanStatus(isOn);
        }

        private static Form1 mainForm;

        public static void SetMainForm(Form1 form)
        {
            mainForm = form;
        }

        public static void BringFormToFront()
        {
            if (mainForm != null)
            {
                mainForm.Invoke(new Action(() =>
                {
                    mainForm.WindowState = FormWindowState.Normal;
                    mainForm.BringToFront();
                    mainForm.Activate();
                }));
            }
        }
        #endregion

        #region form2
        //-------------------------------------form2-------------------------------------
        public void Form1_button1(Form1 parentForm){

            parentForm.Hide();
            Form2 form2 = new Form2("f_invest");
            form2.ShowDialog();
            parentForm.Show();

        }

        public void Form1_button2(Form1 parentForm){

            parentForm.Hide();
            Form2 form2 = new Form2("p_invest");
            form2.ShowDialog();
            parentForm.Show();

        }
        public void Print_Invest_List1(string msg)
        {
            if (form2 == null)
            {
                form2 = Application.OpenForms.OfType<Form2>().FirstOrDefault();

                if (form2 == null)
                {
                    form2 = new Form2();
                    Singleton.form2 = form2;
                    form2.Show();
                }
            }
            ListBox listBox = form2.Give_Listbox();
            ProgressBar progressBar = form2.Give_progressBar();
            Form2_Print_List(form2, listBox, progressBar, msg);
        }
        public void Form2_Print_List(Form2 parentForm, ListBox listbox, ProgressBar progressBar, string msg) {
            parentForm.Show();
            listbox.Items.Add(msg);

            // 자동으로 스크롤을 맨 아래로 내리기
            listbox.SelectedIndex = -1;  // 선택된 항목을 없애기
            listbox.TopIndex = listbox.Items.Count - 1;  // 자동으로 스크롤을 맨 아래로 내리기

            // 로딩바 값 증가
            progressBar.Value = listbox.Items.Count;  // 리스트박스에 아이템 갯수만큼 증가
        }
        

        #endregion
        #region form3
        //-------------------------------------form3-------------------------------------
        public void Print_Invest_List2(Form3 parentForm, ListBox listbox, List<string> msg) {
            Form_Print_List(parentForm, listbox, msg);
        }

        public void Form_Print_List(Form3 parentForm, ListBox listBox, List<string> msg) {
            parentForm.Show();

            foreach (var name in msg) {
                listBox.Items.Add(name); 
            }    
        }

        public void Print_Image_Shield1(PictureBox picturebox) {
            LoadImage_Shield1(picturebox);
        }
        public void Print_Image_Shield2(PictureBox picturebox, Form3 form, ListBox listbox, List<string> malwareNames)
        {
            LoadImage_Shield2(picturebox);
            Print_Invest_List2(form, listbox, malwareNames);
        }
        public void LoadImage_Shield1(PictureBox picturebox) {
            string imagePath = Env.GetString("SHIELD_IMAGE1", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "shield1.jpg"));

            picturebox.Image = System.Drawing.Image.FromFile(imagePath);

        }
        public void LoadImage_Shield2(PictureBox picturebox) {
            string imagePath = Env.GetString("SHIELD_IMAGE2", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "shield1.jpg"));

            picturebox.Image = System.Drawing.Image.FromFile(imagePath);

        }

        public void Form3_Close_After(object sender, FormClosingEventArgs e) {
            Singleton.form1.UpdateScanInfoUI();
            Singleton.form1.Show();
        }
        #endregion
    }
}