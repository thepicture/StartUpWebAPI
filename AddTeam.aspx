<%@ Page Language="C#"
    Title="Добавление команды"
    AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeBehind="AddTeam.aspx.cs"
    Inherits="StartUpWebAPI.AddTeam" %>

<asp:Content runat="server"
    ContentPlaceHolderID="MainContent"
    ID="BindingTeamsContent">
    <div class="add-teams-main-container">
        <div class="round-div-block semi-transparent-register-like-two request-auto-height add-teams-sub-main-container">
            <br />
            <br />
            <asp:Label runat="server"
                class="add-teams-name-label"
                Text="Название"></asp:Label>
            <asp:TextBox runat="server"
                ID="TBoxName"
                CssClass="nice-text-box prevent-selection textbox-style-setting add-teams-name-box"
                TextMode="SingleLine"
                Wrap="true"></asp:TextBox>
            <br />
            <br />
            <asp:Label runat="server"
                class="add-teams-members-label"
                Text="Максимум участников"></asp:Label>
            <asp:TextBox runat="server"
                ID="TBoxMaxMembers"
                CssClass="nice-text-box prevent-selection textbox-style-setting add-teams-member-box"
                Width="50"
                TextMode="Number"
                MaxLength="6"
                AutoCompleteType="Disabled"></asp:TextBox>
            <br />
            <br />
            <asp:Label runat="server"
                Text="Описание"
                CssClass="request-vertical-top add-teams-description-label"></asp:Label>
            <asp:TextBox runat="server"
                ID="TBoxDescription"
                ForeColor="Black"
                CssClass="nice-text-box prevent-selection textbox-style-setting request-overflow-y"
                Width="457"
                TextMode="MultiLine"
                Height="80"></asp:TextBox>
            <br />
            <br />

            <asp:Label runat="server"
                Text="Логотип команды"
                CssClass="add-teams-logo-label"></asp:Label>
            <br />
            <%-- ListView for demonstrating attached images if any. --%>
            <asp:Panel runat="server"
                CssClass="container-item startup-panel radius-like add-teams-panel">
                <asp:Image class="startup-image radius-like image-cover-auto"
                    runat="server"
                    ID="TeamImage"
                    alt='Логотип команды' />

                <asp:Button runat="server"
                    CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page-four margin-bottom-top-as-usual add-teams-remove-image-button"
                    Text="Удалить"
                    ID="BtnRemoveImage"
                    OnClick="BtnRemoveImage_Click"
                    Visible="false"></asp:Button>
                <br />
            </asp:Panel>
            <asp:FileUpload runat="server"
                AllowMultiple="false"
                CssClass="add-teams-upload-image"
                ID="FileUploadImage" />

            <asp:Button runat="server"
                CssClass="round-div-block-specified-about about-like-cloud-button-three add-teams-add-image"
                Text="Прикрепить логотип команды"
                ID="BtnAddImage"
                OnClick="BtnAddImage_Click" />
            <div class="add-teams-inline-block">
                <asp:Button runat="server"
                    CssClass="round-div-block-specified-about about-like-cloud-button-three add-teams-save"
                    OnClick="BtnSave_Click"
                    ID="BtnSave"
                    Text="Сохранить изменения" />

                <asp:Button runat="server"
                    CssClass="round-div-block-specified-about about-like-cloud-button-three add-teams-discard"
                    OnClick="BtnCancel_Click"
                    ID="BtnCancel"
                    Text="Отмена" />
            </div>
        </div>
        <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
    </div>
</asp:Content>
