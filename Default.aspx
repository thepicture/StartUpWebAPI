<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StartUpWebAPI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="AnonContent">
        <div class="jumbotron gradientable">
            <img class="ImageSetting" src="Resources/StarTup.png" alt="STARTUP" />
            <h1>StartUp</h1>
            <p class="lead">Компания, помогающая стартаперам выполнить цели.</p>
            &nbsp;
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
    </asp:Panel>
    <asp:Panel runat="server" ID="LoggedInContent">
        <h1>Привет, <%: Request.Cookies.Get("username").Value %>. </h1>
        <div class="inline-div">

            <div class="jumbotron gradientable marginaled">
                <h1>Организованные мной стартапы</h1>
                <asp:ListView runat="server" ID="LViewMyStartups">
                    <ItemTemplate>
                        <div class="round-div-block">
                            <asp:LinkButton runat="server" Text='<%# Eval("Name") %>' ForeColor="Black" ID="BtnStartUpInfo" OnClick="BtnStartUpInfo_Click"></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <asp:Panel runat="server" ID="EmptyStartupsPanel" Visible="false">
                    <p class="lead">Стартапов пока нет. :(</p>
                    <asp:Button runat="server" Text="Создать новую команду"></asp:Button>
                </asp:Panel>
                &nbsp;
            </div>
            <div class="jumbotron gradientable">
                <h1>Организованные мной команды</h1>
                <asp:ListView runat="server" ID="LViewMyTeams">
                    <ItemTemplate>
                        <div class="round-div-block">
                            <asp:LinkButton runat="server" Text='<%# Eval("Name") %>' ForeColor="Black" ID="BtnTeamInfo" OnClick="BtnTeamInfo_Click"></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <asp:Panel runat="server" ID="EmptyTeamsPanel" Visible="false">
                    <p class="lead">Команд пока нет. :(</p>
                    <asp:Button runat="server" Text="Создать новую команду"></asp:Button>
                </asp:Panel>
                &nbsp;
            </div>
        </div>
    </asp:Panel>
</asp:Content>
