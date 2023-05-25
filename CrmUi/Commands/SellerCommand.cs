using CrmBl.Model;
using CrmShopModel.BL.Model;
using CrmUi;
using System.Windows.Forms;

namespace CrmShopModel.UI.Commands
{
	/// <summary>
	/// Команды для продавца.
	/// </summary>
    public class SellerCommand : BaseCommand, ICommand
    {
		/// <summary>
		/// Добавляет нового продавца.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
        public void Add(CrmContext db)
        {
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.Seller);
                SaveChanges(db, "Данные успешно сохранены.");
            }
        }

		/// <summary>
		/// Изменяет данные продавца.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
		/// <param name="id"> Идентификатор. </param>
        public void Change(CrmContext db, int id)
        {
            if (db.Sellers.Find(id) is Seller seller)
            {
                var form = new SellerForm(seller);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SaveChanges(db, "Данные успешно изменены.");
                }
            }
        }

		/// <summary>
		/// Удаляет продавца.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
		/// <param name="id"> Идентификатор. </param>
        public void Remove(CrmContext db, int id)
        {
            if (db.Sellers.Find(id) is Seller seller)
            {
                db.Sellers.Remove(seller);
                SaveChanges(db, "Данные успешно удалены.");
            }
        }
    }
}
