using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
	/// <summary>
	/// Чек.
	/// </summary>
    public class Check
    {
		/// <summary>
		/// Идентификатор.
		/// </summary>
        public int ID { get; set; }
		
		/// <summary>
		/// Идентификатор покупателя.
		/// </summary>
        public int CustomerID { get; set; }
		
		/// <summary>
		/// Покупатель.
		/// </summary>
        public virtual Customer Customer { get; set; }
		
		/// <summary>
		/// Идентификатор продавца.
		/// </summary>
        public int SellerID { get; set; }
		
		/// <summary>
		/// Продавец.
		/// </summary>
        public virtual Seller Seller { get; set; }
		
		/// <summary>
		/// Дата.
		/// </summary>
        public DateTime Created { get; set; }
		
		/// <summary>
		/// Коллекция продаж.
		/// </summary>
        public virtual ICollection<Sell> Sells { get; set; }
		
		/// <summary>
		/// Стоимость.
		/// </summary>
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"№{ID} от {Created:dd.MM.yy hh:mm:ss}";
        }
    }
}
