<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StartUpWebAPI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron gradientable">
        <h1>StartUp</h1>
        <p class="lead">Компания, помогающая стартаперам выполнить цели.</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Что это?</h2>
            <p>
                Это платформа для людей и объединений людей, позволяющая найти круги по интересам в короткий промежуток времени.
                Миллионы людей уже зарегистрированы.
                Присоединяйтесь!
            </p>
        </div>
        <div class="col-md-4">
            <h2>Что здесь есть?</h2>
            <p>
                Категории групп, возможность пообщаться с другими в реальном времени, в том числе в командах.
            </p>
        </div>
        <div class="col-md-4">
            <h2>Хочу зарегистрироваться!</h2>
            <p>
                Отличное решение!
            </p>
            <p>
                <a class="btn btn-default" href="Account/Register.aspx">Тогда нажмите cюда! &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
