using System;
using System.Drawing;
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

	        bool success = label2.Text.ToLower().Contains("[success]");
			label2.BackColor = success ? Color.LightGreen : Color.LightPink;
        }
    }
}
