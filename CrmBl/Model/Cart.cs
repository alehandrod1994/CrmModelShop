using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CrmBl.Model
{
	/// <summary>
	/// Корзина.
	/// </summary>
	public class Cart : IEnumerable
	{
		/// <summary>
		/// Инициализирует новый экземпляр класса Cart.
		/// </summary>
		/// <param name="customer"> Покупатель. </param>
		public Cart(Customer customer)
		{
			Customer = customer;
			Products = new Dictionary<Product, int>();
		}

		/// <summary>
		/// Покупатель.
		/// </summary>
		public Customer Customer { get; set; }
		
		/// <summary>
		/// Товары.
		/// </summary>
		public Dictionary<Product, int> Products { get; set; }
		
		/// <summary>
		/// Общая стоимость.
		/// </summary>
		public decimal Price => GetAll().Sum(p => p.Price);

		/// <summary>
		/// Добавляет товар.
		/// </summary>
		public void Add(Product product)
		{
			if (Products.TryGetValue(product, out int count))
			{
				Products[product] = ++count;
			}
			else
			{
				Products.Add(product, 1);
			}
		}

		/// <summary>
		/// Удаляет товар.
		/// </summary>
		/// <param name="product"> Товар. </param>
		public void Remove(Product product)
		{
			if (Products.TryGetValue(product, out int count))
			{
				if (Products[product] <= 1)
				{
					Products.Remove(product);
				}
				else
                {
					Products[product] = --count;
				}									
			}
		}

		public IEnumerator GetEnumerator()
		{
			foreach(var product in Products.Keys)
			{
				for (int i = 0; i < Products[product]; i++)
				{
					yield return product;
				}
			}
		}

		/// <summary>
		/// Получает список всех товаров.
		/// </summary>
		/// <returns> Список товаров. </returns>
		public List<Product> GetAll()
		{
			var result = new List<Product>();

			foreach (Product product in this)
			{
				result.Add(product);
			}

			return result;
		}
	}
}
