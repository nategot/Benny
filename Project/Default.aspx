<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1.0, maximum-scale=1.0">
        <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
        <link href="Styles/killercarousel.css" rel="stylesheet" type="text/css" />
        <script src="Scripts/killercarousel.js" type="text/javascript"></script>
        
<%--        <style type="text/css">
            /* CSS for images inside item wrapper */
            .kc-item img
            {
                visibility: hidden;
                position: absolute;
                pointer-events: none; /* Make images non-selectable. */
                width: 100%; /* Make images expand to wrapper size (used in 2d modes). */
            }
        </style>--%>
        <script type="text/javascript">            // Create the carousel.
            $(function () {
                $('.kc-wrap').KillerCarousel({
                    // Default natural width of carousel.
                    width: 800,
                    // Item spacing in 3d (has CSS3 3d) mode.
                    spacing3d: 175,
                    // Item spacing in 2d (no CSS3 3d) mode. 
                    spacing2d: 175,
                    showShadow: true,
                    showReflection: true,
                    // Looping mode.
                    infiniteLoop: true,
                    // Scale at 75% of parent element.
                    autoScale: 50
                });
            });
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
        <div id="wrapper">
            <div class="kc-wrap">
                <div class="kc-item">
                    <asp:ImageButton ID="Soccer" ImageUrl="Styles/pic/1.png" runat="server" OnClick="CategoryFilter" />
                    <p>
                        Soccer</p>
                </div>
                <div class="kc-item">
                    <asp:ImageButton ID="Basketball" ImageUrl="Styles/pic/2.png" runat="server" OnClick="CategoryFilter" />
                    <p>
                        Basketball</p>
                </div>
                <div class="kc-item">
                    <asp:ImageButton ID="Tennis" ImageUrl="Styles/pic/3.png" runat="server" OnClick="CategoryFilter" />
                    <p>
                        Tennis</p>
                </div>
                <div class="kc-item">
                    <asp:ImageButton ID="Running" ImageUrl="Styles/pic/04.png" runat="server" OnClick="CategoryFilter" />
                    <p>
                        Running</p>
                </div>
                <div class="kc-item">
                    <asp:ImageButton ID="Swimming" ImageUrl="Styles/pic/05.png" runat="server" OnClick="CategoryFilter" />
                    <p>
                        Swimming</p>
                </div>
                <div class="kc-item">
                    <asp:ImageButton ID="Cycling" ImageUrl="Styles/pic/06.png" runat="server" OnClick="CategoryFilter" />
                    <p>
                        Cycling</p>
                </div>
                <div class="kc-item">
                    <asp:ImageButton ID="Volleyball" ImageUrl="Styles/pic/7.png" runat="server" OnClick="CategoryFilter" />
                    <p>
                        Volleyball</p>
                </div>
            </div>
        </div>
</asp:Content>
