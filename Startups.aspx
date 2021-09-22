<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Startups.aspx.cs" Inherits="StartUpWebAPI.Startups" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--ListView for startups presentation.--%>
    <asp:Panel HorizontalAlign="Center" runat="server">
        <asp:Panel runat="server" ID="FiltrationPanel">
            <div class="unbordered-block">
                <asp:TextBox ID="NameBox" OnTextChanged="NameBox_TextChanged"
                    Style="background-color: deeppink; color: white;"
                    TextMode="SingleLine" CssClass="prevent-selection" runat="server"
                    ForeColor="Black" Height="60" BorderStyle="None"
                    BackColor="Transparent"
                    AutoPostBack="true"></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:ListView ID="StartupsView" runat='server' OnItemCommand="StartupsView_ItemCommand">
            <ItemTemplate>
                <asp:Panel runat="server" Height="20">
                    <div class="startup-panel radius-like gradientable jumpable">
                        <div>
                            <asp:Label CssClass="startup-name" runat="server" Font-Bold="true" Font-Size="Large" Text='<%# Eval("Name") %>'></asp:Label>
                        </div>
                        <asp:Image Width="100" Height="100" runat='server' CssClass="startup-image-radius radius-like marginaled" ImageUrl='<%# Eval("ImagePreview") %>' />
                        <br />
                        <asp:Label CssClass="startup-name" runat="server" Text='<%# Eval("FirstCreator") %>'></asp:Label>
                        <br />
                        <asp:Label CssClass="startup-name" runat="server" Font-Bold="true" Text='<%# "Категория: " + Eval("Category.Name") %>'></asp:Label>
                        <br />
                        <asp:LinkButton Text="Подробнее" CssClass="round-div-block simple-cloud-button" ID="BtnRedirect"
                            runat="server" CommandName="StartUpClicked" CommandArgument='<%# Eval("Id") %>' Style="width: fit-content;" />
                </asp:Panel>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
</asp:Content>
