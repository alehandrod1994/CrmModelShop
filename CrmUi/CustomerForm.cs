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
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        public CustomerForm(Customer customer) : this()
        {
            Customer = customer ?? new Customer();
            textBox1.Text = Customer.Name;
        }

        public Customer Customer { get; private set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            Customer = Customer ?? new Customer()
            {
                Name = textBox1.Text
            };
           
            Close();
        }
    }
}
