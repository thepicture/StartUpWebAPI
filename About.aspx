﻿<%@ Page Title="О проекте" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="StartUpWebAPI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 align="center" style="color: white" class="title-page-margin "><%: Title %></h2>
    <div class="lead">
        <h1 style="font-size: 4em" class="fadeIn alignh1">StartUp – это площадка для ваших начинаний</h1>
        <p style="font-size: 1.5em" class="fadeIn alignh1"">Начните стартап уже сегодня. Сотни людей ждут вас</p>
    </div>
    <asp:Panel ID="AnonContent" runat="server">
        <div class="centerized-text">
            <asp:Button Text="Регистрация" CssClass="round-div-block simple-cloud-button"
                runat="server" ID="BtnRegister" OnClick="BtnRegister_Click" Font-Size="3em" Height="70"/>
        </div>
    </asp:Panel>
    <video id="video-preview" autoplay muted loop>
        <source src="Resources/Исландия.mp4">
    </video>
</asp:Content>
