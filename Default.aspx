<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StartUpWebAPI.Default" %>

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
            <h1 class="centerized-title">Привет, <%: User.Identity.Name %>. </h1>
            <div class="marginaled center-align">
                <h1 class="items-title">Мои стартапы</h1>
                <asp:ListView runat="server" ID="LViewMyStartups">
                    <ItemTemplate>
                        <asp:LinkButton runat="server"
                            ID="BtnStartUpInfo"
                            href='<%#"/StartUpInfo?id=" + Eval("Id") %>'
                            onmouseenter='<%# "showDescription(`start-up-" + Eval("Id")  + "`, `" + Eval("SafeDescription") + "`)"%>'
                            onmouseleave="hideDescription()"
                            class='<%# "start-up-" + Eval("Id") %>'>
                                <div class="startup-panel radius-like container-item">
                                    <img class="startup-image radius-like image-cover-auto"
                                         src='<%# Eval("ImagePreview") %>' alt='<%# Eval("Name") %>' />
                                    <h1 class="tag-item item-name"><%# Eval("Name") %></h1>
                                    <%-- Div gets tag-item class to be in the container item. --%>
                                    <div class="tag-item inherit-font-size marginated-tag-item">
                                          <h1 class="tag-item category-element"><%# Eval("SplittedCategory") %></h1>
                                          <h1 class="tag-item blue-sign"
                                              runat="server"
                                              visible='<%# ((HashSet<StartUpWebAPI.Entities.StartUpOfUser>)Eval("StartUpOfUser")).Any(s => s.User.Login.Equals(User.Identity.Name) && !s.RoleType.Name.Equals("Участник"))%>'><%# Eval("MyRole") %></h1>
                                    </div>
                        <%-- The startup is ended sign. --%>
                        <div class="tag-item startup-end-block"
                                          runat="server"
                                          visible='<%# Convert.ToBoolean(Eval("IsDone")) %>'>
                                          <div class="tag-item transparent-blue"></div>
                                          <span class="tag-item request-solid-color done-text">Завершён</span>
                                      </div>
                                    <div class="tag-item gray-gradient radius-like"></div>
                                    <div class="sign-my-startup tag-item align-right"
                                        runat="server"
                                        Visible='<%#((HashSet<StartUpWebAPI.Entities.StartUpOfUser>)Eval("StartUpOfUser"))
                                            .Count >= Convert.ToInt32(Eval("MaxMembersCount")) 
                                            && !Convert.ToBoolean(Eval("IsDone")) %>'>
                                        <asp:Label runat="server"
                                            class="rotated-text">MAX</asp:Label>
                                    </div>
                                </div>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:ListView>
                <asp:Panel runat="server" ID="EmptyStartupsPanel" Visible="false">
                    <p class="default-lead empty-items-font-size">Стартапов пока нет :(</p>
                </asp:Panel>
                <asp:Button ID="BtnCreateStartUp"
                    OnClick="BtnCreateStartUp_Click"
                    runat="server"
                    Style="height: 40px; margin-block-start: 30px; width: 250px;"
                    CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usua"
                    Text="Создать новый стартап"></asp:Button>
                &nbsp;
            </div>
            <div class="marginaled center-align">
                <h1 class="items-title">Мои команды</h1>
                <asp:ListView runat="server" ID="LViewMyTeams">
                    <ItemTemplate>
                        <%-- New design --%>
                        <asp:LinkButton runat="server"
                            ID="BtnStartUpInfo"
                            href='<%#"/TeamInfo?id=" + Eval("Id") %>'
                            onmouseenter='<%# "showDescription(`team-" + Eval("Id")  + "`, `" + Eval("SafeDescription") + "`)"%>'
                            onmouseleave="hideDescription()"
                            class='<%# "team-" + Eval("Id") %>'>
                                <div class="startup-panel radius-like container-item">
                                    <img class="startup-image radius-like image-cover-auto"
                                         src='<%# Eval("ImagePreview") %>' alt='<%# Eval("Name") %>' />
                                    <h1 class="tag-item item-name"><%# Eval("Name") %></h1>
                                    <%-- Div gets tag-item class to be in the container item. --%>
                                    <div class="tag-item inherit-font-size marginated-tag-item">
                                          <h1 class="tag-item category-element"><%# "Участников: " + Eval("CountOfMembers") %></h1>
                                          <h1 class="tag-item blue-sign"
                                              runat="server"
                                              visible='<%# ((HashSet<StartUpWebAPI.Entities.TeamOfUser>)Eval("TeamOfUser")).Any(s => s.User.Login.Equals(User.Identity.Name) && !s.RoleType.Name.Equals("Участник"))%>'><%# Eval("MyRole") %></h1>
                                    </div>
                        <%-- The team is ended sign. --%>
                                    <div class="tag-item gray-gradient radius-like"></div>
                                    <div class="sign-my-startup tag-item align-right"
                                        runat="server"
                                        Visible='<%#((HashSet<StartUpWebAPI.Entities.TeamOfUser>)Eval("TeamOfUser"))
                                            .Count >= Convert.ToInt32(Eval("MaxMembersCount")) %>'>
                                        <asp:Label runat="server"
                                            class="rotated-text">MAX</asp:Label>
                                    </div>
                                </div>
                        </asp:LinkButton>

                    </ItemTemplate>
                </asp:ListView>
                <asp:Panel runat="server" ID="EmptyTeamsPanel" Visible="false">
                    <p class="default-lead empty-items-font-size">Команд пока нет :(</p>
                </asp:Panel>
                <div class="center-align">
                    <asp:Button Style="height: 40px; margin-block-start: 30px; width: 250px;"
                        CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usua"
                        runat="server"
                        ID="BtnCreateTeam"
                        OnClick="BtnCreateTeam_Click"
                        Text="Создать новую команду"></asp:Button>
                </div>
                &nbsp;
            </div>
        </LoggedInTemplate>
    </asp:LoginView>
    <script src='<%=ResolveUrl("~/Scripts/descriptor-presenter.js") %>'
        type="text/javascript">
    </script>
</asp:Content>
