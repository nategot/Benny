<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MessagePage.aspx.cs" Inherits="MessagePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&libraries=places&language=he"></script>
    <script src="Scripts/MapScriptNewEvent2.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script src="Scripts/SmallPopUpScript.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="Scripts/Facebook.js" type="text/javascript"></script>
    <asp:Timer ID="MyEventsTimer" runat="server" OnTick="MyEventsTimer_Tick" Interval="1500"
        Enabled="False">
    </asp:Timer>
    <asp:Timer ID="NewEventTimer" runat="server" OnTick="NewEventTimer_Tick" Interval="1500"
        Enabled="False">
    </asp:Timer>
    <br /> <br /> <br /> <br />
    <asp:Label ID="massageLBL" runat="server" CssClass="ErroerLBL"></asp:Label>
    <div id="NoRecords" class="container" visible="false" runat="server">
        <div class='ring blue'>
        </div>
        <div id="contenttt">
            <span>LOADING</span>
        </div>
    </div>
</asp:Content>
