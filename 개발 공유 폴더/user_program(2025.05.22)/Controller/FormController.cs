using DotNetEnv;
using System.Windows.Forms;
using user_program.FormAll;
using System.Text; 

namespace user_program.Controller{
    public class FormController{

        public Form1 form1 = null;
        public Form2 form2 = null;
        public Form3 form3 = null;
        public FormSetting formSetting = null;
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

        //최근 업데이트 ---------- 날짜
        public void Recent_Update(string r_update) {
            Action<string> del = (Version) => { form1.Recent_Update = r_update; };
            form1.Invoke(del, new object[] { r_update });
        }
        //최근 업데이트 ---------- 
        public void Recent_Update2(string r_update) {
            Action<string> del = (Version) => { form1.Recent_Update2 = r_update; };
            form1.Invoke(del, new object[] { r_update });
        }

        //최근 검사 -------------- 시간
        public void Recent_Test(string r_test) {
            Action<string> del = (Version) => { form1.Recent_Test = r_test; };
            form1.Invoke(del, new object[] { r_test });
        }
        //최근 검사 -------------- 
        public void Recent_Test2(string r_test) {
            Action<string> del = (Version) => { form1.Recent_Test2 = r_test; };
            form1.Invoke(del, new object[] { r_test });
        }
        //현재 버전
        public void Update_Version_1(string u_version) {
            Action<string> del = (Version) => { form1.Update_Version = u_version; };
            form1.Invoke(del, new object[] { u_version });
        }
        #endregion

        #region 언어 번역
        //빠른 검사 버튼
        public void Fest_Button(string button_text) {
            Action<string> del = (Button_text) => { form1.Button_Text = button_text; };
            form1.Invoke(del, new object[] { button_text });
        }
        //정밀 검사 버튼
        public void Pull_Button(string button_text2) {
            Action<string> del = (Button_text2) => { form1.Button_Text2 = button_text2; };
            form1.Invoke(del, new object[] { button_text2 });
        }
        //실시간 검사 on/off
        public void Real_Time_OnOffSwitch(string switch_text) {
            Action<string> del = (Switch_text) => { form1.Real_Time_Switch = switch_text; };
            form1.Invoke(del, new object[] { switch_text });
        }
        //버전 체크
        public void Version_Check(string version) {
            Action<string> del = (Version) => { form1.Version_Check = version; };
            form1.Invoke(del, new object[] { version });
        }

        

        //세팅폼 실시간 검사 on/off
        public void Real_Time_OnOffSwitch_Set(string switch_text_set) {
            Action<string> del = (Switch_text_set) => { formSetting.Real_Time_Switch_Set = switch_text_set; };
            formSetting.Invoke(del, new object[] { switch_text_set });
        }

        //세팅폼 실시간 검사 주기 설정
        public void Real_Time_Cycle_Set(string time_cycle_set) {
            Action<string> del = (Time_cycle_set) => { formSetting.Real_Time_Cycle_Set = time_cycle_set; };
            formSetting.Invoke(del, new object[] { time_cycle_set });
        }

        //세팅폼 검색 제외 항목
        public void WhiteList_Data_Set(string whitelist_data_set) {
            Action<string> del = (Whitelist_data_set) => { formSetting.Whitelist_Data_set = whitelist_data_set; };
            formSetting.Invoke(del, new object[] { whitelist_data_set });
        }

        //세팅폼 업데이트 라벨
        public void Update_Label_Set(string update_label_set) {
            Action<string> del = (Update_label_set) => { formSetting.Update_Label_Set = update_label_set; };
            formSetting.Invoke(del, new object[] { update_label_set });
        }

        //세팅폼 업데이트 확인 버튼
        public void Update_Check_Set(string update_check_set) {
            Action<string> del = (Update_check_set) => { formSetting.Update_Check_Set = update_check_set; };
            formSetting.Invoke(del, new object[] { update_check_set });
        }

        //세팅폼 언어 라벨 - 그냥 영어로 통일 = 미사용
        public void Language_Set(string language_set) {
            Action<string> del = (Language_set) => { formSetting.Language_Set = language_set; };
            formSetting.Invoke(del, new object[] { language_set });
        }
        //세팅폼 저장 버튼
        public void Save_Button_Set(string save_button_text_set) {
            Action<string> del = (Save_button_text_set) => { formSetting.Save_Button = save_button_text_set; };
            formSetting.Invoke(del, new object[] { save_button_text_set });
        }
        //세팅폼 닫기 버튼
        public void Close_Button_Set(string close_button_text_set) {
            Action<string> del = (Close_button_text_set) => { formSetting.Close_Button = close_button_text_set; };
            formSetting.Invoke(del, new object[] { close_button_text_set });
        }

        #endregion

        #region Form2 버전 라벨
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
        
