<%@ Page Language="C#"
    Title="Стартапы"
    AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeBehind="Startups.aspx.cs"
    Inherits="StartUpWebAPI.Startups"
    EnableEventValidation="false" %>

<asp:Content ID="SearchContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex;">
        <div class="navbar navbar-inverse navbar-fixed-top request-white-bg" style="top: inherit;">
            <div style="display: flex; width: 100%; justify-content: center;">

                <asp:UpdatePanel runat="server" ID="UpdateFiltration" UpdateMode="Conditional" style="margin-left: 10px;">
                    <ContentTemplate>

                        <asp:TextBox ID="NameBox"
                            Style="height: 42px; width: 200px !important; float: left !important; margin-right: 6px;"
                            TextMode="SingleLine" CssClass="search-box" runat="server"
                            ForeColor="Black" Height="60" BorderColor="#808080" BorderStyle="NotSet"
                            BackColor="Transparent"> </asp:TextBox>

                        <%-- The members dropdownbox. --%>
                        <div class="dropdown-box-additions">
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
                        <div class="dropdown-box-additions">
                            <div style="width: 160px;" class="dropdown-container dropdown no-margin"
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

                        <%-- The categories dropdownbox. --%>
                        <div class="dropdown-box-additions">
                            <div class="dropdown-container dropdown no-margin"
                                id="categories-container">
                                <div class="dropdown-button noselect">
                                    <div class="dropdown-label">Категории</div>
                                    <div class="dropdown-quantity">(<span class="quantity">Любые</span>)</div>
                                    <i class="fa fa-filter"></i>
                                </div>
                                <div class="dropdown-list static-height-dropdown" hidden>
                                    <div>
                                        <asp:Button runat="server"
                                            OnClientClick="flushDropDownBox(`#categories-container`)"
                                            ID="BtnCleanCategories"
                                            Text="Очистить" />
                                        <input type="search" placeholder="Поиск по категории"
                                            class="dropdown-search request-static-height" />
                                    </div>
                                    <div class="request-white-bg">
                                        <ul class="ul-stylebox">
                                            <asp:ListView runat="server" ID="CategoriesView">
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



                        <div style="margin-left: 2px;" class="mt-3">
                            <a data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample" class="advanced">Дополнительные параметры <i class="fa fa-angle-down"></i></a>
                            <div class="collapse" id="collapseExample">
                                <div class="card card-body">
                                    <div class="row">
                                        <div class=" centerized-div" style="margin-left: 10px;">
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
                                    </div>
                                </div>
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
        <%--ListView for startups presentation.--%>
        <asp:Panel HorizontalAlign="Center" runat="server"
            Style="margin-top: 100px; z-index: 0;">
            <asp:ListView ID="StartupsView" runat='server'>
                <ItemTemplate>
                    <asp:LinkButton runat="server"
                        href='<%#"/StartUpInfo?id=" + Eval("Id") %>'
                        onmouseenter='<%# "showDescription(`start-up-" + Eval("Id")  + "`, `" + Eval("SafeDescription") + "`)"%>'
                        onmouseleave="hideDescription()"
                        class='<%# "start-up-" + Eval("Id") %>'>
                        <div class="startup-panel radius-like container-item">
                            <img class="startup-image radius-like image-cover-auto"
                                src='<%# Eval("ImagePreview") %>' alt='<%# Eval("Name") %>' />
                            <h1 class="tag-item"
                                style="margin-left: 20px;margin-bottom: 45px;"><%# Eval("Name") %></h1>
                            <h1 class="tag-item"
                                style="margin-left: 20px;margin-bottom: 20px;color:#d4d4dd; font-size:1.15em;"><%# Eval("SplittedCategory") %></h1>
                        <div class="tag-item gray-gradient radius-like" style="z-index:64;opacity:.8;"></div>
                        </div>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:ListView>
        </asp:Panel>
    </div>
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
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
