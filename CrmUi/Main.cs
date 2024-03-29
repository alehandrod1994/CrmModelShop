﻿using CrmBl.Model;
using CrmShopModel.BL.Model;
using CrmShopModel.UI.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
	public partial class Main : Form
	{
		private readonly CrmContext _db;
		private readonly CashDesk _cashDesk;
		private Cart _cart;
		private Customer _customer;		
		private ICommand _command;

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
			_command = new ProductCommand();
			var catalogProduct = new Catalog<Product>(_db.Products, _db, _command);
			catalogProduct.Show();
		}

		private void SellerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_command = new SellerCommand();
			var catalogSeller = new Catalog<Seller>(_db.Sellers, _db, _command);
			catalogSeller.Show();
		}

		private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_command = new CustomerCommand();
			var catalogCustomer = new Catalog<Customer>(_db.Customers, _db, _command);
			catalogCustomer.Show();
		}

		private void CheckToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var catalogCheck = new Catalog<Check>(_db.Checks, _db);
			catalogCheck.Show();
		}

		private void CustomerAddToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			_command = new CustomerCommand();
			_command.Add(_db);
		}

		private void SellerAddToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			_command = new SellerCommand();
			_command.Add(_db);
		}

		private void ProductAddToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_command = new ProductCommand();
			_command.Add(_db);
		}

		private void ModelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new ModelForm();
			form.Show();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				listBoxProducts.Invoke((Action)delegate
				{
					listBoxProducts.Items.AddRange(_db.Products.ToArray());
					UpdateLists();
				});
			});

		}

		private void ListBox1_DoubleClick(object sender, EventArgs e)
		{
			if (listBoxProducts.SelectedItem is Product product)
			{
				_cart.Add(product);
				UpdateLists();
			}
		}

		private void ListBoxCart_DoubleClick(object sender, EventArgs e)
		{
			if (listBoxCart.SelectedItem is Product product)
			{
				_cart.Remove(product);
				UpdateLists();
			}
		}

		private void UpdateLists()
		{
			listBoxCart.Items.Clear();
			listBoxCart.Items.AddRange(_cart.GetAll().ToArray());
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

				linkLabel1.Text = "Здравствуйте, " + _customer.Name;
			}            
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			if (_customer != null)
			{
				_cashDesk.Enqueue(_cart);
				var price = _cashDesk.Dequeue();
				listBoxCart.Items.Clear();
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
