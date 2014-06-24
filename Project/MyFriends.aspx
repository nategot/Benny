<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MyFriends.aspx.cs" Inherits="MyFriends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  
<asp:GridView ID="userGride" runat="server">
   
</asp:GridView>
 <asp:Button ID="Button1" runat="server" Text="Invite" onclick="Button1_Click" />
</asp:Content>

