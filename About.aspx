<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="StartUpWebAPI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="top-margin-about">
        <div class="lead">
            <h1 class="fade-in align-h1 main-about-title stylecolor">StartUp – это площадка для ваших начинаний</h1>
            <p class="fade-in align-h1 sub-main-about-title stylecolor">Начните стартап уже сегодня. Сотни людей ждут вас</p>
        </div>
        <asp:LoginView runat="server">
            <AnonymousTemplate>
                <div class="centerized-text">
                    <asp:Button Text="Регистрация"
                        CssClass="round-div-block-specified-about about-like-cloud-button fade-in delayed-fade-in suspended-fade-in"
                        runat="server"
                        ID="BtnRegister"
                        OnClick="BtnRegister_Click" Font-Size="3em" />
                </div>
            </AnonymousTemplate>
        </asp:LoginView>
    </div>
    <video id="video-preview" autoplay muted loop>
        <source src="Resources/Исландия.mp4">
    </video>
</asp:Content>
