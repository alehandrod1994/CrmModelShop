using CrmBl.Model;
using CrmShopModel.BL.Model;
using CrmUi;
using System.Windows.Forms;

namespace CrmShopModel.UI.Commands
{
    public class CustomerCommand : BaseCommand, ICommand
    {
        public void Add(CrmContext db)
        {
            var form = new CustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                SaveChanges(db, "Данные успешно сохранены.");
            }
        }

        public void Change(CrmContext db, int id)
        {
            if (db.Customers.Find(id) is Customer customer)
            {
                var form = new CustomerForm(customer);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SaveChanges(db, "Данные успешно изменены.");
                }
            }
        }

        public void Remove(CrmContext db, int id)
        {
            if (db.Customers.Find(id) is Customer customer)
            {
                db.Customers.Remove(customer);
                SaveChanges(db, "Данные успешно удалены.");
            }
        }
    }
}
