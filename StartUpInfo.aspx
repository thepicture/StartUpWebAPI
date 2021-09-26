<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="StartUpInfo.aspx.cs" Inherits="StartUpWebAPI.StartUpInfo" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel runat="server">
        <div class="jumbotron gradientable">
            <h1>
                <asp:Label runat="server" ID="MainName" CssClass="centerized-text"></asp:Label></h1>
            <p class="lead">
            </p>

            <div style="width: 200px; height: 200px;">
                <asp:Image ID="MainImage" runat="server" CssClass="radius-like swimming-content image-cover-auto" />
            </div>
            <asp:Panel ID="PanelMyStartups" runat="server">
                <div class="round-div-block">
                    <asp:Label ID="Name" runat="server" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label ID="MaxMembersCount" runat="server" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label ID="CountOfMembers" runat="server" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label ID="CountOfTeams" runat="server" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label ID="Creator" runat="server" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label ID="DateOfCreation" runat="server" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label ID="Description" runat="server" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label ID="Category" runat="server" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label ID="IsActual" runat="server" ForeColor="Black"></asp:Label>
                </div>
                <asp:Panel runat="server" ID="PStartupEdit" Visible="false">
                    <asp:LinkButton runat="server" Text='Изменить информацию'
                         CssClass="round-div-block button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                        ForeColor="White"
                        ID="LinkButtonModifyStartUp"
                        OnClick="LinkButtonModifyStartUp_Click"></asp:LinkButton>
                </asp:Panel>
                <asp:Button ID="BtnSubscribe"
                    OnClick="BtnSubscribe_Click"
                    runat="server"
                    CssClass="round-div-block simple-cloud-button"
                    Text="Вступить в стартап"
                    Visible="false"></asp:Button>
                <asp:Button ID="BtnUnsubscribe"
                    OnClick="BtnUnsubscribe_Click"
                    runat="server"
                    CssClass="round-div-block simple-cloud-button"
                    Text="Покинуть стартап"
                    Visible="false"></asp:Button>
            </asp:Panel>
            &nbsp;
                <p>
                </p>
        </div>

        <div class="jumbotron gradientable">
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
                <asp:LinkButton runat="server" Text='Отправить' ForeColor="Black" ID="BtnSendComment" OnClick="BtnSendComment_Click"></asp:LinkButton>
            </div>
        </div>



        <asp:Panel runat="server" ID="CommentsPanel">
            <asp:ListView runat="server" ID="LViewStartUpComments">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td class="poster_info td1 hide-for-print">
                                <p>
                                    <asp:Image Width="100" Height="100" runat="server" CssClass="startup-image-radius radius-like marginaled" ImageUrl='<%# Eval("GetCommentImage") %>'></asp:Image>
                                </p>
                                <td class="message td2" rowspan="2">
                                    <div class="post_head">
                                        <p class="post-time">
                                            <asp:Label Font-Bold="true" runat="server" Style="font-size: 20px;" Text='<%# Eval("User.Name") %>'></asp:Label>
                                        </p>

                                        <p style="float: right; padding: 3px 2px 4px;">
                                            <asp:Label runat="server" Style="font-size: 13px;" Text='<%# Eval("DateTime") %>'></asp:Label>
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
</asp:Content>
