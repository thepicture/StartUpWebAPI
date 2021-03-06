<%@ Page EnableEventValidation="false" Title="Команды"
    Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeBehind="Teams.aspx.cs" Inherits="StartUpWebAPI.Teams" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <div class="container navbar-fixed-top request-white-bg request-top"
            style="width: 100%; display: flex !important; justify-content: center;">

            <%--style="display: flex; width: 100%; justify-content: center;--%>

            <div style="margin-left: 20px !important; height: 30px; width: 300px !important; float: left !important; margin-right: 30px">
                <asp:TextBox ID="NameBox"
                    Style="height: 40px; width: 300px !important; float: left !important; margin-right: 6px"
                    TextMode="SingleLine" CssClass="search-box" runat="server"
                    ForeColor="Black" Height="60" BorderColor="#808080" BorderStyle="NotSet"
                    BackColor="Transparent"> </asp:TextBox>
            </div>

            <%-- The members dropdownbox. --%>
            <div style="margin-right: 30px;" class="dropdown-box-additions">
                <div class="dropdown-container dropdown no-margin"
                    id="members-container">
                    <div class="dropdown-button border-radius noselect">
                        <div class="dropdown-label">Число участников</div>
                        <div class="dropdown-quantity">(<span class="quantity">Любое</span>)</div>
                        <i class="fa fa-filter"></i>
                    </div>
                    <div class="dropdown-list static-height-dropdown" hidden>
                        <div>
                            <asp:Button runat="server" CssClass="green-and-blue-button"
                                OnClientClick="flushDropDownBox(`#members-container`)"
                                ID="BtnClearMembers"
                                Text="Очистить" />
                            <input type="search" style="border: 1px solid" placeholder="Поиск по кол-ву"
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
            <div style="margin-right: 20px;" class="dropdown-box-additions">
                <div class="dropdown-container dropdown no-margin"
                    id="regions-container">
                    <div class="dropdown-button border-radius noselect">
                        <div class="dropdown-label">Регион</div>
                        <div class="dropdown-quantity">(<span class="quantity">Любой</span>)</div>
                        <i class="fa fa-filter"></i>
                    </div>
                    <div class="dropdown-list static-height-dropdown" hidden>
                        <div>
                            <asp:Button runat="server" CssClass="green-and-blue-button"
                                OnClientClick="flushDropDownBox(`#regions-container`)"
                                ID="BtnClearRegions"
                                Text="Очистить" />
                            <input type="search" style="border: 1px solid" placeholder="Поиск по региону"
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

            <div style="float: left !important; margin-top: 5px;">
                <asp:Button Text="Искать" Style="width: 160px !important;"
                    CssClass="button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                    runat="server" ID="BtnSearch" OnClick="BtnSearch_Click" />
            </div>
            <div>
                <asp:Button Text="Очистить фильтры"  Style="margin-left: 20px; margin-top: 5px !important;"
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
                    <%-- New design --%>
                    <asp:LinkButton runat="server"
                        ID="BtnStartUpInfo"
                        href='<%#"/TeamInfo?id=" + Eval("Id") %>'
                        onmouseenter='<%# "showDescription(`team-" + Eval("Id")  + "`, `" + Eval("RestrictedDescription") + "`)"%>'
                        onmouseleave="hideDescription()"
                        class='<%# "team-" + Eval("Id") %>'>
                                <div class="startup-panel radius-like container-item">
                                    <img class="startup-image radius-like image-cover-auto"
                                         src='<%# Eval("ImagePreview") %>' alt='<%# Eval("Name") %>' />
                                    <h1 class="tag-item item-name"><%# Eval("CroppedName") %></h1>
                                    <%-- Div gets tag-item class to be in the container item. --%>
                                    <div class="tag-item inherit-font-size marginated-tag-item not-on-top">
                                          <h1 class="tag-item category-element"><%# "Участников: " + Eval("CountOfMembers") %></h1>
                                          <h1 class="tag-item blue-sign"
                                              runat="server"
                                              visible='<%# ((HashSet<StartUpWebAPI.Entities.TeamOfUser>)Eval("TeamOfUser")).Any(s => s.User.Login.Equals(User.Identity.Name) && !s.RoleType.Name.Equals("Участник"))%>'><%# Eval("MyRole") %></h1>
                                    </div>
                        <%-- The team is ended sign. --%>
                                    <div class="tag-item gray-gradient radius-like"></div>
                                    <div class="sign-my-startup tag-item align-right"
                                        runat="server"
                                        Visible='<%#((HashSet<StartUpWebAPI.Entities.TeamOfUser>)Eval("TeamOfUser"))
                                            .Count >= Convert.ToInt32(Eval("MaxMembersCount")) %>'>
                                        <asp:Label runat="server"
                                            class="rotated-text">MAX</asp:Label>
                                    </div>
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
