<%@ Page Title="Информация о пользователе" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MyAccount.aspx.cs" Inherits="StartUpWebAPI.MyAccount" %>

<asp:Content ID="ContentMyAccount" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top:100px; display: flex; justify-content: center; align-items: center; vertical-align: middle;">
        <div class="white-shadow-block radius-like"
            style="width: 650px; animation: none !important; display: inline-block;">

            <h2 class="title-setting-contact title-page-margin" style="color: black !important; text-align: center; color: black;"><%: Title %></h2>
            <br />

            <table>
                <tr>
                    <td class="poster_info td1 hide-for-print">
                        <p>
                            <asp:Image Width="100" Height="100"
                                ID="UserImage"
                                runat="server" CssClass="startup-image-radius radius-like marginaled image-cover-auto"
                                ImageUrl='<%# Eval("GetCommentImage") %>' AlternateText="Фотография"></asp:Image>
                        </p>
                        <td class="message td2" rowspan="2">
                            <div class="post_head">

                                <p class="post-time">
                                    <asp:Label ID="LabelName" Font-Bold="true" runat="server" Style="color: black !important; font-size: 30px;" Text='Ф.И.О.'></asp:Label>
                                </p>
                                <p style="color: black !important;  padding: 3px 2px 4px;">
                                    <asp:Label ID="LabelRole" runat="server" Style="font-size: 13px; font-size: 20px !important;" Text='Роль'></asp:Label>
                                </p>
                                <asp:FileUpload runat="server"
                                    Style=" margin-bottom: 0px !important; color: black !important; margin-left: 5px !important; font-size: 17px; display: inline-block;"
                                    AllowMultiple="false"
                                    Visible="false"
                                    ID="FileUploadImage" />
                                <div class="clear"></div>
                            </div>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server"
                            CssClass="round-div-block-specified-about-page-account about-like-cloud-button-page-account"
                            Text="Изменить"
                            ID="BtnAddImage"
                            Style="width: 100px; margin-left: 10px !important;"
                            OnClick="BtnAddImage_Click" />

                    </td>
                    <td>
                        <%-- Do not delete --%>
                    </td>

                </tr>

            </table>
        </div>
    </div>

   
</asp:Content>
