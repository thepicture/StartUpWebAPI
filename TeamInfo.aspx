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
                        <asp:Label runat="server" Text='<%# Eval("DateTime") %>'></asp:Label>
                        <asp:Label runat="server" Text='<%# Eval("CommentText") %>'></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
