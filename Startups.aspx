<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Startups.aspx.cs" Inherits="StartUpWebAPI.Startups" EnableEventValidation="false" %>

<asp:Content ID="SearchContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="FiltrationStartupsPanel" Style="width: 100%;z-index:99999;">
        <div class="unbordered-block" style="display: flex; justify-content: space-between; position: fixed; top: inherit; background-color: white;">
            <asp:TextBox ID="NameBox"
                Style="height: 30px; vertical-align: middle;"
                TextMode="SingleLine" CssClass="nice-text-box prevent-selection" runat="server"
                ForeColor="Black" Height="60" BorderStyle="None"
                BackColor="Transparent"></asp:TextBox>
            <asp:UpdatePanel runat="server" ID="UpdateFiltration" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ComboCategories"
                        Style="height: 30px; vertical-align: middle;" runat="server"
                        ForeColor="Black" Height="60" BorderStyle="None"
                        BackColor="Transparent">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Button Text="Искать" CssClass="round-div-block simple-cloud-button"
                runat="server" ID="BtnSearch" OnClick="BtnSearch_Click" />
            <br />
        </div>
    </asp:Panel>
    <br />
    <%--ListView for startups presentation.--%>
    <asp:Panel HorizontalAlign="Center" runat="server">
        <asp:ListView ID="StartupsView" runat='server' OnItemCommand="StartupsView_ItemCommand">
            <ItemTemplate>
                <%--   <asp:Panel runat="server" Height="20">
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
                            runat="server" CommandName="StartUpClicked" CommandArgument='<%# Eval("Id") %>' style="width: fit-content;" />
                </asp:Panel>--%>
                <asp:LinkButton runat="server" CommandName="StartUpClicked" CommandArgument='<%# Eval("Id") %>'>
                    <div class="startup-panel radius-like container-item ">
                            <img class="startup-image radius-like image-cover-auto" src='<%# Eval("ImagePreview") %>' alt='<%# Eval("Name") %>' />
                        <h1 class="tag-item" style="margin-left: 20px;margin-bottom: 45px;"><%# Eval("Name") %></h1>
                        <h1 class="tag-item" style="margin-left: 20px;margin-bottom: 20px;color:#d4d4dd; font-size:1.15em;"><%# Eval("SplittedCategory") %></h1>
                        <div class="tag-item gray-gradient radius-like" style="z-index:555;opacity:.8"></div>
                    </div>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
</asp:Content>
