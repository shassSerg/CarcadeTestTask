using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarcadeTestTask.Models
{
    /// <summary>
    /// Модель платежа
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// ИД платежа
        /// </summary>
        [Key, Display(Name = "Платеж")]
        public Guid PaymentId { set; get; }

        /// <summary>
        /// Контрагент. Может быть пустым?
        /// </summary>
        [Required, Display(Name = "Контрагент")]
        public Customer Customer { set; get; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        [Required, Display(Name = "Дата платежа")]
        public DateTime PaymentDate { set; get; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [Required, Display(Name = "Сумма платежа")]
        public decimal Sum { set; get; }
    }
}