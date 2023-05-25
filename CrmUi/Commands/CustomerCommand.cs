using CrmBl.Model;
using CrmShopModel.BL.Model;
using CrmUi;
using System.Windows.Forms;

namespace CrmShopModel.UI.Commands
{
	/// <summary>
	/// Команды для покупателя.
	/// </summary>
    public class CustomerCommand : BaseCommand, ICommand
    {
		/// <summary>
		/// Добавляет нового покупателя.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
        public void Add(CrmContext db)
        {
            var form = new CustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                SaveChanges(db, "Данные успешно сохранены.");
            }
        }

		/// <summary>
		/// Изменяет данные покупателя.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
		/// <param name="id"> Идентификатор. </param>
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

		/// <summary>
		/// Удаляет покупателя.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
		/// <param name="id"> Идентификатор. </param>
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
