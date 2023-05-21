using CrmBl.Model;
using CrmShopModel.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        public ProductForm(Product product) : this()
        {
            Product = product ?? new Product();
            tbName.Text = Product.Name;
            nudPrice.Value = Product.Price;
            nudCount.Value = Product.Count;
        }

        public Product Product { get; private set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox || control is NumericUpDown)
                {
                    control.BackColor = Color.White;
                }
            }

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

            Product = Product ?? new Product();
            Product.Name = tbName.Text;
            Product.Price = nudPrice.Value;
            Product.Count = Convert.ToInt32(nudCount.Value);

            DialogResult = DialogResult.OK;
        }
    }
}
