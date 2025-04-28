
using System.Windows.Forms;
using func;

namespace func.Controller{
    public class FormController{

        //임시로 Form1? 라고 수정.
        public Form1? form1 = null;

        #region 싱글톤
        static public FormController Singleton { get; private set; }
        static FormController()
        {
            Singleton = new FormController();
        }
        #endregion


        public void Print_Invest_List1(string msg){
            if (form1 == null)
            {
                form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();

                if (form1 == null)
                {
                    form1 = new Form1();
                    Singleton.form1 = form1;
                    form1.Show();
                }
            }
            ListBox listBox = form1.Give_Listbox();
            Form1_Print_List(form1, listBox, msg);
        }

        public void Form1_Print_List(Form1 parentForm, ListBox listbox, string msg) {
            parentForm.Show();
            listbox.Items.Add(msg);

            // 자동으로 스크롤을 맨 아래로 내리기
            listbox.SelectedIndex = -1;  // 선택된 항목을 없애기
            listbox.TopIndex = listbox.Items.Count - 1;  // 자동으로 스크롤을 맨 아래로 내리기
        }


    }
}