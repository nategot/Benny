<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<script src="Scripts/MapScriptHome.js" type="text/javascript"></script>--%>
    <link href="Styles/HomeCss.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" ClientIDMode="Inherit">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <div id="search" class="search" style="font-size: 16px; font-weight: bold;">
        <table>
            <tr>
                <td>
                    Category:
                </td>
                <td>
                    <asp:DropDownList ID="catgoryDdl" runat="server" CssClass="ggg">
                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem>Soccer</asp:ListItem>
                        <asp:ListItem>Basketball</asp:ListItem>
                        <asp:ListItem>Tennis</asp:ListItem>
                        <asp:ListItem>Running</asp:ListItem>
                        <asp:ListItem>Cycling</asp:ListItem>
                        <asp:ListItem>Volleyball</asp:ListItem>
                    </asp:DropDownList>&nbsp&nbsp&nbsp
                </td>
                <td>
                    Age:
                </td>
                <td>
                    <asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="ageTXT"
                        Minimum="0" Maximum="100" TargetButtonDownID="downArrow" TargetButtonUpID="upArrow">
                    </asp:NumericUpDownExtender>
                    <asp:TextBox ID="ageTXT" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="downArrow" runat="server" CssClass="btnCh" src="Images/down.gif" />
                    <asp:ImageButton ID="upArrow" src="Images/up.gif" CssClass="btnCh" runat="server" />&nbsp&nbsp&nbsp
                </td>
                <td>
                    City:
                </td>
                <td>
                    <asp:TextBox ID="freeSearch" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                </td>
                <td>
                    <asp:Button ID="searchBtn" class="myButton" runat="server" Text="Search" OnClick="searchBtn_Click" height="35px" Width="100px" />&nbsp&nbsp
                </td>
                <td>
                    <asp:Button ID="MapviewBTN" runat="server" class="myButton" Text="Map View" OnClick="MapviewBTN_Click" height="35px" Width="100px" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <asp:HiddenField ID="eventNumHF" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="searchBtn" />
        </Triggers>
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" RowStyle-VerticalAlign="Middle" Font-Bold="True"
                Font-Size="Medium" CellPadding="4" GridLines="None" ForeColor="#333333" HorizontalAlign="Center">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:PlaceHolder ID="MapPlaceHolder" runat="server">
        <div class="HomeMap" id="mapholder">
        </div>
        <</asp:PlaceHolder>
    <script type="text/javascript">
        function initialize() {

            // center the map in Ruppin
            var ruppinPos = new Object();
            ruppinPos.lat = 32.343193;
            ruppinPos.long = 34.911908;
            var myLatlng = new google.maps.LatLng(ruppinPos.lat, ruppinPos.long);
            var mapOptions = {
                zoom: 6,
                center: myLatlng,
                mapTypeId: google.maps.MapTypeId.Map
            }
            map = new google.maps.Map(document.getElementById('mapholder'), mapOptions);

            var marker1 = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: 'Ruppin'
            });
            getPOIList();
        }

        google.maps.event.addDomListener(window, 'load', initialize);

        //-----------------------------------------------------------------------
        // get the getPOIList 
        //-----------------------------------------------------------------------
        function getPOIList() {
            var dataString = '{ss:"sssd"}'; ;
            $.ajax({ // ajax call starts
                url: 'WebService.asmx/getEvents',   // server side method
                // parameters passed to the server
                type: 'POST',
                dataType: 'json', // Choosing a JSON datatype
                contentType: 'application/json; charset = utf-8',
                success: function (data) // Variable data contains the data we get from server side
                {
                    poiList = $.parseJSON(data.d);

                    // run on all the POIs and display them
                    for (i = 0; i < poiList.length; i++) {
                        showPOI(poiList[i]);
                    }
                }, // end of success
                error: function (e) {
                    alert("failed in getTarget :( " + e.responseText);
                } // end of error
            }) // end of ajax call
        }

        //--------------------------------------
        // show the POI on the map
        //--------------------------------------
        function showPOI(poiPoint) {

            var poiLatlng = new google.maps.LatLng(poiPoint.Point.Lat, poiPoint.Point.Lng);
            image = poiPoint.ImageUrl;
            var marker = new google.maps.Marker({
                position: poiLatlng,
                map: map,
                title: poiPoint.Name,
                icon: image

            });

            var contentString = '<div id="content" > <img src ="' + poiPoint.ImageUrl + '" style="width: 80px"/></br><h1>' + poiPoint.Description + '</h1><div id="bodyContent" style="color:Black">'
              + '<p>Age Range: ' + poiPoint.MaxAge + '-' + poiPoint.MinAge + '</p>' + '<p>Address: ' + poiPoint.Address + '</p>' + '<p>Date & Time: ' + poiPoint.DateTimeStr + '</p>' + '</div>' + '<p><asp:Button ID="joinBtnInfoW" class="myButton" runat="server" Text="JOIN" onclick="JoinBtn_Click" /><input type="button" class="myButton" onclick="JoinEvent(' + poiPoint.EventNum + ')" id="btnJoinMap" value="Join"/><p>' + '</div>';

            var infowindow = new google.maps.InfoWindow({
                content: contentString
            });

            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });

        }

        function JoinEvent(num) {

            var a = document.getElementById("MainContent_eventNumHF");
            a.value = num;
        }
 
    </script>
</asp:Content>
