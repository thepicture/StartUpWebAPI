<%@ Page Title="О проекте" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="StartUpWebAPI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="lead">
        <h1 style="font-size: 4em;" class="fade-in align-h1">StartUp – это площадка для ваших начинаний</h1>
        <p style="font-size: 1.5em;" class="fade-in align-h1">Начните стартап уже сегодня. Сотни людей ждут вас</p>
    </div>
    <asp:LoginView runat="server">
        <AnonymousTemplate>
            <div class="centerized-text">
                <asp:Button Text="Регистрация" CssClass="round-div-block-specified-about about-like-cloud-button fade-in delayed-fade-in"
                    runat="server" ID="BtnRegister" OnClick="BtnRegister_Click" Font-Size="3em" Height="70" />
            </div>
        </AnonymousTemplate>
    </asp:LoginView>
    <video id="video-preview" autoplay muted loop>
        <source src="Resources/Исландия.mp4">
    </video>
</asp:Content>
