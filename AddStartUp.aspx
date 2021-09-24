<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddStartUp.aspx.cs" Inherits="StartUpWebAPI.AddStartUp" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="BindingStartupsContent">
    <asp:Label runat="server" Text="Имя"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxName" Width="200" TextMode="SingleLine"></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Категория"></asp:Label>
    <asp:DropDownList runat="server" ID="ComboCategories" Width="200"></asp:DropDownList>
    <br />
    s
    <asp:Label runat="server" Text="Максимум участников"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxMaxMembers" Width="50" TextMode="Number"></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Описание"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxDescription" Width="200" Height="80"></asp:TextBox>
    <br />
    <asp:Button runat="server" OnClick="BtnSave_Click" ID="BtnSave" Text="Сохранить изменения" />
    <asp:Button runat="server" OnClick="BtnCancel_Click" ID="BtnCancel" Text="Отмена" />
</asp:Content>
