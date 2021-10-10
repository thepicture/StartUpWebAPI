<%@ Page Language="C#"
    Title="Информация о стартапе"
    AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeBehind="StartUpInfo.aspx.cs"
    Inherits="StartUpWebAPI.StartUpInfo"
    EnableEventValidation="false" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" Style="padding-left: 50px; padding-right: 50px;">

    <asp:Panel runat="server">

        <div class="white-shadow-block radius-like">

            <div class="jumbotron  " style="padding-bottom: 0px !important; margin-bottom: -30px !important;">
                <h1>
                    <asp:Label runat="server" ID="MainName" Style="color: black;" CssClass="centerized-text"></asp:Label></h1>
                <p class="lead">
                </p>

                <div class="no-image-select image-flow"
                    onmouseover="showButtons()"
                    onmouseout="hideButtons()">
                    <button class="arrow prev button-style-for-page-info about-like-cloud-button-for-page-info popup-button"
                        onclick="return false;"
                        id="btnGoLeft">
                        &lt</button>
                    <div class="visible-gallery">
                        <ul class="images"
                            id="imageBoard">
                            <asp:ListView runat="server"
                                ID="LViewStartUpImages">
                                <ItemTemplate>
                                    <li class="inline-block">
                                        <asp:Image ImageUrl='<%# Eval("ImageInBase64") %>'
                                            runat="server"
                                            CssClass="radius-like image-cover-auto image" />
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                        </ul>
                    </div>
                    <button class="arrow next button-style-for-page-info about-like-cloud-button-for-page-info popup-button"
                        onclick="return false;"
                        id="btnGoRight">
                        &gt
                    </button>
                </div>
                <asp:Panel Style="display: block" ID="PanelMyStartups" runat="server">
                    <div style="margin-top: 70px !important; margin-left: 240px !important; overflow-y: auto !important; overflow-wrap: break-word; margin-top: 90px !important; height: 150px !important;">
                        <asp:Label Style="margin-top: 90px !important; font-size: x-large; text-wrap: normal !important;"
                            ID="Description"
                            runat="server"
                            ForeColor="Black"></asp:Label>
                    </div>

                    <table align="center" runat="server" cellpadding="5" style="border-spacing: 30px !important;" cellspacing="9">
                        <tr cellspacing="9" runat="server">

                            <td runat="server" style="margin-right: 20px;">
                                <label style="display: block; font-size: 17px; color: darkslateblue">Название:</label>
                                <asp:Label ID="Name" runat="server" ForeColor="Black"></asp:Label>
                            </td>

                            <td>
                                <label style="display: block; font-size: 17px; color: darkslateblue">Максимум участников:</label>
                                <asp:Label ID="MaxMembersCount" runat="server" ForeColor="Black"></asp:Label>
                            </td>

                            <td>
                                <label style="display: block; font-size: 17px; color: darkslateblue">Участников:</label>
                                <asp:Label ID="CountOfMembers" runat="server" ForeColor="Black"></asp:Label>
                            </td>

                            <td>
                                <label style="display: block; font-size: 17px; color: darkslateblue">Команд:</label>
                                <asp:Label ID="CountOfTeams" runat="server" ForeColor="Black"></asp:Label>
                            </td>

                        </tr>

                        <tr>

                            <td>
                                <label style="display: block; font-size: 17px; color: darkslateblue">Стартапер:</label>
                                <asp:Label ID="Creator" runat="server" ForeColor="Black"></asp:Label>
                            </td>

                            <td>
                                <label style="display: block; font-size: 17px; color: darkslateblue">Дата создания:</label>
                                <asp:Label ID="DateOfCreation" runat="server" ForeColor="Black"></asp:Label>
                            </td>

                            <td>
                                <label style="display: block; font-size: 17px; color: darkslateblue">Категория:</label>
                                <asp:Label ID="Category" runat="server" ForeColor="Black"></asp:Label>
                            </td>

                            <td>
                                <label style="display: block; font-size: 17px; color: darkslateblue">Проект:</label>
                                <asp:Label ID="IsActual" runat="server" ForeColor="Black"></asp:Label>
                            </td>
                            <td>
                                <label style="display: block; font-size: 17px; color: darkslateblue">Регион:</label>
                                <asp:Label ID="Region" runat="server" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                    <asp:Panel Style="display: block;" runat="server" ID="PStartupEdit" Visible="false">
                        <table style="margin-left: -10px; margin-top: -20px;">
                            <tr>
                                <td>
                                    <asp:LinkButton runat="server" Style="float: left; margin-left: 170px !important;"
                                        Text="Изменить информацию"
                                        CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual request-solid-color"
                                        ID="LinkButtonModifyStartUp"
                                        OnClick="LinkButtonModifyStartUp_Click"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server"
                                        Text='Удалить стартап'
                                        CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                                        ForeColor="White"
                                        ID="BtnDeleteStartUp"
                                        OnClick="BtnDeleteStartUp_Click"
                                        OnClientClick="return getDeleteItemState(`стартап`);"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </asp:Panel>
                    <table style="margin-left: 150px; margin-top: -30px;" class="inline-button">
                        <tr>
                            <td>
                                <asp:Button ID="BtnSubscribe"
                                    OnClick="BtnSubscribe_Click"
                                    runat="server"
                                    Style="float: left; width: 170px !important;"
                                    CssClass="button-style-for-page about-like-cloud-button-for-page inline-button"
                                    Text="Вступить в стартап"
                                    Visible="false"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="BtnUnsubscribe"
                                    OnClick="BtnUnsubscribe_Click"
                                    runat="server"
                                    Style="float: left;"
                                    CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                                    Text="Покинуть стартап"
                                    Visible="false"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
            </div>
        </div>
        <br />
        <br />
        <div class="white-shadow-block radius-like">
            <div class="jumbotron">
                <p style="margin-left: 10px; color: black;" class="lead">
                    <asp:Label runat="server" ID="UsersCount"></asp:Label>
                </p>
                <%-- User images template. --%>
                <div class="visible-gallery users-gallery">
                    <ul class="images users-sliding">
                        <asp:ListView runat="server"
                            ID="LViewUsersFlow">
                            <ItemTemplate>
                                <li class="image">
                                    <div class="container-item transparent-container">
                                        <asp:Image Width="200"
                                            Height="200"
                                            runat="server"
                                            CssClass="startup-image-radius radius-like marginaled image-cover-auto inline-block tag-item not-absolute user-image-padding"
                                            ImageUrl='<%# Eval("UserImageOrDefault") %>'></asp:Image>
                                        <h1 runat="server"
                                            class="tag-item user-paragraph"><%# Eval("Name") %></h1>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
            </div>
        </div>
        <div class="white-shadow-block radius-like">
            <div class="jumbotron">
                <p style="margin-left: 10px; color: black;" class="lead">
                    <asp:Label runat="server" ID="CommentsCount"></asp:Label>
                </p>
                <asp:TextBox TextMode="SingleLine"
                    CssClass="search-box" BorderColor="#808080"
                    Style="margin-bottom: 20px; border-style: inset; height: 70px; margin-left: 10px; max-width: 1300px; width: 1030px !important; margin-right: 6px;"
                    placeholder="Введите комментарий для отправки"
                    runat="server" ID="CommentBox"
                    ForeColor="Black" Height="60"
                    BorderStyle="None"
                    BackColor="Transparent"></asp:TextBox>

                <div style="width: 1030px !important" class="round-div-block about-like-cloud-button-for-page-five button-style-for-page margin-bottom-top-as-usual">
                    <asp:LinkButton runat="server" ForeColor="White" Text='Отправить' ID="BtnSendComment" OnClick="BtnSendComment_Click"></asp:LinkButton>
                </div>
            </div>



            <asp:Panel runat="server" ID="CommentsPanel">



                <asp:ListView runat="server" ID="LViewStartUpComments" OnItemCommand="LViewStartUpComments_ItemCommand">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td class="poster_info td1 hide-for-print">
                                    <p>
                                        <asp:Image Width="100"
                                            Height="100"
                                            runat="server"
                                            CssClass="startup-image-radius radius-like marginaled image-cover-auto"
                                            ImageUrl='<%# Eval("GetCommentImage") %>'></asp:Image>
                                    </p>
                                    <td class="message td2" rowspan="2">
                                        <div class="post_head">
                                            <p class="post-time">
                                                <asp:Label Font-Bold="true" runat="server" Style="font-size: 20px;" Text='<%# Eval("User.Name") %>'></asp:Label>
                                            </p>

                                            <p style="float: right; padding: 3px 2px 4px;">
                                                <asp:Label runat="server" Style="font-size: 13px;" Text='<%# Eval("IsMemberText") %>'></asp:Label>
                                            </p>
                                            <div>
                                                <p style="float: right; padding: 3px 2px 4px;">
                                                    <asp:Label runat="server" Style="font-size: 13px;" Text='<%# Eval("DateTime") %>'></asp:Label>
                                                </p>
                                            </div>
                                            <div class="clear"></div>


                                        </div>
                                        <div class="post_wrap">
                                            <div class="post_body">
                                                <asp:Label runat="server" Text='<%# Eval("CommentText") %>'></asp:Label>
                                            </div>

                                            <asp:LinkButton
                                                Visible='<%# Eval("ICanChange") %>'
                                                CssClass="button-style-for-page about-like-cloud-button-for-page inline-button"
                                                runat="server"
                                                CommandName="DeleteCommentById"
                                                CommandArgument='<%# Eval("Id") %>'>
                                            <span class="vertical-align-text">Удалить комментарий</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton Visible='<%# Eval("IsNotSelfCommentAndICanChange") %>'
                                                CssClass="button-style-for-page about-like-cloud-button-for-page inline-button"
                                                runat="server"
                                                CommandName="BanUserByCommentId"
                                                CommandArgument='<%# Eval("Id") %>'>
                                             <span class="vertical-align-text"><%# Eval("BanUserText") %></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton Visible='<%# Eval("IsNotSelfAndIAmOrganizerAndUserIsMember") %>'
                                                CssClass="button-style-for-page about-like-cloud-button-for-page inline-button"
                                                runat="server"
                                                CommandName="ChangeUserRoleType"
                                                CommandArgument='<%# Eval("Id") %>'>
                                             <span class="vertical-align-text"><%# Eval("ChangeUserRoleTypeText") %></span>
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:ListView>

            </asp:Panel>
        </div>
    </asp:Panel>

    <script src='<%=ResolveUrl("~/Scripts/delete-confirmer.js") %>'
        type="text/javascript">
    </script>
    <script src='<%=ResolveUrl("~/Scripts/image-scroller.js") %>'
        type="text/javascript">
    </script>
    <script src='<%=ResolveUrl("~/Scripts/image-flow-buttons-presenter.js") %>'
        type="text/javascript">
    </script>
</asp:Content>
