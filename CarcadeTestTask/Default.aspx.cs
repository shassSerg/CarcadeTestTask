﻿using CarcadeTestTask.Helpers;
using CarcadeTestTask.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarcadeTestTask
{
    public partial class _Default : Page
    {
        private readonly PaymentsHelper _paymentsHelper;

        public _Default()
        {
            _paymentsHelper = new PaymentsHelper(ConfigurationManager.ConnectionStrings["carcadeConnectionString"]?.ConnectionString);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Для контрола даты
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "$(function () { $(\".datepicker\").datepicker(); $(\".datepicker\").attr(\"autocomplete\", \"off\") });", true);

            if (!Page.IsPostBack)
            {
                LoadPayments();
            }
        }

        /// <summary>
        /// Устанавливает стрелки сортировки
        /// </summary>
        private void SetArrowsBySorting(SortDirection sortDirection, string sortExpression)
        {
            if (!string.IsNullOrWhiteSpace(sortExpression) &&
                customerPayments.HeaderRow?.Cells != null)
            {
                foreach (TableCell tc in customerPayments.HeaderRow.Cells)
                {
                    if (tc.HasControls())
                    {
                        var lnk = (LinkButton)tc.Controls[0];
                        if (lnk != null && sortExpression == lnk.CommandArgument)
                        {
                            var imageUrl = "~/img/icon_" + (sortDirection == SortDirection.Ascending ? "asc" : "desc") + ".png";

                            tc.Controls.Add(new LiteralControl(" "));
                            tc.Controls.Add(new Image
                            {
                                ImageUrl = imageUrl,
                                Width = 15,
                                Height = 15
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Получает настрйоки сортировки
        /// </summary>
        private (SortDirection SortDirection, string SortExpression) GetSortSetting()
        {
            var sortDirection = (SortDirection?)ViewState[nameof(SortDirection)] ?? SortDirection.Ascending;
            var sortExpression = ViewState[nameof(GridView.SortExpression)]?.ToString();

            return (SortDirection: sortDirection, SortExpression: sortExpression);
        }

        /// <summary>
        /// Загружает платежи
        /// </summary>
        private void LoadPayments()
        {
            // Загрузка данных
            DateTime? date = null;
            if (DateTime.TryParseExact(datePicker.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                date = parsedDate;
            }
            string nameCustomer = customerName.Text;

            var payments = _paymentsHelper.GetCustomerPayments(date, nameCustomer)
                .Select(payment => new
                {
                    // Сумма платежа
                    payment.Sum,
                    // Дата платежа
                    payment.PaymentDate,
                    // Контрагент
                    Customer = payment.Customer?.CustomerName
                });

            // Сортировка, также можно перенести в запрос, но если понадобится сортировать коллекцию в сессии, то придется доделывать
            // На относительно небольшой выборке данных, такой алгоритм уместен, но его нужно будет оптимизировать для большой выборки данных (переносить все SQL запрос (сортировку и пэйджинг))
            var sortSetting = GetSortSetting();
 
            if (!string.IsNullOrWhiteSpace(sortSetting.SortExpression) &&
                payments.Any())
            {
                var property = payments.First().GetType().GetProperty(sortSetting.SortExpression);

                if (property != null)
                {
                    payments = sortSetting.SortDirection == SortDirection.Ascending ?
                        payments.OrderBy(payment => property.GetValue(payment))
                        : payments.OrderByDescending(payment => property.GetValue(payment));
                }
            }

            // Установка Data Source
            payments = payments.ToList();

            // Уведомление, если пусто
            customerPaymentsEmpty.Text = !payments.Any() ? CommonResources.EmptyCustomerPayments : string.Empty;

            customerPayments.DataSource = payments;
            customerPayments.DataBind();
        }

        /// <summary>
        /// Смену индекса страницы можно реализовать в запросе (custompageindex)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void customerPayments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            customerPayments.PageIndex = e.NewPageIndex;
            LoadPayments();
        }

        protected void customerPayments_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState[nameof(SortDirection)] = ((SortDirection?)ViewState[nameof(SortDirection)] ?? SortDirection.Ascending) == SortDirection.Ascending && string.Equals(e.SortExpression, ViewState[nameof(GridView.SortExpression)]?.ToString()) ?
                SortDirection.Descending : SortDirection.Ascending;
            ViewState[nameof(GridView.SortExpression)] = e.SortExpression;
            LoadPayments();
        }

        protected void filterButton_Click(object sender, EventArgs e)
        {
            LoadPayments();
        }

        protected void customerPayments_DataBound(object sender, EventArgs e)
        {
            var sortSetting = GetSortSetting();

            SetArrowsBySorting(sortSetting.SortDirection, sortSetting.SortExpression);
        }
    }
}