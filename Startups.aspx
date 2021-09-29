<%@ Page Language="C#" Title="Стартапы" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Startups.aspx.cs" Inherits="StartUpWebAPI.Startups" EnableEventValidation="false" %>

<asp:Content ID="SearchContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex;">
        <div class="navbar navbar-inverse navbar-fixed-top request-white-bg" style="top: inherit;">
            <div style="display: flex; width: 100%; justify-content: center;">


                <asp:UpdatePanel runat="server" ID="UpdateFiltration" UpdateMode="Conditional" style="margin-left: 10px;">
                    <ContentTemplate>

                        <asp:TextBox ID="NameBox"
                            Style="height: 30px; width: 300px !important; float: left !important; margin-right: 6px"
                            TextMode="SingleLine" CssClass="search-box" runat="server"
                            ForeColor="Black" Height="60" BorderColor="#808080" BorderStyle="NotSet"
                            BackColor="Transparent"> </asp:TextBox>

                        <asp:DropDownList ID="ComboCategories"
                            class="form-control"
                            Style="height: 30px !important; width: 180px; margin-left: 5px; margin-right: 10px; float: left !important; vertical-align: middle;" runat="server"
                            ForeColor="Black" Height="60"
                            BackColor="Transparent">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ComboMaxMembers"
                            class="form-control"
                            Style="height: 30px; width: 180px; margin-right: 6px; vertical-align: middle; float: left !important;" runat="server"
                            ForeColor="Black" Height="60"
                            BackColor="Transparent">
                        </asp:DropDownList>
                        <div class="inline-button centerized-div">
                            <asp:CheckBox runat="server"
                                Text="Показывать актуальные"
                                CssClass="search-text-box"
                                Checked="false"
                                ID="ActualBox" />
                            <br />
                            <asp:CheckBox runat="server"
                                Text="Показывать завершенные"
                                CssClass="search-text-box"
                                Checked="true"
                                ID="DoneBox" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button Text="Искать" Style="width: 120px !important;" CssClass="button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                    runat="server" ID="BtnSearch" OnClick="BtnSearch_Click" />
                <asp:Button Text="Очистить фильтры" Style="margin-left: -2px;" CssClass="button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                    runat="server" ID="BtnClear" OnClick="BtnClear_Click" />
            </div>
        </div>
        <%--ListView for startups presentation.--%>
        <asp:Panel HorizontalAlign="Center" runat="server" Style="margin-top: 100px; z-index: 0;">
            <asp:ListView ID="StartupsView" runat='server' OnItemCommand="StartupsView_ItemCommand">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="StartUpClicked" CommandArgument='<%# Eval("Id") %>'>
                        <div class="startup-panel radius-like container-item">
                            <img class="startup-image radius-like image-cover-auto" src='<%# Eval("ImagePreview") %>' alt='<%# Eval("Name") %>' />
                            <h1 class="tag-item" style="margin-left: 20px;margin-bottom: 45px;"><%# Eval("Name") %></h1>
                            <h1 class="tag-item" style="margin-left: 20px;margin-bottom: 20px;color:#d4d4dd; font-size:1.15em;"><%# Eval("SplittedCategory") %></h1>
                        <div class="tag-item gray-gradient radius-like" style="z-index:64;opacity:.8;"></div>
                    </div>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:ListView>
        </asp:Panel>
    </div>
</asp:Content>
