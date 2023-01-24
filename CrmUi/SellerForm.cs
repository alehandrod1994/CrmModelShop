using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Seller = seller;
            textBox1.Text = Seller.Name;
        }

        public Seller Seller { get; private set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            Seller = Seller ?? new Seller()
            {
                Name = textBox1.Text
            };
                  
            Close();
        }
    }
}
