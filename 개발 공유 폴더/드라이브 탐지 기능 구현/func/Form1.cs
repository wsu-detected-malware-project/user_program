using System;
using System.IO;
using System.Management;
using System.Windows.Forms;

using func.Controller;
using func.Invest;


namespace func {
    public partial class Form1 : Form
    {
        private PEController PE_controller = PEController.Singleton;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PE_controller.get_d_invest();
        }


        public ListBox Give_Listbox()
        {
            return listBox1;
        }

    }
}
