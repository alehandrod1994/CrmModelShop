using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class Catalog<T> : Form
        where T : class
    {
        private CrmContext _db;
        private DbSet<T> _set;

        public Catalog(DbSet<T> set, CrmContext db)
        {
            InitializeComponent();

            _db = db;
            _set = set;

            _set.Load();
            dataGridView.DataSource = _set.Local.ToBindingList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (typeof(T) == typeof(Product))
            {
                //var form = new ProductForm();
                //if (form.ShowDialog() == DialogResult.OK)
                //{
                //    db.Products.Add(form.Product);
                //    db.SaveChanges();
                //}
            }
            else if (typeof(T) == typeof(Seller))
            {
                //var form = new SellerForm();
                //if (form.ShowDialog() == DialogResult.OK)
                //{
                //    db.Sellers.Add(form.Seller);
                //    db.SaveChanges();
                //}
            }
            else if (typeof(T) == typeof(Customer))
            {
                //var form = new ProductForm();
                //if (form.ShowDialog() == DialogResult.OK)
                //{
                //    db.Products.Add(form.Product);
                //    db.SaveChanges();
                //}
            }
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            var id = dataGridView.SelectedRows[0].Cells[0].Value;

            if (typeof(T) == typeof(Product))
            {
                if (_set.Find(id) is Product product)
                {
                    var form = new ProductForm(product);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        product = form.Product;
                        _db.SaveChanges();
                        dataGridView.Update();
                    }
                }
            }
            else if (typeof(T) == typeof(Seller))
            {
                if (_set.Find(id) is Seller seller)
                {
                    var form = new SellerForm(seller);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        seller = form.Seller;
                        _db.SaveChanges();
                        dataGridView.Update();
                    }
                }
            }
            else if (typeof(T) == typeof(Customer))
            {
                if (_set.Find(id) is Customer customer)
                {
                    var form = new CustomerForm(customer);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        customer = form.Customer;
                        _db.SaveChanges();
                        dataGridView.Update();
                    }
                }
            }
        }
    }
}
