<%@ Page EnableEventValidation="false" Title="Команды"
    Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeBehind="Teams.aspx.cs" Inherits="StartUpWebAPI.Teams" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="display: flex;">
        <div class="navbar navbar-inverse navbar-fixed-top request-white-bg" style="top: inherit;">
            <div style="display: flex; width: 100%; justify-content: center;">
                <asp:UpdatePanel runat="server" ID="UpdateFiltration" UpdateMode="Conditional" style="margin-left: 10px;">
                    <ContentTemplate>
                        <asp:TextBox ID="NameBox"
                            Style="height: 30px; width: 200px !important; float: left !important; margin-right: 6px"
                            TextMode="SingleLine" CssClass="search-box" runat="server"
                            ForeColor="Black" Height="60" BorderColor="#808080" BorderStyle="NotSet"
                            BackColor="Transparent"> </asp:TextBox>
                        <div class="inline-button request-overflow-y">
                            <asp:CheckBoxList ID="ComboMaxMembers"
                                class="form-control"
                                Style="height: 40px; width: max-content; margin-right: 6px; vertical-align: middle; float: left !important;"
                                runat="server"
                                ForeColor="Black" Height="60"
                                BackColor="Transparent">
                            </asp:CheckBoxList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button Text="Искать" Style="width: 120px !important;"
                    CssClass="button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                    runat="server" ID="BtnSearch" OnClick="BtnSearch_Click" />
                <asp:Button Text="Очистить фильтры" Style="margin-left: -2px;"
                    CssClass="button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                    runat="server" ID="BtnClear" OnClick="BtnClear_Click" />
            </div>
        </div>
    </div>

    <%--ListView for startups presentation.--%>
    <asp:Panel HorizontalAlign="Center" runat="server" Style="margin-top: 100px; z-index: 0;">
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
