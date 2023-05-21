using CrmBl.Model;
using System.Windows.Forms;

namespace CrmShopModel.UI.Commands
{
    public abstract class BaseCommand
    {
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
