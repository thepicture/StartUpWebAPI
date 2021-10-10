<%@ Page Title="Регистрация" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Register.aspx.cs"
    Inherits="StartUpWebAPI.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <br />
    <div style="display: flex; justify-content: center; align-items: center; vertical-align: middle;">
        <div class="round-div-block semi-transparent-register-like request-auto-height"
            style="width: 650px; animation: none !important; display: inline-block;">
            <div class="grid-like">
                <asp:PlaceHolder runat="server" Visible="false" ID="SameUsernamesMessage">
                    <div class="text-danger-register-like inline-block">
                        Введённый логин уже существует в системе. Пожалуйста, укажите другой логин.
                    </div>
                </asp:PlaceHolder>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="LoginBox"
                    CssClass="text-danger-register-like"
                    ErrorMessage="Поле логина обязательно для заполнения"
                    Display="Dynamic" />
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="FullNameBox"
                    CssClass="text-danger-register-like"
                    ErrorMessage="Поле ФИО обязательно для заполнения"
                    Display="Dynamic" />
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="PasswordBox"
                    CssClass="text-danger-register-like"
                    ErrorMessage="Поле пароля обязательно"
                    Display="Dynamic" />
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="ConfirmPasswordBox"
                    CssClass="text-danger-register-like"
                    Display="Dynamic"
                    ErrorMessage="Подтверждение пароля обязательно" />
                <asp:CompareValidator runat="server"
                    ControlToCompare="PasswordBox"
                    ControlToValidate="ConfirmPasswordBox"
                    CssClass="text-danger-register-like"
                    Display="Dynamic"
                    ErrorMessage="Пароли не совпадают" />
            </div>
            <h2 class="title-setting" style="color: white;"><%: Title %></h2>
            <div style="display: flex; justify-content: center;">
                <asp:TextBox Style="margin: 20px 0px -10px 0px;" ID="LoginBox" TextMode="SingleLine"
                    runat="server"
                    BorderStyle="None"
                    CssClass="nice-text-box prevent-selection textbox-style-setting"
                    placeholder="Логин"></asp:TextBox>
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
            </div>
            <div style="display: flex; justify-content: center;">
                <asp:TextBox Style="margin: 0px 0px 0px 0px;" ID="FullNameBox" runat="server" BorderStyle="None"
                    CssClass="nice-text-box prevent-selection textbox-style-setting"
                    placeholder="ФИО"
                    TextMode="SingleLine"></asp:TextBox>

            </div>
            <br />
            <div style="display: flex; justify-content: center;">
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:TextBox Style="margin: -30px 0px 0px 0px;" runat="server" ID="PasswordBox" TextMode="Password"
                    BorderStyle="None"
                    CssClass="nice-text-box prevent-selection textbox-style-setting" placeholder="Пароль"></asp:TextBox>
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:TextBox Style="margin: -30px 0px 0px 0px;" runat="server" ID="ConfirmPasswordBox"
                    TextMode="Password"
                    BorderStyle="None"
                    CssClass="nice-text-box prevent-selection textbox-style-setting" placeholder="Повтор пароля"></asp:TextBox>
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
            </div>

            <div style="display: flex; justify-content: center; margin-bottom: 20px;">
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:Button Style="margin: -25px 0px 0px 0px; font-size: 26px !important;" Text="Зарегистрироваться"
                    CssClass="round-div-block-specified-about about-like-cloud-button"
                    runat="server" ID="BtnLogIn" OnClick="CreateUser_Click" />
            </div>
        </div>
    </div>
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
