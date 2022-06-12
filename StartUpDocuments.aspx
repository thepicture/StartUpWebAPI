<%@ Page
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="StartUpDocuments.aspx.cs"
    Inherits="StartUpWebAPI.StartUpDocuments" %>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">
    <div class="marginaled center-align">
        <h1 class="items-title">Документы стартапа</h1>
        <asp:ListView runat="server" ID="LViewDocuments">
            <EmptyDataTemplate>
                Документов не найдено
            </EmptyDataTemplate>
            <ItemTemplate>
                <itemtemplate>
                    <div class="container-item startup-panel radius-like main-startup-image-container"
                        style="width: 200px; height: 200px;">
                        <h1 class="tag-item startup-image-header"><%# Eval("FileName") %></h1>
                        <asp:Button runat="server"
                            CssClass="tag-item round-div-block button-style-for-page about-like-cloud-button-for-page-four margin-bottom-top-as-usual remove-image-button"
                            Text="Скачать"
                            CommandName="DownloadStartUp"
                            CommandArgument='<%# Bind("Id") %>'
                            ID="ButtonDownloadDocument"
                            OnClick="BtnDownloadDocument_Click"></asp:Button>
                        <br />
                    </div>
                </itemtemplate>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
