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
    public partial class ProductForm : Form
    {        
        public ProductForm()
        {
            InitializeComponent();
        }

        public ProductForm(Product product) : this()
        {
            Product = product;
            textBox1.Text = Product.Name;
            numericUpDown1.Value = Product.Price;
            numericUpDown2.Value = Product.Count;
        }

        public Product Product { get; private set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            var p = Product ?? new Product();
            p.Name = textBox1.Text;
            p.Price = numericUpDown1.Value;
            p.Count = Convert.ToInt32(numericUpDown2.Value);
           
            Close();
        }
    }
}
