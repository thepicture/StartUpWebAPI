<%@ Page EnableEventValidation="false" Title="Добавление стартапа" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddStartUp.aspx.cs" Inherits="StartUpWebAPI.AddStartUp" EnableViewState="true" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="BindingStartupsContent">
    <div class="search-panel">
        <div class="round-div-block semi-transparent-register-like-two request-auto-height search-items">
            <br />

            <asp:Label runat="server" CssClass="search-label" Text="Имя"></asp:Label>
            <asp:TextBox runat="server"
                ID="TBoxName"
                CssClass="nice-text-box prevent-selection textbox-style-setting search-startup-text-box"
                TextMode="SingleLine"
                Wrap="true"></asp:TextBox>
            <br />
            <br />
             <asp:Label runat="server" CssClass="search-label" Text="Телеграм"></asp:Label>
            <asp:TextBox runat="server"
                ID="ContactBox"
                CssClass="nice-text-box prevent-selection textbox-style-setting search-startup-text-box"
                TextMode="SingleLine"
                Wrap="true"
                Width="447 !important;"></asp:TextBox>
            <br />
            <br />
            <asp:Label runat="server" CssClass="search-label" Text="Категория"></asp:Label>
            <asp:DropDownList AutoPostBack="true"
                runat="server"
                class="form-control category-dropdown"
                ID="ComboCategories"
                Width="200 !important">
            </asp:DropDownList>
            <br />
            <br />
              <asp:Label runat="server" CssClass="search-label" Text="Регион"></asp:Label>
            <asp:DropDownList AutoPostBack="true"
                runat="server"
                class="form-control category-dropdown"
                ID="DropDownRegions"
                Width="200 !important">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label runat="server" CssClass="search-label" Text="Максимум участников"></asp:Label>
            <asp:TextBox runat="server"
                ID="TBoxMaxMembers"
                CssClass="nice-text-box prevent-selection textbox-style-setting max-members-textbox"
                Width="50 !important"
                TextMode="Number"
                MaxLength="10"
                AutoCompleteType="Disabled"></asp:TextBox>
            <br />
            <br />
            <asp:Label runat="server"
                Text="Описание"
                CssClass="request-vertical-top description-startup-label"></asp:Label>
            <asp:TextBox runat="server"
                ID="TBoxDescription"
                ForeColor="Black"
                Wrap="true"
                Width="457 !important"
                TextMode="MultiLine"
                CssClass="nice-text-box prevent-selection textbox-style-setting request-overflow-y"
                Height="80"></asp:TextBox>
            <br />
            <br />

            <asp:Label runat="server" CssClass="search-label" Text="Изображения стартапа"></asp:Label>
            <br />
            <asp:CheckBox runat="server" Text="Завершён" ID="CheckBoxDone" Checked='<%# Bind("IsDone") %>' Visible="false"
                AutoPostBack="true"></asp:CheckBox>
            <br />
            <%-- ListView for demonstrating attached images if any. --%>
            <asp:ListView runat="server" ID="LViewImages" OnItemCommand="LViewImages_ItemCommand">
                <ItemTemplate>
                    <div class="container-item startup-panel radius-like main-startup-image-container">
                        <img class="startup-image radius-like image-cover-auto" src='<%# Eval("ImageInBase64") %>' alt='<%# Eval("Name") %>' />

                        <h1 class="tag-item startup-image-header"><%# Eval("Name") %></h1>

                        <asp:Button runat="server"
                            CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page-four margin-bottom-top-as-usual remove-image-button"
                            Text="Удалить"
                            CommandName="RemoveImage"
                            CommandArgument='<%# Bind("Name") %>' ID="BtnRemoveImage"></asp:Button>
                        <br />
                    </div>
                </ItemTemplate>
            </asp:ListView>

            <asp:FileUpload runat="server"
                CssClass="upload-image-input"
                AllowMultiple="true"
                ID="FileUploadImages" />

            <asp:Button runat="server"
                CssClass="round-div-block-specified-about about-like-cloud-button-three add-images-startup-button"
                Text="Добавить изображения"
                ID="BtnAddImages"
                OnClick="BtnAddImages_Click" />

            <asp:Label runat="server"
                Text="Документы (до 25Мб)"
                CssClass="search-label"></asp:Label>
            <br />
            <%-- ListView for demonstrating attached documents if any. --%>
            <asp:ListView runat="server" ID="LViewDocuments" OnItemCommand="LViewDocuments_ItemCommand">
                <ItemTemplate>
                    <div class="container-item startup-panel radius-like main-startup-image-container">
                        <h1 class="tag-item startup-image-header"><%# Eval("FileName") %></h1>
                        <asp:Button runat="server"
                            CssClass="tag-item round-div-block button-style-for-page about-like-cloud-button-for-page-four margin-bottom-top-as-usual remove-image-button"
                            Text="Удалить"
                            CommandName="RemoveDocument"
                            CommandArgument='<%# Bind("FileName") %>' ID="BtnRemoveDocument"></asp:Button>
                        <br />
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <br />
            <asp:FileUpload
                CssClass="upload-image-input"
                runat="server"
                AllowMultiple="true"
                ID="DocumentUpload" />

            <asp:Button CssClass="round-div-block-specified-about about-like-cloud-button-three add-startup-documents-button"
                runat="server"
                Text="Добавить документы"
                ID="BtnAddDocuments"
                OnClick="BtnAddDocuments_Click" />

            <asp:Button runat="server"
                CssClass="round-div-block-specified-about about-like-cloud-button-three startup-save-button"
                OnClick="BtnSave_Click"
                ID="BtnSave"
                Text="Сохранить изменения" />

            <asp:Button runat="server"
                CssClass="round-div-block-specified-about about-like-cloud-button-three startup-cancel-button"
                OnClick="BtnCancel_Click"
                ID="BtnCancel"
                Text="Отмена" />
        </div>
    </div>
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
