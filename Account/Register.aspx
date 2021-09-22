﻿<%@ Page Title="Регистрация" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="StartUpWebAPI.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2 class="title-setting"><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <asp:PlaceHolder runat="server" Visible="false" ID="SameUsernamesMessage">
        <div class="text-success">
            Введённое имя пользователя уже существует в системе. Пожалуйста, укажите другое имя пользователя.
        </div>
    </asp:PlaceHolder>
    <div class="form-horizontal request-center">
        <h4 class="textMarginAndSize">Создать новый аккаунт</h4>
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="LoginBox" CssClass="col-md-2 control-label">Логин</asp:Label>
            <div class="col-md-10" >
                <asp:TextBox runat="server" ID="LoginBox" CssClass="form-control txt-shadow" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LoginBox"
                    CssClass="text-danger" ErrorMessage="Логин обязателен для заполнения." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FullNameBox" CssClass="col-md-2 control-label">ФИО</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FullNameBox" CssClass="form-control txt-shadow" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FullNameBox"
                    CssClass="text-danger" ErrorMessage="ФИО обязательно для заполнения." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PasswordBox" CssClass="col-md-2 control-label">Пароль</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="PasswordBox" TextMode="Password " CssClass="form-control txt-shadow" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordBox"
                    CssClass="text-danger" ErrorMessage="Поле пароля обязательно." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPasswordBox" CssClass="col-md-2 control-label">Повторите пароль</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPasswordBox" TextMode="Password" CssClass="form-control txt-shadow" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPasswordBox"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Подтверждение пароля обязательно." />
                <asp:CompareValidator runat="server" ControlToCompare="PasswordBox" ControlToValidate="ConfirmPasswordBox"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Пароли не совпадают." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10 btn-setting">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Зарегистрироваться" CssClass="btn btn-primary " />
            </div>
        </div>
    </div>
</asp:Content>
