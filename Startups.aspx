<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Startups.aspx.cs" Inherits="StartUpWebAPI.Startups" EnableEventValidation="false" %>

<asp:Content ID="SearchContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex;">
        <div class="navbar navbar-inverse navbar-fixed-top request-white-bg" style="top: inherit;">
            <div style="display: flex; width: 100%; justify-content: center;">
                <asp:TextBox ID="NameBox"
                    Style="height: 30px;"
                    TextMode="SingleLine" CssClass="nice-text-box prevent-selection" runat="server"
                    ForeColor="Black" Height="60" BorderStyle="None"
                    BackColor="Transparent"></asp:TextBox>
                <asp:CheckBox runat="server" Text="Только актуальные" Checked="true" ID="ActualBox" />
                <asp:UpdatePanel runat="server" ID="UpdateFiltration" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ComboCategories"
                            Style="height: 30px; vertical-align: middle;" runat="server"
                            ForeColor="Black" Height="60"
                            BackColor="Transparent">
                        </asp:DropDownList>
                          <asp:DropDownList ID="ComboMaxMembers"
                            Style="height: 30px; vertical-align: middle;" runat="server"
                            ForeColor="Black" Height="60"
                            BackColor="Transparent">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button Text="Искать" CssClass="round-div-block simple-cloud-button margin-bottom-top-as-usual"
                    runat="server" ID="BtnSearch" OnClick="BtnSearch_Click" />
                <asp:Button Text="Очистить фильтры" CssClass="round-div-block simple-cloud-button margin-bottom-top-as-usual"
                    runat="server" ID="BtnClear" OnClick="BtnClear_Click" />
            </div>
        </div>
        <%--ListView for startups presentation.--%>
        <asp:Panel HorizontalAlign="Center" runat="server" Style="margin-top: 100px; z-index: 0;">
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
                        <div class="tag-item gray-gradient radius-like" style="z-index:64;opacity:.8"></div>
                    </div>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:ListView>
        </asp:Panel>
    </div>
</asp:Content>
