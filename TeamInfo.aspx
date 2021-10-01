<%@ Page Language="C#" Title="Информация о команде" AutoEventWireup="true" CodeBehind="TeamInfo.aspx.cs" MasterPageFile="~/Site.Master" Inherits="StartUpWebAPI.TeamInfo" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">

    <asp:Panel runat="server">

        <div class="jumbotron  " style="padding-bottom: 0px !important; margin-bottom: -30px !important;">


            <h1>
                <asp:Label runat="server" ID="MainName" Style="color: black;" CssClass="centerized-text"></asp:Label></h1>
            <p class="lead">
            </p>

            <div style="width: 200px; height: 200px; float: left;">
                <asp:Image ID="MainImage" runat="server" CssClass="radius-like swimming-content image-cover-auto" Height="200" Width="200" />
            </div>


            <asp:Panel Style="display: block" ID="PanelMyStartups" runat="server">

                <table align="center" runat="server" cellpadding="5" style="border-spacing: 30px !important;" cellspacing="9">
                    <tr cellspacing="9" runat="server">
                        <th runat="server" style="margin-right: 20px;">
                            <label style="display: block; font-size: 17px; color: darkslateblue">Название:</label>
                            <asp:Label ID="Name" runat="server" ForeColor="Black"></asp:Label>
                        </th>

                        <th>
                            <label style="display: block; font-size: 17px; color: darkslateblue">Максимиум участников:</label>
                            <asp:Label ID="MaxMembersCount" runat="server" ForeColor="Black"></asp:Label>
                        </th>

                        <th>
                            <label style="display: block; font-size: 17px; color: darkslateblue">Участников:</label>
                            <asp:Label ID="CountOfMembers" runat="server" ForeColor="Black"></asp:Label>

                        </th>
                    </tr>



                    <tr>

                        <th>
                            <label style="display: block; font-size: 17px; color: darkslateblue">Колличество стартапов:</label>
                            <asp:Label ID="CountOfStartUps" runat="server" ForeColor="Black"></asp:Label>

                        </th>
                        <th>
                            <label style="display: block; font-size: 17px; color: darkslateblue">Организатор:</label>
                            <asp:Label ID="Creator" runat="server" ForeColor="Black"></asp:Label>
                        </th>

                        <th>
                            <label style="display: block; font-size: 17px; color: darkslateblue">Дата создания:</label>
                            <asp:Label ID="DateOfCreation" runat="server" ForeColor="Black"></asp:Label>
                        </th>
                         <th>
                            <label style="display: block; font-size: 17px; color: darkslateblue">Регион:</label>
                            <asp:Label ID="Region" runat="server" ForeColor="Black"></asp:Label>
                        </th>
                    </tr>
                </table>




                <asp:Panel Style="display: block;" runat="server" ID="PTeamEdit" Visible="false">
                    <table style="margin-left: -10px; margin-top: -20px;">
                        <tr>
                            <td>

                                <asp:LinkButton runat="server" Text='Изменить информацию'
                                    Style="float: left; margin-left: 170px !important;"
                                    ForeColor="White"
                                    CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                                    ID="LinkButtonModifyTeam"
                                    href='<%# "/AddTeam?id=" + Convert.ToInt32(Eval("Id")) %>'
                                    OnClick="LinkButtonModifyTeam_Click"></asp:LinkButton>

                            </td>

                            <td>

                                <asp:LinkButton runat="server" Text='Удалить команду'
                                    CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                                    ForeColor="White"
                                    ID="BtnDeleteTeam"
                                    OnClick="BtnDeleteTeam_Click"
                                    OnClientClick="return getDeleteItemState(`команду`);"></asp:LinkButton>
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
                                Text="Вступить в команду"
                                Visible="false"></asp:Button>
                        </td>
                        <td>

                            <asp:Button ID="BtnUnsubscribe"
                                OnClick="BtnUnsubscribe_Click"
                                runat="server"
                                Style="float: left;"
                                CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                                Text="Покинуть команду"
                                Visible="false"></asp:Button>

                        </td>


                    </tr>

                </table>


            </asp:Panel>
            <br />
        </div>



        <div class="jumbotron ">
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
                <asp:LinkButton runat="server"
                    Text='Отправить'
                    ForeColor="White"
                    ID="BtnSendComment"
                    OnClick="BtnSendComment_Click"></asp:LinkButton>
            </div>
        </div>
        <br />

        <asp:Panel runat="server" ID="CommentsPanel">
            <asp:ListView runat="server" ID="LViewTeamComments">
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
                                            <asp:Label Font-Bold="true" Style="font-size: 20px;" runat="server" Text='<%# Eval("User.Name") %>'></asp:Label>
                                        </p>
                                        <p style="float: right; padding: 3px 2px 4px;">
                                            <asp:Label runat="server" Style="font-size: 13px;" Text='<%# Eval("CreationDate") %>'></asp:Label>
                                        </p>
                                        <div class="clear"></div>

                                    </div>
                                    <div class="post_wrap">
                                        <div class="post_body">
                                            <asp:Label runat="server" Text='<%# Eval("CommentText") %>'></asp:Label>
                                        </div>



                                    </div>
                                </td>
                            </td>
                        </tr>
                    </table>

                </ItemTemplate>

            </asp:ListView>

        </asp:Panel>

    </asp:Panel>
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
    <script src='<%=ResolveUrl("~/Scripts/delete-confirmer.js") %>'
        type="text/javascript">
    </script>
</asp:Content>
