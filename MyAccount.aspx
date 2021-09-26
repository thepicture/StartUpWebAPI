<%@ Page Title="Информация о пользователе" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MyAccount.aspx.cs" Inherits="StartUpWebAPI.MyAccount" %>

<asp:Content ID="ContentMyAccount" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex; justify-content: center; vertical-align: middle;"class="radius-like gradientable">

        <div class="round-div-block" style="width: 600px; height: 300px; animation: none !important; display: inline-block;">

            <h2 class="title-setting-contact title-page-margin" style="text-align: center; color: black;"><%: Title %></h2>
            <br />

            <table>
                <tr>
                    <td class="poster_info td1 hide-for-print">
                        <p>
                            <asp:Image Width="100" Height="100"
                                ID="UserImage"
                                runat="server" CssClass="startup-image-radius radius-like marginaled"
                                ImageUrl='<%# Eval("GetCommentImage") %>' AlternateText="Фотография"></asp:Image>
                            <asp:FileUpload runat="server"
                                Style="margin-bottom: 0px !important; font-size: 17px;"
                                AllowMultiple="false"
                                Visible="false"
                                ID="FileUploadImage" />
                            <asp:Button runat="server"
                                CssClass="button-style-for-page about-like-cloud-button-for-page"
                                Text="Изменить"
                                ID="BtnAddImage"
                                style="width:100px;"
                                OnClick="BtnAddImage_Click" />
                        </p>
                        <td class="message td2" rowspan="2">
                            <div class="post_head">
                                <p class="post-time">
                                    <asp:Label ID="LabelName" Font-Bold="true" runat="server" Style="font-size: 30px;" Text='Ф.И.О.'></asp:Label>
                                </p>

                                <p style="float: right; padding: 3px 2px 4px;">
                                    <asp:Label ID="LabelRole" runat="server" Style="font-size: 13px;" Text='Роль'></asp:Label>
                                </p>
                                <div class="clear"></div>


                            </div>
                                <div class="post_body">
                                    <asp:Label runat="server" Text='<%# Eval("CommentText") %>'></asp:Label>
                                </div>
                            </div>
                        </td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
