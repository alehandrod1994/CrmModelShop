using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class ModelForm : Form
    {
        private readonly ShopComputerModel _model = new ShopComputerModel();
        public ModelForm()
        {
            InitializeComponent();          
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var cashBoxes = new List<CashBoxView>();

            for (int i = 0; i < _model.CashDesks.Count; i++)
            {
                var box = new CashBoxView(_model.CashDesks[i], i, 10, 26 * i);
                cashBoxes.Add(box);
                Controls.Add(box.CashDeskName);
                Controls.Add(box.Price);
                Controls.Add(box.QueueLength);
                Controls.Add(box.LeaveCustomersCount);
            }

            _model.Start();
        }

        private void ModelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _model.Stop();
        }

        private void ModelForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = _model.CustomerSpeed;
            numericUpDown2.Value = _model.CashDeskSpeed;
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _model.CustomerSpeed = (int)numericUpDown1.Value;
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            _model.CashDeskSpeed = (int)numericUpDown2.Value;
        }
    }
}
