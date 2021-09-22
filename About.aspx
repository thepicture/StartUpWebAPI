<%@ Page Title="О проекте" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="StartUpWebAPI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 align="center" style="color:white" class="title-page-margin"><%: Title %></h2>
    <div class="lead">
        <h1 style="font-size: 4em">StartUp - это площадка для всех стартаперов.</h1>
        <p style="font-size: 2em">Начните стартап уже сегодня. Сотни людей ждут вас!</p>
    </div>
    <video id="video-preview" autoplay muted loop>
        <source src="/Resources/Исландия.mp4">
    </video>
</asp:Content>
