<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamInfo.aspx.cs" MasterPageFile="~/Site.Master" Inherits="StartUpWebAPI.TeamInfo" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel runat="server">
        <div class="jumbotron gradientable">
            <h1>
                <asp:Label runat="server" ID="MainName" CssClass="centerized-text"></asp:Label></h1>
            <p class="lead">
            </p>

            <asp:Image ID="MainImage" runat="server" CssClass="radius-like swimming-content" Height="200" Width="200" />
            <asp:Panel ID="PanelMyTeams" runat="server">
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
                        CssClass="round-div-block simple-cloud-button"
                        ForeColor="White"
                        ID="LinkButtonModifyTeam"
                        OnClick="LinkButtonModifyTeam_Click"></asp:LinkButton>
                </asp:Panel>
                <asp:Button ID="BtnSubscribe"
                    OnClick="BtnSubscribe_Click"
                    runat="server"
                    CssClass="round-div-block simple-cloud-button"
                    Text="Вступить в команду"
                    Visible="false"></asp:Button>
                <asp:Button ID="BtnUnsubscribe"
                    OnClick="BtnUnsubscribe_Click"
                    runat="server"
                    CssClass="round-div-block simple-cloud-button"
                    Text="Покинуть команду"
                    Visible="false"></asp:Button>
            </asp:Panel>
            &nbsp;
                <p>
                </p>
        </div>
        <div class="jumbotron gradientable">
            <p class="lead">
                <asp:Label runat="server" ID="CommentsCount"></asp:Label>
            </p>
            <div class="round-div-block">
                <div class="unbordered-block">
                    <asp:TextBox TextMode="SingleLine" CssClass="prevent-selection" placeholder="Введите комментарий для отправки" runat="server" ID="CommentBox" ForeColor="Black" Height="60" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                </div>
            </div>
            <div class="round-div-block">
                <asp:LinkButton runat="server" Text='Отправить' ForeColor="Black" ID="BtnSendComment" OnClick="BtnSendComment_Click"></asp:LinkButton>
            </div>
        </div>
        <asp:Panel runat="server" ID="CommentsPanel">
            <asp:ListView runat="server" ID="LViewTeamComments">
                <ItemTemplate>
                    <div class="unbordered-block">
                        <asp:Image Width="100" Height="100" runat="server" CssClass="startup-image-radius radius-like marginaled" ImageUrl='<%# Eval("GetCommentImage") %>'></asp:Image>
                        <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("User.Name") %>'></asp:Label>
                        <asp:Label runat="server" Text='<%# Eval("CreationDate") %>'></asp:Label>
                        <asp:Label runat="server" Text='<%# Eval("CommentText") %>'></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
