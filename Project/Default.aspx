﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1.0, maximum-scale=1.0">
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <link href="Styles/killercarousel.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/killercarousel.js" type="text/javascript"></script>
    <script type="text/javascript">        // Create the carousel.
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
                autoScale: 45
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    <div id="wrapper">
        <div class="kc-wrap">
            <div class="kc-item">
                <asp:ImageButton ID="Soccer" ImageUrl="Styles/pic/1.png" runat="server" OnClick="CategoryFilter" />
                <br />
                <br />
                <p>
                    Soccer</p>
            </div>
            <div class="kc-item">
                <asp:ImageButton ID="Basketball" ImageUrl="Styles/pic/2.png" runat="server" OnClick="CategoryFilter" />
                <br />
                <br />
                <p>
                    Basketball</p>
            </div>
            <div class="kc-item">
                <asp:ImageButton ID="Tennis" ImageUrl="Styles/pic/3.png" runat="server" OnClick="CategoryFilter" />
                <br />
                <br />
                <p>
                    Tennis</p>
            </div>
            <div class="kc-item">
                <asp:ImageButton ID="Running" ImageUrl="Styles/pic/4.png" runat="server" OnClick="CategoryFilter" />
                <p>
                    Running</p>
            </div>
            <div class="kc-item">
                <asp:ImageButton ID="Swimming" ImageUrl="Styles/pic/5.png" runat="server" OnClick="CategoryFilter" />
                <p>
                    Swimming</p>
            </div>
            <div class="kc-item">
                <asp:ImageButton ID="Cycling" ImageUrl="Styles/pic/6.png" runat="server" OnClick="CategoryFilter" />
                <br />
                <br />
                <p>
                    Cycling</p>
            </div>
            <div class="kc-item">
                <asp:ImageButton ID="Surfing" ImageUrl="Styles/pic/7.png" runat="server" OnClick="CategoryFilter" />
                <p>
                    Surfing</p>
            </div>
            <div class="kc-item">
                <asp:ImageButton ID="Volleyball" ImageUrl="Styles/pic/8.png" runat="server" OnClick="CategoryFilter" />
                <br />
                <br />
                <p>
                    Volleyball</p>
            </div>
        </div>
        
    </div>
    <div id="jjj"></div>
    <%--<div class='social-wrap'>
        <ul>
            <li><a class='facebook' href='http://www.facebook.com/' target='_blaank'>Facebook</a></li>
            <li><a class='twitter' href='http://twitter.com/' target='_blaank'>Twitter</a></li>
            <li><a class='google-p' href='https://plus.google.com/' target='_blaank'>Google Plus</a></li>
            <li><a class='rss' href='#' target='_blaank'>Rss</a></li>
        </ul>
    </div>--%>
</asp:Content>
