﻿<%@ Page Title="Тестовое задание" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarcadeTestTask._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="customerPaymentsUpdatePanel" runat="server">
        <ContentTemplate>
            <div class="customers-payments-control">
                <asp:textbox id="datePicker" runat="server" cssclass="datepicker"/>
                <asp:textbox id="customerName" runat="server"/>
                <asp:Button ID="filterButton" Text="Отфильтровать" runat="server" OnClick="filterButton_Click" />
            </div>
            <asp:Label ID="customerPaymentsEmpty" runat="server" CssClass="customers-payments-empty" />
            <!-- Таблица с платежами -->
            <asp:GridView runat="server" 
                ID="customerPayments"
                AllowPaging="True" 
                AllowSorting="true"
                PageSize="7"
                AutoGenerateColumns="false"
                OnPageIndexChanging="customerPayments_PageIndexChanging"
                OnSorting="customerPayments_Sorting"
                GridLines="None"
                CellPadding="5"
                CellSpacing="5"
                BorderWidth="1px"
                CssClass="customer-payments">

                <RowStyle BackColor="#e4e4e4" BorderColor="Black" BorderWidth="1px" />
                <FooterStyle BackColor="#5a5a5a" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#5a5a5a" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5a5a5a" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />

                <Columns>
                    <asp:BoundField HeaderText="Контрагент" DataField="Customer" SortExpression="Customer" />
                    <asp:BoundField HeaderText="Платеж" DataField="Sum" SortExpression="Sum" />
                    <asp:BoundField HeaderText="Дата платежа" DataField="PaymentDate" SortExpression="PaymentDate" />
                </Columns>

            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
