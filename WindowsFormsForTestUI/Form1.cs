using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsForTestUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DealControl(null);  //NO.1 
            Thread th1 = new Thread(new ThreadStart(Th1_proc));
            th1.Start();
        }
        private void Th1_proc()
        {
            DealControl(null);  //NO.2 
        }
        private void DealControl(object[] args)
        {
            //… 
            if (InvokeRequired)  //NO.3 
            {
                Invoke((Action)delegate ()  //NO.4 
                {
                    DealControl(args);
                });
            }
            else
            {
                //access ui controls directly 
                //… 
            }
        }

    }
}
