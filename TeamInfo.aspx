﻿<%@ Page Language="C#" Title="Информация о команде" AutoEventWireup="true" CodeBehind="TeamInfo.aspx.cs" MasterPageFile="~/Site.Master" Inherits="StartUpWebAPI.TeamInfo" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">

    <asp:Panel runat="server">

        <div class="body-content">

            <div class="div-size body-content section blacked wf-section body-background marginated">

                <h1>
                    <asp:Label runat="server" ID="MainName" CssClass="centerized-text"></asp:Label></h1>
                <p class="lead">
                </p>

                <br />

                <asp:Image ID="MainImage" runat="server" CssClass="radius-like swimming-content image-cover-auto" Height="200" Width="200" />

                <asp:Panel ID="PanelMyTeams" runat="server">
                    <div>
                        <asp:Label ID="Name" runat="server" ForeColor="Black"></asp:Label>
                    </div>
                    <div class="round-div-block">
                        <asp:Label ID="MaxMembersCount" runat="server" ForeColor="Black"></asp:Label>
                    </div>
                    <div class="round-div-block">
                        <asp:Label ID="CountOfMembers" runat="server" ForeColor="Black"></asp:Label>
                    </div>
                    <div class="round-div-block">
                        <asp:Label ID="CountOfStartUps" runat="server" ForeColor="Black"></asp:Label>
                    </div>
                    <div class="round-div-block">
                        <asp:Label ID="Creator" runat="server" ForeColor="Black"></asp:Label>
                    </div>
                    <div class="round-div-block">
                        <asp:Label ID="DateOfCreation" runat="server" ForeColor="Black"></asp:Label>
                    </div>
                    <asp:Panel runat="server" ID="PTeamEdit" Visible="false">
                        <asp:LinkButton runat="server" Text='Изменить информацию'
                            ForeColor="White"
                            CssClass="round-div-block button-style-for-page-two about-like-cloud-button-for-page-two margin-bottom-top-as-usual"
                            ID="LinkButtonModifyTeam"
                            OnClick="LinkButtonModifyTeam_Click"></asp:LinkButton>
                        <asp:LinkButton runat="server" Text='Удалить команду'
                            CssClass="round-div-block simple-cloud-button"
                            ForeColor="White"
                            ID="BtnDeleteTeam"
                            OnClick="BtnDeleteTeam_Click"
                            OnClientClick="return getDeleteItemState(`команду`);"></asp:LinkButton>
                    </asp:Panel>


                    <asp:Button ID="BtnSubscribe"
                        OnClick="BtnSubscribe_Click"
                        runat="server"
                        CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                        Text="Вступить в команду"
                        Visible="false"></asp:Button>
                    <asp:Button ID="BtnUnsubscribe"
                        OnClick="BtnUnsubscribe_Click"
                        runat="server"
                        Style="height: 40px; margin-left: 20px; width: 200px;"
                        CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                        Text="Покинуть команду"
                        Visible="false"></asp:Button>
                </asp:Panel>
                &nbsp;
                <p>
                </p>
            </div>

        </div>

        <div class="jumbotron gradientable paddinated">
            <p style="margin-left: 10px;" class="lead">
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
            <div style="width: 1030px !important" class="round-div-block button-style-for-page-two about-like-cloud-button-for-page-two margin-bottom-top-as-usual">
                <asp:LinkButton runat="server"
                    Text='Отправить'
                    ForeColor="Black"
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
    <script src='<%=ResolveUrl("~/Scripts/delete-confirmer.js") %>'
        type="text/javascript"></script>
</asp:Content>
