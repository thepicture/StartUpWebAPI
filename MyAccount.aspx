﻿<%@ Page Title="Информация о пользователе" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MyAccount.aspx.cs" Inherits="StartUpWebAPI.MyAccount" %>

<asp:Content ID="ContentMyAccount" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex; justify-content: center; align-items:center; vertical-align: middle;">
        <div class="round-div-block semi-transparent-my-account request-auto-height"
            style="width: 650px; animation: none !important; display: inline-block;">

            <h2 class="title-setting-contact title-page-margin" style="color:white !important; text-align: center; color: black;"><%: Title %></h2>
            <br />

            <table >
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
                                    <asp:Label ID="LabelName" Font-Bold="true" runat="server" Style="color:white !important; font-size: 30px;" Text='Ф.И.О.'></asp:Label>
                                </p>
                                   <p style="color:white !important; float: left;   padding: 3px 2px 4px;">
                                    <asp:Label ID="LabelRole" runat="server" Style="font-size: 13px ; font-size:20px !important; " Text='Роль'></asp:Label>
                                </p> 

                                      <div class="clear"></div>    
                                           </div>
                             </tr>
                <tr >
                    <td>
                        <asp:Button runat="server"
                                CssClass="button-style-for-page round-div-block-specified-about about-like-cloud-button about-like-cloud-button-for-page-account"
                                
                                Text="Изменить"
                                ID="BtnAddImage"
                                Style="width: 100px; margin-left:10px !important;"
                                OnClick="BtnAddImage_Click" />
                    
                    </td>
                    <td>
                        <asp:FileUpload runat="server"
                                Style="margin-bottom: 0px !important; color:white !important; margin-left:5px !important;  font-size: 17px;"
                                AllowMultiple="false"
                                Visible="false"
                                ID="FileUploadImage" />
                    </td>
                    
                </tr>
               
            </table>
    </div>
 </div>
   
    <asp:Image runat="server" ID="BgImage" CssClass="bg-image"></asp:Image>
</asp:Content>
