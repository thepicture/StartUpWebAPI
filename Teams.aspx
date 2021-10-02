<%@ Page EnableEventValidation="false" Title="Команды"
    Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeBehind="Teams.aspx.cs" Inherits="StartUpWebAPI.Teams" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <div class=" container  navbar-fixed-top request-white-bg"  style="top: inherit; width: 100%; flex-wrap:wrap;  display:flex !important; justify-content: center;" >
           
         <%--style="display: flex; width: 100%; justify-content: center;--%>
           
            

            <div   Style=" margin-left:20px !important; height: 30px; width: 300px !important; float: left !important; margin-right: 15px">
                  <asp:TextBox ID="NameBox"
                    Style="height: 40px; width: 300px !important; float: left !important; margin-right: 6px"
                    TextMode="SingleLine" CssClass="search-box" runat="server"
                    ForeColor="Black" Height="60" BorderColor="#808080" BorderStyle="NotSet"
                    BackColor="Transparent"> </asp:TextBox>
            </div>
                      
                    
                        <div  Style=" width: max-content; height:30px; margin-right: 15px; vertical-align: middle; float: left !important;"  class="request-overflow-y">
                    <asp:CheckBoxList ID="ComboMaxMembers"
                        class="form-control"
                        Style="width: max-content; margin-right: 6px; vertical-align: middle; float: left !important;"
                        runat="server"
                        ForeColor="Black"
                        Height="60"
                        BackColor="Transparent">
                    </asp:CheckBoxList>
                </div>
                    
                              <div Style= "height:50px !important; align-content:center; width: max-content; height: 100px; margin-left: 5px; margin-right: 10px; float: left !important; vertical-align: middle;"  >
                    <div class="dropdown-container" Style =" margin: 0 0 0 0" >
                        <div  class="dropdown-button noselect">
                            <div  class="dropdown-label">Регион</div>
                            <div class="dropdown-quantity">(<span class="quantity">Любой регион</span>)</div>
                            <i class="fa fa-filter"></i>
                        </div>
                        <div class="dropdown-list"
                            style="display: none !important; height: 500px;">
                            <div>
                                <input type="search" placeholder="Поиск по региону"
                                    class="dropdown-search request-static-height" />
                            </div>
                            <div style="background-color: white;">
                                <ul class="ul-stylebox" runat="server" id="RegionBox">
                                    <asp:ListView runat="server" ID="RegionsView">
                                        <ItemTemplate>
                                            <li>
                                                <input name='<%#Eval("Name") %>' type='checkbox' runat='server' />
                                                <label class="region-contents" runat="server" for='<%#Eval("Name") %>'><%#Eval("Name") %></label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                 
            <div  Style="float:left !important;">
   <asp:Button Text="Искать" Style="width: 120px !important;"
                    CssClass="button-style-for-page about-like-cloud-button-for-page margin-bottom-top-as-usual"
                    runat="server" ID="BtnSearch" OnClick="BtnSearch_Click" />

            </div>
                      
                    <div >
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
