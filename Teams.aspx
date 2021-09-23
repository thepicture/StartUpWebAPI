<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Teams.aspx.cs" Inherits="StartUpWebAPI.Teams" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--ListView for startups presentation.--%>
    <asp:Panel HorizontalAlign="Center" runat="server">
        <asp:ListView ID="TeamsView" runat='server' OnItemCommand="TeamsView_ItemCommand">
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
        </asp:ListView>
    </asp:Panel>
</asp:Content>
