using CrmBl.Model;
using CrmShopModel.BL.Model;
using CrmUi;
using System.Windows.Forms;

namespace CrmShopModel.UI.Commands
{
	/// <summary>
	/// Команды для товара.
	/// </summary>
    public class ProductCommand : BaseCommand, ICommand
    {
		/// <summary>
		/// Добавляет новый товар.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
        public void Add(CrmContext db)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                SaveChanges(db, "Данные успешно сохранены.");
            }
        }

		/// <summary>
		/// Изменяет данные товара.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
		/// <param name="id"> Идентификатор. </param>
        public void Change(CrmContext db, int id)
        {
            if (db.Products.Find(id) is Product product)
            {
                var form = new ProductForm(product);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SaveChanges(db, "Данные успешно изменены.");
                }
            }
        }

		/// <summary>
		/// Удаляет товар.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
		/// <param name="id"> Идентификатор. </param>
        public void Remove(CrmContext db, int id)
        {
            if (db.Products.Find(id) is Product product)
            {
                db.Products.Remove(product);
                SaveChanges(db, "Данные успешно удалены.");
            }
        }
    }
}
