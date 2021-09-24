<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" EnableEventValidation="false" CodeBehind="AddStartUp.aspx.cs" Inherits="StartUpWebAPI.AddStartUp" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="BindingStartupsContent">
    <asp:Label runat="server" Text="Имя"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxName" Width="200" TextMode="SingleLine" Wrap="true"></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Категория"></asp:Label>
    <asp:DropDownList AutoPostBack="true" runat="server" ID="ComboCategories" Width="200"></asp:DropDownList>
    <br />
    <asp:Label runat="server" Text="Максимум участников"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxMaxMembers" Width="50" TextMode="Number" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Описание"></asp:Label>
    <asp:TextBox runat="server" ID="TBoxDescription" Width="200" Height="80"></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Изображения стартапа"></asp:Label>
    <br />
    <asp:CheckBox runat="server" Text="Завершён" ID="CheckBoxDone" Checked='<%# Bind("IsDone") %>' Visible="false"
        AutoPostBack="true"></asp:CheckBox>
    <br />
    <asp:ListView runat="server" ID="LViewImages" OnItemCommand="LViewImages_ItemCommand">
        <ItemTemplate>
            <div class="container-item startup-panel radius-like">
                <img class="startup-image radius-like image-cover-auto" src='<%# Eval("ImageInBase64") %>' alt='<%# Eval("Name") %>' />
                <h1 class="tag-item"><%# Eval("Name") %></h1>
                <asp:Button runat="server"
                    CssClass="tag-item round-div-block simple-cloud-button"
                    Style="width: 90%; height: max-content;"
                    Text="Удалить"
                    CommandName="RemoveImage"
                    CommandArgument='<%# Bind("Id") %>' ID="BtnRemoveImage"></asp:Button>
            </div>

        </ItemTemplate>
    </asp:ListView>
    <asp:FileUpload runat="server" AllowMultiple="true" ID="FileUploadImages" />
    <asp:Button runat="server" Text="Добавить изображения" ID="BtnAddImages"
        OnClick="BtnAddImages_Click" />
    <asp:Button runat="server" OnClick="BtnSave_Click" ID="BtnSave" Text="Сохранить изменения" />
    <asp:Button runat="server" OnClick="BtnCancel_Click" ID="BtnCancel" Text="Отмена" />
</asp:Content>
