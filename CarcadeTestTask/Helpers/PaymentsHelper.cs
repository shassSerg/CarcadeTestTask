using CarcadeTestTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarcadeTestTask.Helpers
{
    /// <summary>
    /// Класс для работы с платежами
    /// </summary>
    public class PaymentsHelper
    {
        /// <summary>
        /// Connection string to SQL DB
        /// </summary>
        private readonly string _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionToDb"></param>
        public PaymentsHelper(string connectionToDb)
        {
            if (string.IsNullOrWhiteSpace(connectionToDb))
            {
                throw new ArgumentNullException(nameof(connectionToDb));
            }

            _connection = connectionToDb;
        }

        /// <summary>
        /// Получает платежи контрагента
        /// </summary>
        /// <param name="date">Дата (необязательный)</param>
        /// <param name="nameCustomer">Наименование контрагента (необязательный)</param>
        public IEnumerable<Payment> GetCustomerPayments(DateTime? date = null, string nameCustomer = null)
        {
            return new List<Payment> {
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231253423,
                    PaymentDate = DateTime.Parse("19.08.2020 18:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Первый контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1153423,
                    PaymentDate = DateTime.Parse("12.08.2020 18:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Второй контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231112423,
                    PaymentDate = DateTime.Parse("12.08.2020 12:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Третий контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231212423,
                    PaymentDate = DateTime.Parse("12.08.2020 12:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Четвертый контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231233323,
                    PaymentDate = DateTime.Parse("15.08.2020 12:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Пятый контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231253423,
                    PaymentDate = DateTime.Parse("19.08.2020 18:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Первый контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1153423,
                    PaymentDate = DateTime.Parse("12.08.2020 18:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Второй контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231112423,
                    PaymentDate = DateTime.Parse("12.08.2020 12:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Третий контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231212423,
                    PaymentDate = DateTime.Parse("12.08.2020 12:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Четвертый контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231233323,
                    PaymentDate = DateTime.Parse("15.08.2020 12:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Пятый контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231253423,
                    PaymentDate = DateTime.Parse("19.08.2020 18:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Первый контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1153423,
                    PaymentDate = DateTime.Parse("12.08.2020 18:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Второй контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231112423,
                    PaymentDate = DateTime.Parse("12.08.2020 12:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Третий контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231212423,
                    PaymentDate = DateTime.Parse("12.08.2020 12:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Четвертый контрагент"
                    }
                },
                new Payment
                {
                    PaymentId = new Guid(),
                    Sum = 1231233323,
                    PaymentDate = DateTime.Parse("15.08.2020 12:00"),
                    Customer = new Customer
                    {
                        CustomerId = new Guid(),
                        CustomerName = "Пятый контрагент"
                    }
                }
            }.Where(payment => 
            {
                var result = true;

                if (date.HasValue)
                {
                    result = result && payment.PaymentDate.Date == date.Value.Date;
                }

                if (!string.IsNullOrWhiteSpace(nameCustomer))
                {
                    result = result && payment.Customer?.CustomerName?.ToLower()?.Contains(nameCustomer.ToLower()) == true;
                }

                return result;
            });

            //using (var sqlConnection = new SqlConnection(_connection))
            //{
            //    sqlConnection.Open();

            //    var query = $"SELECT * FROM Платежи as pay " +
            //        $"inner join Контрагент as cust on cust.CustomerId = pay.CustomerId where 1=1";

            //    if (date.HasValue)
            //    {
            //        query += $" and pay.{nameof(Payment.PaymentDate)}='{date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}'";
            //    }

            //    if (!string.IsNullOrWhiteSpace(nameCustomer))
            //    {
            //        query += $" and cust.{nameof(Customer.CustomerName)} like '%{nameCustomer}%'";

            //    }

            //    using (var command = new SqlCommand(query, sqlConnection))
            //    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            //    {
            //        while (reader.Read())
            //        {
            //            yield return new Payment
            //            {
            //                Sum = decimal.Parse(reader[nameof(Payment.Sum)]?.ToString()),
            //                PaymentDate = DateTime.Parse(reader[nameof(Payment.PaymentDate)]?.ToString()),
            //                PaymentId = Guid.Parse(reader[nameof(Payment.PaymentId)]?.ToString()),
            //                Customer = new Customer
            //                {
            //                    CustomerId = Guid.Parse(reader[nameof(Customer.CustomerId)]?.ToString()),
            //                    CustomerName = reader[nameof(Customer.CustomerName)]?.ToString()
            //                }
            //            };
            //        }
            //    }
            //}
        }
    }
}