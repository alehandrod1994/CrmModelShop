using CrmBl.Model;

namespace CrmShopModel.BL.Model
{
    public interface ICommand
    {
        void Add(CrmContext db);
        void Change(CrmContext db, int id);
        void Remove(CrmContext db, int id);
    }
}
