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
        private Cart _cart;
        private Customer _customer;
        private CashDesk _cashDesk;

        public Main()
        {
            InitializeComponent();

            _db = new CrmContext();
            _cart = new Cart(_customer);
            _cashDesk = new CashDesk(1, _db.Sellers.FirstOrDefault(), _db)
            {
                IsModel = false
            };
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

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ModelForm();
            form.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                listBox1.Invoke((Action)delegate
                {
                    listBox1.Items.AddRange(_db.Products.ToArray());
                    UpdateLists();
                });
            });

        }

        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Product product)
            {
                _cart.Add(product);
                listBox2.Items.Add(product);
                UpdateLists();
            }
        }

        private void UpdateLists()
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(_cart.GetAll().ToArray());
            labelPrice.Text = "Итого: " + _cart.Price;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new LoginForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var tempCustomer = _db.Customers.FirstOrDefault(c => c.Name.Equals(form.Customer.Name));
                if (tempCustomer != null)
                {
                    _customer = tempCustomer;
                }
                else
                {
                    _db.Customers.Add(form.Customer);
                    _db.SaveChanges();
                    _customer = form.Customer;
                }

                _cart.Customer = _customer;

                linkLabel1.Text = "Здравствуй, сеньор " + _customer.Name;
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_customer != null)
            {
                _cashDesk.Enqueue(_cart);
                var price = _cashDesk.Dequeue();
                listBox2.Items.Clear();
                _cart = new Cart(_customer);

                MessageBox.Show("Покупка выполнена успешно. Сумма: " + price, "Покупка выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Авторизуйтесь, пожалуйста", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
