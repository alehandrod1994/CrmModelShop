﻿using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
	/// <summary>
	/// Покупатель.
	/// </summary>
    public class Customer
    {
		/// <summary>
		/// Идентификатор.
		/// </summary>
        public int ID { get; set; }
		
		/// <summary>
		/// Имя.
		/// </summary>
        public string Name { get; set; }
		
		/// <summary>
		/// Коллекция чеков.
		/// </summary>
        public virtual ICollection<Check> Checks { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
