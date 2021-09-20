<%@ Page Title="О проекте" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="StartUpWebAPI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 align="center" class="title-page-margin"><%: Title %></h2>
    <h3>StartUp - это площадка для всех стартаперов.</h3>
    <p>Начните стартап уже сегодня. Сотни людей ждут вас!</p>

    <video controls="controls" onloadstart="this.volume=0.0" class="videoAlign" autoplay="autoplay" width="960" height="540">
        <source src="Resources/Исландия.mp4">
    </video>

</asp:Content>
