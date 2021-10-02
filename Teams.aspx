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
                            <asp:CheckBoxList ID="ComboCountries"
                                class="form-control"
                                Style="height: 40px; width: max-content; margin-left: 5px; margin-right: 10px; float: left !important; vertical-align: middle;"
                                runat="server"
                                ForeColor="Black" Height="60"
                                BackColor="Transparent">
                            </asp:CheckBoxList>

                        </div>
                        <div class="instructions">(Click to expand and select states to filter)</div>
                        <div class="dropdown-container">
                            <div class="dropdown-button noselect">
                                <div class="dropdown-label">States</div>
                                <div class="dropdown-quantity">(<span class="quantity">Any</span>)</div>
                                <i class="fa fa-filter"></i>
                            </div>
                            <div class="dropdown-list"
                                style="display: none !important;">
                                <input type="search" placeholder="Search states"
                                    class="dropdown-search request-static-height" />
                                <ul class="ul-stylebox" runat="server" id="RegionBox"></ul>
                            </div>
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
            <asp:ListView ID="TeamsView" runat='server'>
                <ItemTemplate>
                    <asp:LinkButton runat="server"
                        href='<%#"/TeamInfo?id=" + Eval("Id") %>'
                        onmouseenter='<%# "showDescription(`team-" + Eval("Id")  + "`, `" + Eval("SafeDescription") + "`)"%>'
                        onmouseleave="hideDescription()"
                        class='<%# "team-" + Eval("Id") %>'>
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
    <script src='https://cdnjs.cloudflare.com/ajax/libs/lodash.js/3.5.0/lodash.min.js'
        type="text/javascript">
    </script>
    <script src='<%=ResolveUrl("~/Scripts/descriptor-presenter.js") %>'
        type="text/javascript">
    </script>
    <script src='<%=ResolveUrl("~/Scripts/region-presenter.js") %>'
        type="text/javascript">
    </script>
</asp:Content>
