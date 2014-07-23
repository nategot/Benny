<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="Styles/CssAbout.css" rel="stylesheet" type="text/css" />
    <br />
    <br />
    <br />
    <br />
    <table style="width: 80%; margin-left: 5%;">
        <tr>
            <td class="tdabout">
                <div class="Aboutdiv">
                    <img class="imgabot" src="http://proj.ruppin.ac.il/bgroup14/prod/Docs/images/netanel.jpg" /><br />
                    <br />
                    <asp:Label runat="server" CssClass="Aboutxt" Text="Netanel Gottfried"></asp:Label><br />
                    <a href="mailto:nategot@gmail.com?Subject=Hello%20Netanel" target="_top">
                        <img class="Aboutpicc" src="pic/mail.png" /></a>
                </div>
            </td>
            <td class="tdabout">
                <div class="Aboutdiv">
                    <img class="imgabot" src="http://proj.ruppin.ac.il/bgroup14/prod/Docs/images/omer_liverant.png" /><br />
                    <br />
                    <asp:Label runat="server" CssClass="Aboutxt" Text="Omer Liverant"></asp:Label>
                    <a href="mailto:omerliverant@gmail.com?Subject=Hello%20Omer" target="_top"><br />
                        <img class="Aboutpicc" src="pic/mail.png" /></a>
            </td>
            <td class="tdabout">
                <div class="Aboutdiv">
                    <img class="imgabot" src="http://proj.ruppin.ac.il/bgroup14/prod/Docs/images/ido_machnes.jpg" /><br />
                    <br />
                    <asp:Label runat="server" CssClass="Aboutxt" Text="Ido Machnes"></asp:Label><br />
                    <a href="mailto:ido.machnes@gmail.com?Subject=Hello%20Ido" target="_top">
                        <img class="Aboutpicc" src="pic/mail.png" /></a>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <iframe  src="//www.youtube.com/embed/8W3KI1BjlQg" frameborder="0" allowfullscreen></iframe>
</asp:Content>
