using CrmBl.Model;

namespace CrmShopModel.BL.Model
{
	/// <summary>
	/// Команда.
	/// </summary>
    public interface ICommand
    {
		/// <summary>
		/// Добавить.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
        void Add(CrmContext db);
		
		/// <summary>
		/// Изменить.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
		/// <param name="id"> Идентификатор. </param>
        void Change(CrmContext db, int id);
		
		/// <summary>
		/// Удалить.
		/// </summary>
		/// <param name="db"> Контекст БД. </param>
		/// <param name="id"> Идентификатор. </param>
        void Remove(CrmContext db, int id);
    }
}
