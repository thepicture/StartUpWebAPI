<%@ Page Title="Контактные данные" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="StartUpWebAPI.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <br />
    <div style="display: flex; justify-content: left; vertical-align: middle;">

        <div class="round-div-block semi-transparent" style="width: 600px; height: 300px; animation: none !important; display: inline-block;">

            <h2 class="title-setting-contact title-page-margin" style="text-align: center;"><%: Title %></h2>
            <br />

            <article style="font-size: 20px; margin: 0 0 0 30px" class="title-page-margin">
                Проект создан командой программистов из многопрофильного колледжа ТИУ
            </article>
            <br />
            <address>
                <strong style="font-size: 20px; margin: 0 0 0 30px" class="title-page-margin">Поддержка:</strong>   <a href="mailto:start.up.entertainment.company@gmail.com" target="_blank">start.up.entertainment.company@gmail.com</a><br />
            </address>
        </div>
    </div>
    <%-- <object type="image/svg+xml" Class="bg-image" data="Resources/Contact-back.svg">
        
    </object>--%>
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