        public static void Form2_drive_search(){
            Form2 form2 = new Form2("d_invest");
            form2.ShowDialog();
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
        

        public void Form2_Print_List(Form2 parentForm, ListBox listbox, ProgressBar progressBar, string msg)
        {
            // 자동으로 스크롤을 맨 아래로 내리기
            parentForm.Show(); // 선택된 항목을 없애기
            listbox.Items.Add(msg); // 자동으로 스크롤을 맨 아래로 내리기

            listbox.SelectedIndex = -1;
            listbox.TopIndex = listbox.Items.Count - 1;

            progressBar.Value = listbox.Items.Count;  // 리스트박스에 아이템 갯수만큼 증가

            // 예외 방지: 최대값 넘을 경우 증가시키지 않음
            if (listbox.Items.Count <= progressBar.Maximum)
            {
                progressBar.Value = listbox.Items.Count;
            }
            else
            {
                // 넘었으면 그냥 최대값까지만 고정
                progressBar.Value = progressBar.Maximum;
            }
        }

        public void Form2_Prograss_Last()
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
            ProgressBar progressBar = form2.Give_progressBar();
            Form2_Prograss_Last1(form2, progressBar);
        }

        public void Form2_Prograss_Last1(Form2 parentForm, ProgressBar progressBar)
        {
            // 이 시점에서 form2와 progressBar를 다시 가져와서 보정
            parentForm.Show();

            if (progressBar.Value < progressBar.Maximum)
            {
                progressBar.Value = progressBar.Maximum;
            }

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
    
        #region FormSetting
        //콤보박스에 language.csv 파일에서 언어 칼럼만 넣어주는 함수
        public void Combobox_print_language(ComboBox comboBox_language)
        {
            string filePath = Env.GetString("LANGUAGE", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "language.csv"));
            string savedLanguage = Environment.GetEnvironmentVariable("COMBOBOX_LANGUAGE") ?? "Korean";

            using (var reader = new StreamReader(filePath))
            {
                bool isHeader = true;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (isHeader)
                    {
                        isHeader = false;
                        continue; // 첫 줄(헤더)은 건너뜀
                    }

                    var parts = line.Split(',');

                    if (parts.Length >= 1)
                    {
                        string language = parts[0].Trim();
                        comboBox_language.Items.Add(language);
                    }
                }
            }

            // 저장된 언어가 존재하면 그걸 선택
            if (comboBox_language.Items.Contains(savedLanguage))
            {
                comboBox_language.SelectedItem = savedLanguage;
            }
            else
            {
                comboBox_language.SelectedIndex = 0;
            }

        }

        //env에 콤보박스에서 클릭한 데이터 저장
        public void SaveLanguageToEnv(ComboBox comboBox_language)
        {
            string selectedLanguage = comboBox_language.SelectedItem.ToString();

            string envPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");

            var lines = File.Exists(envPath)
                ? File.ReadAllLines(envPath).ToList()
                : new List<string>();

            bool found = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("COMBOBOX_LANGUAGE="))
                {
                    lines[i] = $"COMBOBOX_LANGUAGE={selectedLanguage}";
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                lines.Add($"COMBOBOX_LANGUAGE={selectedLanguage}");
            }

            File.WriteAllLines(envPath, lines);
        }

        public void UpdateFormLabelsFromCsv(string language)
        {
            string filePath = Env.GetString("LANGUAGE", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "language.csv"));
  
            using (var reader = new StreamReader(filePath, Encoding.UTF8)) 
            {
                bool isHeader = true;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    var parts = line.Split(',');
                    if (parts.Length >= 12 && parts[0].Trim() == language)
                    {
                        Fest_Button(parts[1].Trim()); // 빠른 검사 버튼
                        Pull_Button(parts[2].Trim()); // 정밀 검사 버튼
                        Real_Time_OnOffSwitch(parts[3].Trim()); // 실시간 검사 라벨(리치 텍스트 박스)
                        Recent_Update2(parts[4].Trim()); // 최근 업데이트 라벨                        
                        Recent_Test2(parts[5].Trim()); // 마지막 검사 라벨
                        Version_Check(parts[6].Trim()); //버전

                        // 폼 객체가 null인지 체크 후 실행
                        if (formSetting != null)
                        {
                            Real_Time_OnOffSwitch_Set(parts[7].Trim()); // 실시간 검사 on/off        
                            Real_Time_Cycle_Set(parts[8].Trim()); // 실시간 검사 주기 설정
                            WhiteList_Data_Set(parts[9].Trim()); // 검색 제외 항목                    
                            Update_Label_Set(parts[10].Trim()); // 업데이트
                            Update_Check_Set(parts[11].Trim()); // 업데이트 확인                        
                            Save_Button_Set(parts[12].Trim()); // 저장 버튼
                            Close_Button_Set(parts[13].Trim()); // 닫기 버튼
                        }
                        break;
                    }
                }
            }
        }

        #endregion    
       
    }
}