<%@ Page EnableEventValidation="false" Title="Добавление стартапа" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddStartUp.aspx.cs" Inherits="StartUpWebAPI.AddStartUp" EnableViewState="true" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="BindingStartupsContent">
    <div style="display: flex; justify-content: center; align-items: center; vertical-align: middle;">
        <div class="round-div-block semi-transparent-register-like-two request-auto-height"
            style="width: 740px; animation: none !important; display: inline-block;">
            <br />

            <asp:Label runat="server" Style="font-size: 20px; margin-left: 70px; margin-right: 20px;" Text="Имя"></asp:Label>
            <asp:TextBox runat="server" ID="TBoxName" Style="width: 515px !important; max-width: 700px; color: black !important;" CssClass="nice-text-box prevent-selection textbox-style-setting" TextMode="SingleLine" Wrap="true"></asp:TextBox>
            <br />
            <br />
            <asp:Label runat="server" Style="font-size: 20px; margin-left: 70px; margin-right: 20px; float: left !important;" Text="Категория"></asp:Label>
            <asp:DropDownList AutoPostBack="true" runat="server" class="form-control" Style="color: black !important; width: 455px !important; float: unset !important;" ID="ComboCategories" Width="200"></asp:DropDownList>
            <br />
            <asp:Label runat="server" Style="font-size: 20px; margin-left: 70px; margin-right: 20px;" Text="Максимум участников"></asp:Label>
            <asp:TextBox runat="server" ID="TBoxMaxMembers" Style="color: black !important; width: 330px !important;" CssClass="nice-text-box prevent-selection textbox-style-setting" Width="50" TextMode="Number" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
            <br />
            <br />
            <asp:Label runat="server" Style="font-size: 20px; margin-left: 70px; margin-right: 20px;" Text="Описание"></asp:Label>
            <asp:TextBox runat="server" ID="TBoxDescription" Style="color: black !important; width: 457px !important;" CssClass="nice-text-box prevent-selection textbox-style-setting" Width="200" Height="80"></asp:TextBox>
            <br />
            <br />

            <asp:Label runat="server" Style="font-size: 20px; margin-left: 70px; margin-right: 20px;" Text="Изображения стартапа"></asp:Label>
            <br />
            <asp:CheckBox runat="server" Text="Завершён" ID="CheckBoxDone" Checked='<%# Bind("IsDone") %>' Visible="false"
                AutoPostBack="true"></asp:CheckBox>
            <br />
            <%-- ListView for demonstrating attached images if any. --%>
            <asp:ListView runat="server" ID="LViewImages" OnItemCommand="LViewImages_ItemCommand">
                <ItemTemplate>
                    <div class="container-item startup-panel radius-like" style="margin-left: 70px !important; width: 250px !important; height: 150px !important;">
                        <img class="startup-image radius-like image-cover-auto" src='<%# Eval("ImageInBase64") %>' alt='<%# Eval("Name") %>' />

                        <h1 class="tag-item" style="margin-bottom: 100px; margin-left: 60px; width: 150px; height: 50px;"><%# Eval("Name") %></h1>

                        <asp:Button runat="server"
                            CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page-four margin-bottom-top-as-usual"
                            Style="width: 90%; height: max-content;"
                            Text="Удалить"
                            CommandName="RemoveImage"
                            CommandArgument='<%# Bind("Name") %>' ID="BtnRemoveImage"></asp:Button>
                        <br />
                    </div>
                </ItemTemplate>
            </asp:ListView>

            <asp:FileUpload runat="server"
                Style="margin-bottom: 0px !important; font-size: 17px; margin-left: 70px; margin-right: 20px;"
                AllowMultiple="true"
                ID="FileUploadImages" />

            <asp:Button runat="server"
                Style="margin-bottom: 10px !important; font-size: 17px !important; margin-left: 70px; height: 35px !important; border-radius: 5px !important; color: black !important;"
                CssClass="round-div-block-specified-about about-like-cloud-button-three"
                Text="Добавить изображения"
                ID="BtnAddImages"
                OnClick="BtnAddImages_Click" />

            <asp:Label runat="server" Style="font-size: 20px; margin-bottom: 10px !important; margin-left: 70px; margin-right: 20px;" Text="Документы (до 5Мб)"></asp:Label>
            <br />
            <%-- ListView for demonstrating attached documents if any. --%>
            <asp:ListView runat="server" ID="LViewDocuments" OnItemCommand="LViewDocuments_ItemCommand">
                <ItemTemplate>
                    <div class="container-item startup-panel radius-like" style="margin-left: 70px !important; width: 250px !important; height: 150px !important;">
                        <%-- <div class="radius-like">
                    <asp:CheckBox runat="server" Text='Приватный' ID="PublicBox"
                        Checked='<%# Eval("IsPublic") %>'></asp:CheckBox>
                </div>--%>
                        <h1 class="tag-item" style="margin-bottom: 100px; margin-left: 60px; width: 150px; height: 50px;"><%# Eval("FileName") %></h1>
                        <asp:Button runat="server"
                            CssClass="tag-item round-div-block button-style-for-page about-like-cloud-button-for-page-four margin-bottom-top-as-usual"
                            Style="width: 90%; height: max-content;"
                            Text="Удалить"
                            CommandName="RemoveDocument"
                            CommandArgument='<%# Bind("FileName") %>' ID="BtnRemoveDocument"></asp:Button>
                        <br />
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <br />
            <asp:FileUpload Style="margin-bottom: 10px !important; font-size: 17px; margin-left: 70px; margin-right: 20px;"
                runat="server"
                AllowMultiple="true"
                ID="DocumentUpload" />

            <asp:Button Style="margin-bottom: 10px !important; margin-bottom: 0px !important; font-size: 17px !important; margin-left: 70px; height: 35px !important;" CssClass="round-div-block-specified-about about-like-cloud-button-three" runat="server" Text="Добавить документы" ID="BtnAddDocuments"
                OnClick="BtnAddDocuments_Click" />

            <asp:Button runat="server" Style="margin-bottom: 0px !important; float: left; margin-right: 40px !important; font-size: 17px !important; margin-left: 70px; height: 35px !important;" CssClass="round-div-block-specified-about about-like-cloud-button-three" OnClick="BtnSave_Click" ID="BtnSave" Text="Сохранить изменения" />

            <asp:Button runat="server" Style="width: 170px !important; font-size: 17px !important; margin-left: 70px; height: 35px !important;"
                CssClass="round-div-block-specified-about about-like-cloud-button-three" OnClick="BtnCancel_Click" ID="BtnCancel" Text="Отмена" />
        </div>
    </div>
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
