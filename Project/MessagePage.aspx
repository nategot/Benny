<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MessagePage.aspx.cs" Inherits="MessagePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="massageLBL" runat="server" CssClass="ErroerLBL" ></asp:Label>
    <br />
    <br />
    <br />
    <div>
        <asp:Button ID="RegisterBTN" runat="server" Text="Register" 
            Visible="false" CssClass="myButton"  onclick="RegisterBTN_Click" />
        <asp:Button ID="BackeBtn" CssClass="myButton"  runat="server" Text="Back" Visible="false" 
            onclick="BackeBtn_Click" />
    </div>
</asp:Content>
