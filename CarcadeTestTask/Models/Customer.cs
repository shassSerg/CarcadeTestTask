using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarcadeTestTask.Models
{
    /// <summary>
    /// Модель контрагента
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// ИД контрагента
        /// </summary>
        [Key, Display(Name = "Контрагент")]
        public Guid CustomerId { set; get; }

        /// <summary>
        /// Наименование контрагента
        /// </summary>
        [Required, StringLength(100), Display(Name = "Наименование контрагента")]
        public string CustomerName { set; get; } 
    }
}