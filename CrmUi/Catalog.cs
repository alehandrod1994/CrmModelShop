using CrmBl.Model;
using CrmShopModel.BL.Model;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class Catalog<T> : Form
		where T : class
	{
		private readonly CrmContext _db;
		private readonly DbSet<T> _set;
		private readonly ICommand _command;

		public Catalog(DbSet<T> set, CrmContext db)
		{
			InitializeComponent();

			_db = db;
			_set = set;

			_set.Load();
			dataGridView.DataSource = _set.Local.ToBindingList();

			if (typeof(T) == typeof(Check))
			{
				btnAdd.Enabled = false;
				btnChange.Enabled = false;
				btnRemove.Enabled = false;
			}
		}

		public Catalog(DbSet<T> set, CrmContext db, ICommand command) : this(set, db)
		{
			_command = command;
		}

		private void BtnAdd_Click(object sender, EventArgs e)
		{
			_command.Add(_db);
			dataGridView.Refresh();
		}

		private void BtnChange_Click(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
			_command.Change(_db, id);
			dataGridView.Refresh();
		}

		private void BtnRemove_Click(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
			_command.Remove(_db, id);
			dataGridView.Refresh();
		}
		
	}
}
