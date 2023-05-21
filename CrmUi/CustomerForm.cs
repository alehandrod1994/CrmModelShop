using CrmBl.Model;
using CrmShopModel.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class CustomerForm : Form
    {
        public CustomerForm() 
        {
            InitializeComponent();
        }

        public CustomerForm(Customer customer) : this()
        {           
            Customer = customer ?? new Customer();
            tbName.Text = Customer.Name;
        }

        public Customer Customer { get; private set; }

        private void Button1_Click(object sender, EventArgs e)
        {
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

            Customer = Customer ?? new Customer();
            Customer.Name = tbName.Text;

            DialogResult = DialogResult.OK;
        }
    }
}
