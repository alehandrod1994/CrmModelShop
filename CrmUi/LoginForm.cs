using CrmBl.Model;
using System;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class LoginForm : Form
    {       
        public LoginForm()
        {
            InitializeComponent();
        }

        public Customer Customer { get; set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Customer = new Customer()
                {
                    Name = textBox1.Text
                };

                DialogResult = DialogResult.OK;
            }
        }
    }
}
