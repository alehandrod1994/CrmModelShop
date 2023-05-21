using CrmBl.Model;
using CrmShopModel.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class SellerForm : Form
    {
        public SellerForm()
        {
            InitializeComponent();
        }

        public SellerForm(Seller seller) : this()
        {
            Seller = seller ?? new Seller();
            tbName.Text = Seller.Name;
        }

        public Seller Seller { get; private set; }

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

            Seller = Seller ?? new Seller();
            Seller.Name = tbName.Text;

            DialogResult = DialogResult.OK;
        }
    }
}
