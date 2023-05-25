using System;

namespace CrmBl.Model
{
	/// <summary>
	/// Продажа.
	/// </summary>
    public class Sell
    {
		/// <summary>
		/// Идентификатор.
		/// </summary>
        public int ID { get; set; }
		
		/// <summary>
		/// Идентификатор чека.
		/// </summary>
        public int CheckID { get; set; }

		/// <summary>
		/// Идентификатор товара.
		/// </summary>
		public int ProductID { get; set; }
		
		/// <summary>
		/// Чек.
		/// </summary>
        public virtual Check Check { get; set; }

		/// <summary>
		/// Товар.
		/// </summary>
		public virtual Product Product { get; set; }
    }
}
