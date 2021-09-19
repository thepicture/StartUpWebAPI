<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Startups.aspx.cs" Inherits="StartUpWebAPI.Startups" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--ListView for startup presentation.--%>
    <asp:ListView ID="StartupsView" runat='server'>
        <ItemTemplate>
            <asp:Panel runat="server" HorizontalAlign="Center" Width="200" Height="200">
                <div>
                    <asp:Label CssClass="startup-name" runat="server" Font-Bold="true" Font-Size="Large" Text='<%# Eval("Name") %>'></asp:Label>
                </div>
                <div>
                    <asp:Image Width="100" Height="100" runat='server' CssClass="startup-image-radius" ImageUrl='<%# Eval("ImagePreview") %>' />
                </div>
            </asp:Panel>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
