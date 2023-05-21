using CrmBl.Model;
using CrmShopModel.BL.Model;
using CrmUi;
using System.Windows.Forms;

namespace CrmShopModel.UI.Commands
{
    public class ProductCommand : BaseCommand, ICommand
    {
        public void Add(CrmContext db)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                SaveChanges(db, "Данные успешно сохранены.");
            }
        }

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
