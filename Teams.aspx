<%@ Page EnableEventValidation="false" Title="Команды"
    Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeBehind="Teams.aspx.cs" Inherits="StartUpWebAPI.Teams" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <div class=" container navbar-fixed-top request-white-bg"
            style="top: inherit; width: 100%;  display: flex !important; justify-content: center;">

            <%--style="display: flex; width: 100%; justify-content: center;--%>

            <div style="margin-left: 20px !important; height: 30px; width: 300px !important; float: left !important; margin-right: 30px">
                <asp:TextBox ID="NameBox"
                    Style="height: 40px; width: 300px !important; float: left !important; margin-right: 6px"
                    TextMode="SingleLine" CssClass="search-box" runat="server"
                    ForeColor="Black" Height="60" BorderColor="#808080" BorderStyle="NotSet"
                    BackColor="Transparent"> </asp:TextBox>
            </div>

            <%-- The members dropdownbox. --%>
            <div style="margin-right:30px;" class="dropdown-box-additions">
                <div class="dropdown-container dropdown no-margin"
                    id="members-container">
                    <div class="dropdown-button noselect">
                        <div class="dropdown-label">Число участников</div>
                        <div class="dropdown-quantity">(<span class="quantity">Любое</span>)</div>
                        <i class="fa fa-filter"></i>
                    </div>
                    <div class="dropdown-list static-height-dropdown" hidden>
                        <div>
                            <asp:Button runat="server"
                                OnClientClick="flushDropDownBox(`#members-container`)"
                                ID="BtnClearMembers"
                                Text="Очистить" />
                            <input type="search" placeholder="Поиск по кол-ву"
                                class="dropdown-search request-static-height" />
                        </div>
                        <div class="request-white-bg">
                            <ul class="ul-stylebox">
                                <asp:ListView runat="server"
                                    ID="MembersView"
                                    ItemType="System.String">
                                    <ItemTemplate>
                                        <li>
                                            <input name='<%# Item %>'
                                                type='checkbox'
                                                runat='server'
                                                id="checkBoxTemplate" />
                                            <asp:Label class="element-contents"
                                                runat="server"
                                                for='<%# Item %>'
                                                ID='labelTemplate'
                                                Text='<%# Item %>'>
                                            </asp:Label>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <%-- The regions dropdownbox. --%>
            <div style="margin-right:20px;" class="dropdown-box-additions">
                <div class="dropdown-container dropdown no-margin"
                    id="regions-container">
                    <div class="dropdown-button noselect">
                        <div class="dropdown-label">Регион</div>
                        <div class="dropdown-quantity">(<span class="quantity">Любой</span>)</div>
                        <i class="fa fa-filter"></i>
                    </div>
                    <div class="dropdown-list static-height-dropdown" hidden>
                        <div>
                            <asp:Button runat="server"
                                OnClientClick="flushDropDownBox(`#regions-container`)"
                                ID="BtnClearRegions"
                                Text="Очистить" />
                            <input type="search" placeholder="Поиск по региону"
                                class="dropdown-search request-static-height" />
                        </div>
                        <div class="request-white-bg">
                            <ul class="ul-stylebox">
                                <asp:ListView runat="server" ID="RegionsView">
                                    <ItemTemplate>
                                        <li>
                                            <input name='<%# Eval("Name") %>'
                                                type='checkbox'
                                                runat='server'
                                                id="checkBoxTemplate" />
                                            <asp:Label class="element-contents"
                                                runat="server"
                                                for='<%# Eval("Name")%>'
                                                ID="labelTemplate"
                                                Text='<%# Eval("Name") %>'></asp:Label>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <div  float: left !important;">
                <asp:Button Text="Искать" Style="width: 120px !important;"
                    CssClass="button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                    runat="server" ID="BtnSearch" OnClick="BtnSearch_Click" />

            </div>

            <div>
                <asp:Button Text="Очистить фильтры" Style="margin-left: 2px;"
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
    <script src='<%=ResolveUrl("~/Scripts/descriptor-presenter.js") %>'
        type="text/javascript">
    </script>
    <script src='https://cdnjs.cloudflare.com/ajax/libs/lodash.js/3.5.0/lodash.min.js'
        type="text/javascript">
    </script>
    <script src='<%=ResolveUrl("~/Scripts/dropdown-list-presenter.js") %>'
        type="text/javascript">
    </script>
    <script src='<%=ResolveUrl("~/Scripts/dropdown-list-flusher.js") %>'
        type="text/javascript">
    </script>
</asp:Content>
