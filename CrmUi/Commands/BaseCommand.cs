using CrmBl.Model;
using System.Windows.Forms;

namespace CrmShopModel.UI.Commands
{
	/// <summary>
	/// Базовая команда.
	/// </summary>
    public abstract class BaseCommand
    {
		/// <summary>
		/// Сохраняет изменения в БД.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
		/// <param name="message"> Сообщение. </param>
        protected static void SaveChanges(CrmContext db, string message)
        {
            try 
            {
                db.SaveChanges();
                MessageBox.Show(message);
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить данные.");
            }
        }
    }
}
