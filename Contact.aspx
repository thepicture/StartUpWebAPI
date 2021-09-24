<%@ Page Title="Контактные данные" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="StartUpWebAPI.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex; justify-content: left; vertical-align: middle;">
        <div class="round-div-block semi-transparent" style="width: 600px; height: 300px; animation: none !important; display: inline-block;">

            <h2 class="title-setting-contact" style="text-align: center;"><%: Title %></h2>
            <br />

            <article style="font-size: 20px;">
                Проект создан командой програмистов из
                <br />
                многопрофильного колледжа ТИУ
            </article>
            <br />
            <address>
                <strong>Поддержка:</strong>   <a href="mailto:start.up.entertainment.company@gmail.com" target="_blank">start.up.entertainment.company@gmail.com</a><br />
            </address>
        </div>
    </div>
    <%-- <object type="image/svg+xml" Class="bg-image" data="Resources/Contact-back.svg">
        
    </object>--%>
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
