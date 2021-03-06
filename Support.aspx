<%@ Page
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Support.aspx.cs"
    Inherits="StartUpWebAPI.Support" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" Style="padding-left: 50px; padding-right: 50px;">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <br />
    <br />
    <br />
    <div id="frame" style="margin-right: 0 !important; margin-left: 0 !important">
        <div id="sidepanel">


            <div id="contacts" style="background-color: #054ba0;">
                <ul>
                    <asp:ListView runat="server" ID="ContactsView">
                        <EmptyDataTemplate>
                            <p>Контактов нет</p>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <asp:HyperLink
                                runat="server"
                                NavigateUrl='<%# Eval("Id", "~/Support.aspx?receiverId={0}") %>'
                                Style="color: white;">
                                <li class="<%#Request.QueryString["receiverId"] != null && Request.QueryString["receiverId"] == Eval("Id").ToString() ? "contact active" : "contact " %>">
                                    <div class="wrap">
                                        <asp:Image runat="server"
                                            ImageUrl='<%# (int)Eval("Id") == Me.Id ? Eval("UserImageInCommentOrDefault") : Eval("UserImageInCommentOrDefault") %>'
                                            AlternateText="Фотография получателя"></asp:Image>
                                        <div class="meta">
                                            <p class="name"><%# Eval("Name") %></p>
                                            <p class="preview"><span style='<%# (bool)Eval("IsLastMessageMine") ? "display: visible;": "display: none;" %>'>Вы:</span><%# Eval("LastMessage.Text") %></p>
                                        </div>
                                    </div>
                                </li>
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>

        </div>
        <div class="content">
            <div class="contact-profile">
                <asp:Image ID="ReceiverImage"
                    runat="server"
                    AlternateText="Фотография получателя"></asp:Image>
                <p style="margin-top: 20px;"><%: Receiver.Name %></p>

            </div>
            <div class="messages" style="background: #c9d4f2;">
                <ul>
                    <asp:ListView
                        runat="server"
                        ID="MessagesView">
                        <EmptyDataTemplate>
                            <p style="text-align: center;">Сообщений нет</p>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <li class="<%# (int)Eval("ReceiverId") == Me.Id ? "sent" : "replies" %>">
                                <asp:Image runat="server"
                                    ImageUrl='<%# (int)Eval("ReceiverId") == Me.Id ? Eval("User.UserImageInCommentOrDefault") : Eval("Sender.UserImageInCommentOrDefault") %>'
                                    AlternateText="Фотография получателя"></asp:Image>
                                <p><%# Eval("Text") %></p>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
            <div class="message-input">
                <div class="wrap">
                    <form>
                        <asp:TextBox runat="server" ID="textBox" Style="max-width: inherit;" type="text" placeholder="Набрать текст..." />
                        <button type="submit" name="text" class="submit"><i class="fa fa-comment" aria-hidden="true"></i></button>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src='//production-assets.codepen.io/assets/common/stopExecutionOnTimeout-b2a7b3fe212eaa732349046d8416e00a9dec26eb7fd347590fbced3ab38af52e.js'></script>
    <script src='https://code.jquery.com/jquery-2.2.4.min.js'></script>
    <script>
        $(".messages").animate({ scrollTop: $(document).height() }, "fast");

        $("#profile-img").click(function () {
            $("#status-options").toggleClass("active");
        });

        $(".expand-button").click(function () {
            $("#profile").toggleClass("expanded");
            $("#contacts").toggleClass("expanded");
        });
//# sourceURL=pen.js</script>
</asp:Content>
