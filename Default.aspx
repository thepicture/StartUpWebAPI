<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StartUpWebAPI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LoginView runat="server">
        <AnonymousTemplate>
            <div class="jumbotron gradientable" style="height: 200px;">

                <object type="image/svg+xml" class="bg-image" data="Resources/StarTup.svg">
                    <img class="ImageSetting" src="Resources/StarTup.png" alt="STARTUP" />
                </object>

                <h1 class="font-family-title fade-in" style="font-size: 80px; margin-top: 5px !important;">StartUp</h1>
                <p style="font-size: 25px;" class="lead font-family-title fade-in">Компания, объединяющая людей для реализации уникальных проектов</p>
                &nbsp;
            </div>


            <div class="container-fluid fade-in" style="margin-left: -50px;">
                <br />

                <ul class="list-unstyled multi-steps">
                    <li class="is-active">Начни</li>
                    <li class="is-active">Зарегистрируйся</li>
                    <li class="is-active">Вступи или создай проект</li>
                    <li class="is-active">Найди или вступи в команду</li>
                    <li class="is-active">Выступай на конференциях</li>
                </ul>
            </div>

            <br />
            <div class="row" style="margin-left: 2px;">

                <div class="col-md-4 fade-in" style="background-color: #64749E; margin-right: 25px; height: 180px; text-indent: 20px; color: white; text-align: justify; border-radius: 10px;">
                    <h2>Что это?</h2>
                    <br />
                    <p>
                        Это платформа для людей и объединений людей, позволяющая найти круги по интересам в короткий промежуток времени.
                Миллионы людей уже зарегистрированы.
                Присоединяйтесь!
                    </p>
                </div>
                <div class="col-md-4 fade-in" style="background-color: #64689E; width: 331px; margin-right: 25px; height: 180px; text-indent: 20px; color: white; text-align: justify; border-radius: 10px;">
                    <h2>Что здесь есть?</h2>
                    <br />
                    <p>
                        Категории групп, возможность пообщаться с другими стартаперами в реальном времени, участвовать в командах.
                    </p>
                </div>
                <div class="col-md-4 fade-in" style="background-color: #6C649E; height: 180px; color: white; text-align: justify; border-radius: 10px;">
                    <h2 style="margin-left: 20px;">Хочу открыть Startup!</h2>
                    <br />
                    <p style="text-indent: 20px;">
                        Отличное решение!
                    </p>
                    <p>
                        <a class="btn btn-default animatable" style="margin-left: 20px;" href="Account/Register.aspx">Тогда нажмите cюда! </a>
                    </p>
                </div>
            </div>






        </AnonymousTemplate>
        <LoggedInTemplate>
            <h1>Привет, <%: User.Identity.Name %>. </h1>
            <div class="marginaled">
                <h1 style="color: black;">Организованные мной стартапы</h1>
                <asp:Panel HorizontalAlign="Center" runat="server">
                    <asp:ListView runat="server" ID="LViewMyStartups" OnItemCommand="LViewMyStartups_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="BtnStartUpInfo" CommandName="StartUpClicked" CommandArgument='<%# Eval("Id") %>'>
                                <div class="startup-panel radius-like container-item">
                                    <img class="startup-image radius-like image-cover-auto" src='<%# Eval("ImagePreview") %>' alt='<%# Eval("Name") %>' />
                                    <h1 class="tag-item" style="margin-left: 20px; margin-bottom: 45px;"><%# Eval("Name") %></h1>
                                    <h1 class="tag-item" style="margin-left: 20px; margin-bottom: 20px; color: #d4d4dd; font-size: 1.15em;"><%# Eval("SplittedCategory") %></h1>
                                    <div class="tag-item gray-gradient radius-like" style="z-index: 64; opacity: .8;"></div>
                                    <asp:Label class="sign-my-startup tag-item" style="left: auto;" Visible='<%# ((HashSet<StartUpWebAPI.Entities.StartUpOfUser>)Eval("StartUpOfUser")).Any(s => s.User.Login.Equals(User.Identity.Name))%>' runat="server">Мой стартап</asp:Label>
                                </div>
                            </asp:LinkButton>
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
                </asp:Panel>
                &nbsp;
            </div>
            <div class="jumbotron marginaled">
                <h1 style="color: black;">Организованные мной команды</h1>
                <asp:ListView runat="server" ID="LViewMyTeams" OnItemCommand="LViewMyTeams_ItemCommand">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="BtnTeamInfo" CommandName="TeamClicked" CommandArgument='<%# Eval("Id") %>'>
                            <div class="startup-panel radius-like container-item ">
                                <img class="startup-image radius-like image-cover-auto" src='<%# Eval("ImagePreview") %>' alt='<%# Eval("Name") %>' />
                                <h1 class="tag-item" style="margin-left: 20px;margin-bottom: 45px;"><%# Eval("Name") %></h1>
                                <h1 class="tag-item" style="margin-left: 20px;margin-bottom: 20px;color:#d4d4dd; font-size:1.15em;"><%# (string) Eval("CountOfMembers") + " участников" %></h1>
                                <div class="tag-item gray-gradient radius-like" style="z-index:555;opacity:.8"></div>
                    </div>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:ListView>
                <asp:Panel runat="server" ID="EmptyTeamsPanel" Visible="false">
                    <p class="lead">Команд пока нет. :(</p>
                </asp:Panel>
                <asp:Button CssClass="round-div-block simple-cloud-button" runat="server" Text="Создать новую команду"></asp:Button>
                &nbsp;
            </div>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
