<%@ Page Language="C#" Title="Добавление команды" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddTeam.aspx.cs" Inherits="StartUpWebAPI.AddTeam" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="BindingTeamsContent">
    <div style="display: flex; justify-content: center; align-items: center; vertical-align: middle;">
        <div class="round-div-block semi-transparent-register-like-two request-auto-height"
            style="width: 740px; animation: none !important; display: inline-block;">
            <br />
            <br />
            <asp:Label runat="server" Style="font-size: 20px; margin-left: 70px; margin-right: 20px;" Text="Название"></asp:Label>
            <asp:TextBox runat="server" ID="TBoxName" Style="width: 515px !important; max-width: 700px; color: black !important;" CssClass="nice-text-box prevent-selection textbox-style-setting" TextMode="SingleLine" Wrap="true"></asp:TextBox>
            <br />
            <br />
            <asp:Label runat="server" Style="font-size: 20px; margin-left: 70px; margin-right: 20px;" Text="Максимум участников"></asp:Label>
            <asp:TextBox runat="server" ID="TBoxMaxMembers" Style="color: black !important; width: 330px !important;" CssClass="nice-text-box prevent-selection textbox-style-setting" Width="50" TextMode="Number" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
            <br />
            <br />
            <asp:Label runat="server" Style="font-size: 20px; margin-left: 70px; margin-right: 20px;" Text="Описание"></asp:Label>
            <asp:TextBox runat="server" ID="TBoxDescription" Style="color: black !important; width: 457px !important;" CssClass="nice-text-box prevent-selection textbox-style-setting" Width="200" Height="80"></asp:TextBox>
            <br />
            <br />

            <asp:Label runat="server" Style="font-size: 20px; margin-left: 70px; margin-right: 20px;" Text="Логотип команды"></asp:Label>
            <br />
            <%-- ListView for demonstrating attached images if any. --%>
            <asp:Panel runat="server" class="container-item startup-panel radius-like" Style="margin-left: 70px !important; width: 250px !important; height: 150px !important;">
                <asp:Image class="startup-image radius-like image-cover-auto" runat="server" ID="TeamImage" alt='Логотип команды' />

                <asp:Button runat="server"
                    CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page-four margin-bottom-top-as-usual"
                    Style="width: 90%; height: max-content; bottom: 0; position: absolute;"
                    Text="Удалить"
                    ID="BtnRemoveImage"
                    OnClick="BtnRemoveImage_Click"
                    Visible="false"></asp:Button>
                <br />
            </asp:Panel>
            <asp:FileUpload runat="server"
                Style="margin-bottom: 0px !important; font-size: 17px; margin-left: 70px; margin-right: 20px;"
                AllowMultiple="false"
                ID="FileUploadImage" />

            <asp:Button runat="server" Style="margin-bottom: 10px !important; font-size: 17px !important; margin-left: 70px; height: 35px !important; border-radius: 5px !important; color: black !important;"
                CssClass="round-div-block-specified-about about-like-cloud-button-three" Text="Прикрепить логотип команды" ID="BtnAddImage"
                OnClick="BtnAddImage_Click" />
            <div style="display: inline-block;">
                <asp:Button runat="server"
                    Style="margin-bottom: 0px !important; float: left; margin-right: 40px !important; font-size: 17px !important; margin-left: 70px; height: 35px !important;"
                    CssClass="round-div-block-specified-about about-like-cloud-button-three"
                    OnClick="BtnSave_Click"
                    ID="BtnSave"
                    Text="Сохранить изменения" />

                <asp:Button runat="server"
                    Style="width: 170px !important; font-size: 17px !important; margin-left: 70px; height: 35px !important;"
                    CssClass="round-div-block-specified-about about-like-cloud-button-three"
                    OnClick="BtnCancel_Click"
                    ID="BtnCancel"
                    Text="Отмена" />
            </div>
        </div>
        <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
    </div>
</asp:Content>
