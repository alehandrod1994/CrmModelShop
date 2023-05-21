using CrmBl.Model;
using CrmShopModel.BL.Model;
using CrmUi;
using System.Windows.Forms;

namespace CrmShopModel.UI.Commands
{
    public class SellerCommand : BaseCommand, ICommand
    {
        public void Add(CrmContext db)
        {
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.Seller);
                SaveChanges(db, "Данные успешно сохранены.");
            }
        }

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
