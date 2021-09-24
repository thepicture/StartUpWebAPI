<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddStartUp.aspx.cs" Inherits="StartUpWebAPI.AddStartUp" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="BindingStartupsContent">
    <asp:Label runat="server" Text="Имя"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxName" Width="200" TextMode="SingleLine" Wrap="true"></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Категория"></asp:Label>
    <asp:DropDownList AutoPostBack="true" runat="server" ID="ComboCategories" Width="200"></asp:DropDownList>
    <br />
    <asp:Label runat="server" Text="Максимум участников"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxMaxMembers" Width="50" TextMode="Number"></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Описание"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxDescription" Width="200" Height="80"></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Изображения стартапа"></asp:Label>
    <br />
    <asp:ListView runat="server" ID="LViewImages" OnItemCommand="LViewImages_ItemCommand">
        <ItemTemplate>
            <asp:Label runat="server" ID="LImageName" Text='<%# Bind("Name") %>'></asp:Label>
            <br />

            <div class="container-item startup-panel radius-like">
                <img class="startup-image radius-like" src='<%# Eval("ImageInBase64") %>' alt='<%# Eval("Name") %>' />
                <h1 class="tag-item"><%# Eval("Name") %></h1>
                <asp:Button runat="server"
                    CssClass="tag-item round-div-block simple-cloud-button"
                    Style="width: 90%; height: max-content;"
                    Text="Удалить"
                    CommandName="RemoveImage"
                    CommandArgument='<%# Bind("Id") %>' ID="BtnRemoveImage"
                    eve></asp:Button>
            </div>

        </ItemTemplate>
    </asp:ListView>
    <input id="StartUpImages" type="file" draggable="true" multiple runat="server" />
    <asp:Button runat="server" Text="Добавить изображения" ID="BtnAddImages" />
    <asp:Button runat="server" OnClick="BtnSave_Click" ID="BtnSave" Text="Сохранить изменения" />
    <asp:Button runat="server" OnClick="BtnCancel_Click" ID="BtnCancel" Text="Отмена" />
</asp:Content>
