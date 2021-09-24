<%@ Page Title="Регистрация" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Register.aspx.cs"
    Inherits="StartUpWebAPI.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <asp:PlaceHolder runat="server" Visible="false" ID="SameUsernamesMessage">
        <div class="text-success">
            Введённое имя пользователя уже существует в системе. Пожалуйста, укажите другое имя пользователя.
        </div>
    </asp:PlaceHolder>

    <div class="form-horizontal request-center" style="display: none">
        <h4 class="textMarginAndSize">Создать новый аккаунт</h4>
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="LoginBox" CssClass="col-md-2 control-label">Логин</asp:Label>
            <div class="col-md-10">
                <%-- <asp:TextBox runat="server" ID="LoginBox" CssClass="form-control txt-shadow" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LoginBox"
                    CssClass="text-danger" ErrorMessage="Логин обязателен для заполнения." />--%>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FullNameBox" CssClass="col-md-2 control-label">ФИО</asp:Label>
            <div class="col-md-10">
                <%--<asp:TextBox runat="server" ID="FullNameBox" CssClass="form-control txt-shadow" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FullNameBox"
                    CssClass="text-danger" ErrorMessage="ФИО обязательно для заполнения." />--%>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PasswordBox" CssClass="col-md-2 control-label">Пароль</asp:Label>
            <div class="col-md-10">
                <%-- <asp:TextBox runat="server" ID="PasswordBox" TextMode="Password " CssClass="form-control txt-shadow" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordBox"
                    CssClass="text-danger" ErrorMessage="Поле пароля обязательно." />--%>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPasswordBox" CssClass="col-md-2 control-label">Повторите пароль</asp:Label>
            <div class="col-md-10">
                <%-- <asp:TextBox runat="server" ID="ConfirmPasswordBox" TextMode="Password" CssClass="form-control txt-shadow" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPasswordBox"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Подтверждение пароля обязательно." />
                <asp:CompareValidator runat="server" ControlToCompare="PasswordBox" ControlToValidate="ConfirmPasswordBox"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Пароли не совпадают." />--%>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10 btn-setting">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Зарегистрироваться" CssClass="btn btn-primary " />
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <br />

    <div style="display: flex; justify-content: center; vertical-align: middle;">
        <div class="round-div-block semi-transparent-register-like"
            style="width: 650px; height: 460px; animation: none !important; display: inline-block;">
            <h2 class="title-setting-register-like" style="color: white;"><%: Title %></h2>
            <div style="display: flex; justify-content: center;">
                <asp:TextBox Style="margin: 20px 0px -10px 0px;" ID="LoginBox" TextMode="SingleLine"
                    runat="server"
                    BorderStyle="None" CssClass="nice-text-box prevent-selection textbox-style-setting" placeholder="Логин"></asp:TextBox>
            </div>
            <br />


            <div style="display: flex; justify-content: center;">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LoginBox"
                    CssClass="text-danger-register-like" ErrorMessage="Поле логина обязательно для заполнения" />
            </div>
            <div style="display: flex; justify-content: center;">
                <asp:TextBox Style="margin: 0px 0px 0px 0px;" ID="FullNameBox" runat="server" BorderStyle="None"
                    CssClass="nice-text-box prevent-selection textbox-style-setting"
                    placeholder="ФИО"
                    TextMode="SingleLine"></asp:TextBox>

            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FullNameBox"
                    CssClass="text-danger-register-like" ErrorMessage="Поле ФИО обязательно для заполнения" />
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:TextBox Style="margin: -30px 0px 0px 0px;" runat="server" ID="PasswordBox" TextMode="Password"
                    BorderStyle="None"
                    CssClass="nice-text-box prevent-selection textbox-style-setting" placeholder="Пароль"></asp:TextBox>
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordBox"
                    CssClass="text-danger-register-like" ErrorMessage="Поле пароля обязательно" />
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:TextBox Style="margin: -30px 0px 0px 0px;" runat="server" ID="ConfirmPasswordBox"
                    BorderStyle="None"
                    CssClass="nice-text-box prevent-selection textbox-style-setting" placeholder="Повтор пароля"></asp:TextBox>
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPasswordBox"
                    CssClass="text-danger-register-like" Display="Dynamic" ErrorMessage="Подтверждение пароля обязательно" />
            </div>

            <div style="display: flex; justify-content: center; margin-bottom: 20px;">
                <asp:CompareValidator runat="server" ControlToCompare="PasswordBox" ControlToValidate="ConfirmPasswordBox"
                    CssClass="text-danger-register-like" Display="Dynamic" ErrorMessage="Пароли не совпадают" />
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
