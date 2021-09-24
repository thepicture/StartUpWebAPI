<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddStartUp.aspx.cs" Inherits="StartUpWebAPI.AddStartUp" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label runat="server" Text="Имя"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxName" Width="200" TextMode="SingleLine" Text='<%# Bind("Name") %>'></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Категория"></asp:Label>
    <asp:DropDownList runat="server" ID="ComboCategories" Width="200" Text='<%# Bind("Category") %>'></asp:DropDownList>
    <br />
    <asp:Label runat="server" Text="Максимум участников"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxMaxMembers" Width="50" TextMode="Number" Text='<%# Bind("CountOfMembers") %>'></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Описание"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxDescription" Width="200" Height="80" Text='<%# Bind("Description") %>'></asp:TextBox>
</asp:Content>
