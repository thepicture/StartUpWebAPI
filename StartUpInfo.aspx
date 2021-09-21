<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="StartUpInfo.aspx.cs" Inherits="StartUpWebAPI.StartUpInfo" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel runat="server">
        <div class="jumbotron gradientable">
            <p class="lead">
                <asp:Label runat="server" ID="MainName"></asp:Label>
            </p>
            <asp:Panel runat="server" ID="PanelMyTeams">
                <div class="round-div-block">
                    <asp:Label runat="server" ID="Name" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label runat="server" ID="CountOfMembers" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label runat="server" ID="CountOfTeams" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label runat="server" ID="Creator" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label runat="server" ID="DateOfCreation" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label runat="server" ID="Description" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label runat="server" ID="Category" ForeColor="Black"></asp:Label>
                </div>
                <div class="round-div-block">
                    <asp:Label runat="server" ID="IsActual" ForeColor="Black"></asp:Label>
                </div>
            </asp:Panel>
            &nbsp;
        </div>
        <div class="jumbotron gradientable">
            <p class="lead">
                <asp:Label runat="server" ID="CommentsCount"></asp:Label>
            </p>
            <div class="round-div-block">
                <div class="unbordered-block">
                    <asp:TextBox TextMode="SingleLine" CssClass="prevent-selection" ToolTip="Введите комментарий для отправки" runat="server" ID="CommentBox" ForeColor="Black" Height="60" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                </div>
            </div>
            <div class="round-div-block">
                <asp:LinkButton runat="server" Text='Отправить' ForeColor="Black" ID="BtnSendComment" OnClick="BtnSendComment_Click"></asp:LinkButton>
            </div>
        </div>
        <asp:Panel runat="server" ID="CommentsPanel">
            <asp:ListView runat="server" ID="LViewStartUpComments">
                <ItemTemplate>
                    <div class="unbordered-block">
                        <asp:Image runat="server" CssClass="startup-image-radius radius-like marginaled" ImageUrl="<%# Eval("GetCommentImage") %>"></asp:Image>
                        <asp:Label runat="server" Text="<%# Eval("CommentText") %>"></asp:Label>
                        <asp:Label runat="server" Text="<%# Eval("DateTime") %>"></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
