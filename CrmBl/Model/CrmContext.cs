using System;
using System.Data.Entity;

namespace CrmBl.Model
{
	/// <summary>
	/// Контекст БД.
	/// </summary>
    public class CrmContext : DbContext
    {
        public CrmContext() : base("CrmConnection") { }
        
		/// <summary>
		/// Чеки.
		/// </summary>
        public DbSet<Check> Checks { get; set; }
		
		/// <summary>
		/// Покупатели.
		/// </summary>
        public DbSet<Customer> Customers { get; set; }

		/// <summary>
		/// Товары.
		/// </summary>
		public DbSet<Product> Products { get; set; }
		
		/// <summary>
		/// Продажи.
		/// </summary>
        public DbSet<Sell> Sells { get; set; }
		
		/// <summary>
		/// Продавцы.
		/// </summary>
        public DbSet<Seller> Sellers { get; set; }
    }
}
