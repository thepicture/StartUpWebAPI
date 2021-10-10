<%@ Page Title="Контактные данные"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Contact.aspx.cs"
    Inherits="StartUpWebAPI.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <br />
    <div class="contact-main-div">

        <div class="fade-in round-div-block semi-transparent contact-sub-main-div">

            <h2 class="fade-in title-setting-contact title-page-margin centerized-title"><%: Title %></h2>
            <br />

            <article class=" fade-in title-page-margin contact-article">
                Проект создан командой программистов из многопрофильного колледжа ТИУ
            </article>
            <br />
            <address>
                <strong class="title-page-margin contact-article fade-in">Поддержка:</strong>
                <a class="fade-in" href="mailto:start.up.entertainment.company@gmail.com" target="_blank">start.up.entertainment.company@gmail.com</a>
                <br />
            </address>
        </div>
    </div>
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
