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
    public partial class Main : Form
    {
        private CrmContext _db;

        public Main()
        {
            InitializeComponent();

            _db = new CrmContext();
        }

        private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(_db.Products, _db);
            catalogProduct.Show();
        }

        private void SellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(_db.Sellers, _db);
            catalogSeller.Show();
        }

        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCustomer = new Catalog<Customer>(_db.Customers, _db);
            catalogCustomer.Show();
        }

        private void CheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCheck = new Catalog<Check>(_db.Checks, _db);
            catalogCheck.Show();
        }

        private void CustomerAddToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _db.Customers.Add(form.Customer);
                _db.SaveChanges();
            }
        }

        private void SellerAddToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _db.Sellers.Add(form.Seller);
                _db.SaveChanges();
            }
        }

        private void ProductAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _db.Products.Add(form.Product);
                _db.SaveChanges();
            }
        }
    }
}
