<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StartUpWebAPI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LoginView runat="server">
        <AnonymousTemplate>
            <div class="jumbotron gradientable">
                <img class="ImageSetting" src="Resources/StarTup.png" alt="STARTUP" />
                <h1 class="font-family-title fade-in" style="font-size: 80px;">StartUp</h1>
                <p style="font-size: 25px;" class="lead font-family-title fade-in">Компания, объединяющая людей для реализации уникальных проектов</p>
                &nbsp;
            </div>

            <div class="row">
                <div class="col-md-4 fade-in">
                    <h2>Что это?</h2>
                    <p>
                        Это платформа для людей и объединений людей, позволяющая найти круги по интересам в короткий промежуток времени.
                Миллионы людей уже зарегистрированы.
                Присоединяйтесь!
                    </p>
                </div>
                <div class="col-md-4 fade-in">
                    <h2>Что здесь есть?</h2>
                    <p>
                        Категории групп, возможность пообщаться с другими в реальном времени, в том числе в командах.
                    </p>
                </div>
                <div class="col-md-4 fade-in">
                    <h2>Хочу зарегистрироваться!</h2>
                    <p>
                        Отличное решение!
                    </p>
                    <p>
                        <a class="btn btn-default animatable" href="Account/Register.aspx">Тогда нажмите cюда! &raquo;</a>
                    </p>
                </div>
            </div>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h1>Привет, <%: User.Identity.Name %>. </h1>
            <div class="inline-div">

                <div class="jumbotron gradientable marginaled">
                    <h1>Организованные мной стартапы</h1>
                    <asp:ListView runat="server" ID="LViewMyStartups" OnItemCommand="LViewMyStartups_ItemCommand">
                        <ItemTemplate>
                            <div class="round-div-block">
                                <asp:LinkButton runat="server" Text='<%# Eval("Name") %>' ForeColor="Black" ID="BtnStartUpInfo" CommandName="StartUpClicked" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    <asp:Panel runat="server" ID="EmptyStartupsPanel" Visible="false">
                        <p class="lead">Стартапов пока нет. :(</p>
                    </asp:Panel>
                    <asp:Button ID="BtnCreateStartUp"
                        OnClick="BtnCreateStartUp_Click"
                        runat="server"
                        CssClass="round-div-block simple-cloud-button"
                        Text="Создать новый стартап"></asp:Button>
                    &nbsp;
                </div>
                <div class="jumbotron gradientable marginaled">
                    <h1>Организованные мной команды</h1>
                    <asp:ListView runat="server" ID="LViewMyTeams" OnItemCommand="LViewMyTeams_ItemCommand">
                        <ItemTemplate>
                            <div class="round-div-block">
                                <asp:LinkButton runat="server" Text='<%# Eval("Team.Name") %>' ForeColor="Black" ID="BtnTeamInfo" CommandName="TeamClicked" CommandArgument='<%# Eval("Team.Id") %>'></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    <asp:Panel runat="server" ID="EmptyTeamsPanel" Visible="false">
                        <p class="lead">Команд пока нет. :(</p>
                    </asp:Panel>
                    <asp:Button CssClass="round-div-block simple-cloud-button" runat="server" Text="Создать новую команду"></asp:Button>
                    &nbsp;
                </div>
            </div>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
