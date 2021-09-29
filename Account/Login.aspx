<%@ Page Title="Авторизация" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StartUpWebAPI.Account.Login" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="text-align: center;">
    </div>
    <div style="display: none;">
        <div class="row">
            <div class="col-md-8">
                <section id="loginForm">
                    <asp:PlaceHolder runat="server" Visible="false" ID="RegSuccessMessage">
                        <div class="text-success">
                            Вы успешно зарегистрированы. Теперь вы можете войти в систему.
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" Visible="false" ID="LogFailedMessage">
                        <div class="text-danger">
                            Не удалось войти в систему: 
                        неверное имя пользователя и/или пароль.
                        Пожалуйста, проверьте корректность введённых данных.
                        </div>
                    </asp:PlaceHolder>
                    <div class="form-horizontal">
                        <h4 class="textMarginAndSize">Использовать существующий аккаунт для авторизации</h4>
                        <hr width="600px;" />

                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="LoginBox" CssClass="col-md-2 control-label">Логин</asp:Label>
                            <div class="col-md-10">
                                <%--  <asp:TextBox runat="server" ID="LoginBox" CssClass="form-control textblock-design" TextMode="SingleLine" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="LoginBox"
                                    CssClass="text-danger" ErrorMessage="Поле логина обязательно для заполнения." />--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="PasswordBox" CssClass="col-md-2 control-label">Пароль</asp:Label>
                            <div class="col-md-10">
                                <%-- <asp:TextBox runat="server" ID="PasswordBox" TextMode="Password" CssClass="form-control textblock-design" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordBox" CssClass="text-danger" ErrorMessage="Поле пароля обязательно для заполнения." />--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <div class="checkbox">
                                    <%--   <asp:CheckBox runat="server" ID="RememberMe" />
                                    <asp:Label runat="server" AssociatedControlID="RememberMe">Запомнить меня?</asp:Label>--%>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" OnClick="LogIn" Text="Авторизоваться" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                    <p>
                        <%--<asp:HyperLink runat="server" ID="RegisterHyperLink" NavigateUrl="~/Account/Register.aspx" ViewStateMode="Disabled">Ещё нет в системе?</asp:HyperLink>--%>
                    </p>
                </section>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <br />
    <div style="display: flex; justify-content: center; vertical-align: middle;">
        <div class="round-div-block semi-transparent-register-like request-auto-height" style="width: 650px; animation: none !important; display: inline-block;">
            <h2 class="title-setting" style="color: white;"><%: Title %></h2>
            <div style="display: flex; justify-content: center;">
                <asp:TextBox ID="LoginBox" Style="margin: 20px 0px -20px 0px;" runat="server" BorderStyle="None"
                    CssClass="nice-text-box prevent-selection textbox-style-setting"
                    placeholder="Логин"></asp:TextBox>
            </div>
            <br />

            <div style="display: flex; justify-content: center;">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LoginBox"
                    CssClass="text-danger-register-like" ErrorMessage="Поле логина обязательно для заполнения." />
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:TextBox Style="margin: -10px 0px -20px 0px;" runat="server" ID="PasswordBox"
                    BorderStyle="None" CssClass="nice-text-box prevent-selection textbox-style-setting" TextMode="Password" placeholder="Пароль"></asp:TextBox>
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordBox"
                    CssClass="text-danger-register-like"
                    ErrorMessage="Поле пароля обязательно для заполнения." />
            </div>
            <br />
            <div style="display: flex; justify-content: center; margin: -30px 0 20px 0px; color: white; font-size: 20px;">
                <asp:CheckBox Text="&nbsp Запомнить меня" runat="server" Style="margin: 0 10px -5px 0; font-family: 'Cormorant', serif;" ID="RememberMe" />
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:Button Style="margin: -25px 0px 0px 0px; font-size: 26px !important;" Text="Авторизоваться" 
                    CssClass="round-div-block-specified-about about-like-cloud-button"
                    runat="server" ID="BtnLogIn" OnClick="LogIn" />
            </div>
            <br />
            <div style="display: flex; justify-content: center;">
                <asp:HyperLink
                    runat="server"
                    ID="RegisterHyperLink"
                    NavigateUrl="~/Account/Register.aspx"
                    Style="color: white; font-family: 'Cormorant', serif; font-size: 20px;"
                    ViewStateMode="Disabled">Ещё нет в системе?</asp:HyperLink>
            </div>
        </div>
    </div>
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
