<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Teams.aspx.cs" Inherits="StartUpWebAPI.Teams" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--ListView for startups presentation.--%>
    <asp:Panel HorizontalAlign="Center" runat="server">
        <%--  <asp:ListView ID="TeamsView" runat='server' OnItemCommand="TeamsView_ItemCommand">
            <ItemTemplate>
                <asp:Panel runat="server">
                    <div class="team-panel radius-like gradientable-purple pushable">
                        <div>
                            <asp:Label CssClass="startup-name" runat="server" Font-Bold="true" Font-Size="Large" Text='<%# Eval("Name") %>'></asp:Label>
                        </div>
                        <asp:Image Width="100" Height="100" runat='server' CssClass="startup-image-radius radius-like marginaled" ImageUrl='<%# Eval("ImagePreview") %>' />
                        <br />
                        <asp:Label CssClass="startup-name" runat="server" Text='<%# Eval("FirstCreator") %>'></asp:Label>
                        <br />
                        <asp:Label CssClass="startup-name" runat="server" Font-Bold="true" Text='<%# "Дата создания: " + Eval("CreationDate") %>'></asp:Label>
                        <br />
                        <asp:LinkButton Text="Подробнее" CssClass="round-div-block simple-cloud-button-teams" ID="BtnRedirect"
                            runat="server" CommandName="TeamClicked" CommandArgument='<%# Eval("Id") %>' Style="width: fit-content;" />
                    </div>
                </asp:Panel>
            </ItemTemplate>
        </asp:ListView>--%>
        <%--ListView for teams presentation.--%>
        <asp:Panel HorizontalAlign="Center" runat="server">
            <asp:ListView ID="TeamsView" runat='server' OnItemCommand="TeamsView_ItemCommand">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="TeamClicked" CommandArgument='<%# Eval("Id") %>'>
                    <div class="startup-panel radius-like container-item ">
                            <img class="startup-image radius-like image-cover-auto" src='<%# Eval("ImagePreview") %>' alt='<%# Eval("Name") %>' />
                        <h1 class="tag-item" style="margin-left: 20px;margin-bottom: 45px; z-index:600;"><%# Eval("Name") %></h1>
                        <h1 class="tag-item" style="margin-left: 20px;margin-bottom: 20px;color:#d4d4dd; font-size:1.15em; z-index:129;"><%# (string) Eval("CountOfMembers") + " участников" %></h1>
                        <div class="tag-item gray-gradient radius-like" style="z-index:127;opacity:.8"></div>
                    </div>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:ListView>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
