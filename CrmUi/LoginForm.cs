using CrmBl.Model;
using CrmShopModel.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            #region Проверка контролов
            List<Control> controls = Checker.CheckControlsOnNull(Controls);
            if (controls.Count > 0)
            {
                foreach (Control control in controls)
                {
                    control.BackColor = Color.LightCoral;
                }

                MessageBox.Show("Заполните все поля.");
                return;
            }
            #endregion

            Customer = new Customer()
            {
                Name = textBox1.Text
            };

            DialogResult = DialogResult.OK;           
        }
    }
}
