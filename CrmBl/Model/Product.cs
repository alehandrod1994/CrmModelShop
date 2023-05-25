using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
    /// <summary>
    /// Товар.
    /// </summary>
    public class Product
    {
		/// <summary>
		/// Идентификатор.
		/// </summary>
        public int ID { get; set; }
		
		/// <summary>
		/// Название.
		/// </summary>
        public string Name { get; set; }
		
		/// <summary>
		/// Стоимость.
		/// </summary>
        public decimal Price { get; set; }
		
		/// <summary>
		/// Количество.
		/// </summary>
        public int Count { get; set; }
		
		/// <summary>
		/// Коллекция продаж.
		/// </summary>
        public virtual ICollection<Sell> Sells { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price}";
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public override bool Equals(object obj)
        {
            if (obj is Product product)
            {
                return ID.Equals(product.ID);
            }

            return false;
        }
    }
}
