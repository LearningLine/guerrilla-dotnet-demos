using System;
using System.Windows.Forms;
using System.ServiceModel;

namespace DateClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
				label2.Text = TryLine(textBox1.Text);
        }
    }
}
