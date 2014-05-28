<%@ Page Title="My Events" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MyEvents.aspx.cs" Inherits="MyEvents" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&language=he"
        type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="Scripts/MapScriptHome.js" type="text/javascript"></script>
    <link href="Styles/HomeCss.css" rel="stylesheet" type="text/css" />
    <link href="Styles/reveal.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.reveal.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.6.min.js"></script>
    <link href="Styles/JoinEventStyle.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script src="Scripts/SmallPopUpScript.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ToolkitScriptManager>
    <div id="Div1" style="display: none">
    </div>
    <br /><br /><br /> <br /> <br /><br /><br />
    <asp:PlaceHolder ID="searchPholder" runat="server">
        <div id="search" class="search" style="margin-left: 155px">
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
                            <asp:ListItem>Swimming</asp:ListItem>
                            <asp:ListItem>Cycling</asp:ListItem>
                            <asp:ListItem>Surfing</asp:ListItem>
                            <asp:ListItem>Volleyball</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp&nbsp&nbsp
                    </td>
                    <td>
                        Age:
                    </td>
                    <td>
                        <asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="ageTXT"
                            Minimum="0" Maximum="100" TargetButtonDownID="downArrow" TargetButtonUpID="upArrow">
                        </asp:NumericUpDownExtender>
                        <asp:TextBox ID="ageTXT" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="downArrow" runat="server" CssClass="btnCh" src="pic/down.gif" />
                        <asp:ImageButton ID="upArrow" src="pic/up.gif" CssClass="btnCh" runat="server" />&nbsp&nbsp&nbsp
                    </td>
                    <td>
                        City:
                    </td>
                    <td>
                        <asp:TextBox ID="freeSearch" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left">
            <asp:Button ID="searchBtn" class="myButton" runat="server" Text="Search" OnClick="searchBtn_Click"
                Height="35px" Width="100px" />&nbsp&nbsp</div>
    </asp:PlaceHolder>
    <div>
        <asp:Button ID="MapviewBTN" runat="server" class="myButton" Text="Map View" OnClick="MapviewBTN_Click"
            Height="35px" Width="100px" /></div>
    <asp:HiddenField ID="eventNumHF" runat="server" />
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="searchBtn" />
        </Triggers>
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" RowStyle-VerticalAlign="Middle" Font-Bold="True"
                Font-Size="Medium" CellPadding="4" GridLines="None" ForeColor="#333333" HorizontalAlign="Center"
                OnRowDataBound="GridView1_RowDataBound" BorderColor="#999999" BorderStyle="Solid">
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
            <div id="myModal" class="reveal-modal" style="float: left">
                <div>
                    <div id="contect">
                    </div>
                    <div id="map-canvas" class="map">
                    </div>
                    <asp:Button ID="joinBtn" class="btnjoin" runat="server" Text="join" OnClick="JoinBtn_Click" />
                </div>
                <a class="close-reveal-modal">&#215;</a>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:PlaceHolder ID="MapPlaceHolder" runat="server">
        <div class="HomeMap" id="mapholder">
        </div>
        <</asp:PlaceHolder>
    <div id="dialog" style="display: none">
    </div>
    <script type="text/javascript">

        function JoinEvent(num, lat, lng) {

            var a = document.getElementById("MainContent_eventNumHF");
            a.value = num;
            var EventPos = new Object();
            EventPos.lat = lat;
            EventPos.long = lng;
            var pos = new google.maps.LatLng(EventPos.lat, EventPos.long);

            var contster = '<div style="height:100px" id="content" ><h5> Are you sure you want to join? <h5/> <input type="button" class="myButton" onclick="CloseInfo()" id="btnNo" value="No"/> <asp:Button ID="joinBtnInfo2" class="myButton" runat="server" Text="Yes" onclick="JoinBtn_Click" /><div/>';

            infowindow2 = new google.maps.InfoWindow({
                content: contster,
                position: pos

            });

            infowindow2.open(map);
        }

        google.maps.event.addDomListener(window, 'load', initialize);

        function findme(pathList2) {
            pos = new google.maps.LatLng(pathList2[0].Lat, pathList2[0].Lng);
            map.setCenter(pos);
        }

        ////join event popup
        function loadEventDetail(num) {
            getOneEvent(num);
        }

        function getOneEvent(eventNum) {
            var dataString = '{eventNum:"' + eventNum + '"}';
            $.ajax({ // ajax call starts
                url: 'WebService.asmx/getOneEvent',   // server side method
                // parameters passed to the server
                type: 'POST',
                data: dataString,
                dataType: 'json', // Choosing a JSON datatype
                contentType: 'application/json; charset = utf-8',
                success: function (data) // Variable data contains the data we get from server side
                {
                    poiList = $.parseJSON(data.d);
                    document.getElementById('contect').innerHTML = "";
                    str = '';
                    //load deatil
                    str = buildListItem(poiList[0]); // add item to the list in the main events page
                    $("#contect").append(str);
                    $("#contect").collapsibleset('refresh');

                }, // end of success
                error: function (e) {
                    alert("failed in getTarget :( " + e.responseText);
                } // end of error
            }) // end of ajax call
        }



        function buildListItem(poiPoint) {

            var strT = "";
            strT += '<div data-role="collapsible"  data-mini="true"  data-content-theme="a" data-iconpos="right"  >';
            strT += ' <table ><tr><td><div class="title">' + poiPoint.Description + '</div> <br />'
            strT += '<p class="aa" ><img src = "' + poiPoint.ImageUrl + '"style="width: 30px"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp';
            strT += ' <div> <asp:Label  runat="server" CssClass="aa" Text="Admin:"></asp:Label>&nbsp;&nbsp;';
            strT += '<asp:Label runat="server" CssClass="bbb" >' + poiPoint.AdminFullName + ' </asp:Label> <br />';
            strT += '<asp:Label  runat="server" CssClass="aa" Text="Max Participants:"></asp:Label>&nbsp;&nbsp;'
            strT += '<asp:Label  runat="server" CssClass="bbb" >' + poiPoint.NumOfParti + '</asp:Label> <br />'
            strT += '<asp:Label  runat="server" CssClass="aa" Text="Date & Time:"></asp:Label>&nbsp;&nbsp;'
            strT += '<asp:Label  runat="server" CssClass="bbb" >' + poiPoint.DateTimeStr + ' </asp:Label> <br />'
            strT += '<asp:Label  runat="server" CssClass="aa" Text="Age Range:"></asp:Label>&nbsp;&nbsp;'
            strT += '<asp:Label  runat="server" CssClass="bbb">' + poiPoint.MinAge + '-' + poiPoint.MaxAge + '</asp:Label><br />'
            strT += '<asp:Label  runat="server" CssClass="aa" Text="Location:"></asp:Label>&nbsp;&nbsp;'
            strT += ' <asp:Label  runat="server" CssClass="bbb" >' + poiPoint.Address + '</asp:Label><br />'
            strT += '<asp:Label  runat="server" CssClass="aa" Text="Frequency:"></asp:Label>&nbsp;&nbsp;'
            strT += '<asp:Label  runat="server" CssClass="bbb" >' + poiPoint.FrequencyStr + '</asp:Label><br />'
            strT += '<asp:Label runat="server" CssClass="aa" Text="Admin Comments:"></asp:Label>&nbsp;&nbsp;'
            strT += '<asp:Label  runat="server" CssClass="bbb" >' + poiPoint.Comments + '</asp:Label><br /><br />'
            strT += '</br></td> <td class="tt">' + buildBoard(poiPoint.PlayerList, poiPoint.NumOfParti); +'</td></tr>  </table></div>';

            //save the event num
            var a = document.getElementById("MainContent_eventNumHF");
            a.value = poiPoint.EventNum;
            //load table

            //                      

            //build map
            var ruppinPos = new Object();
            var latH = poiPoint.Point.Lat;
            var lngH = poiPoint.Point.Lng;
            ruppinPos.lat = latH;
            ruppinPos.long = lngH;
            var myLatlng = new google.maps.LatLng(ruppinPos.lat, ruppinPos.long);
            var mapOptions = {
                zoom: 11,
                center: myLatlng,
                mapTypeId: google.maps.MapTypeId.Map

            }
            map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

            var marker1 = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: ''
            });


            return strT;

        }

        function buildBoard(PlayerList, numRows) {

            str = "<table class='CSSTableGenerator'>";

            str += "<tr>";
            str += "<td>";
            str += "</td>";
            str += "<td>Username";
            str += "</td> ";
            str += "</tr>";
            for (row = 0; row < numRows; row++) {

                str += "<tr>";
                str += "<td>";
                str += row + 1;
                str += "</td>";

                if (PlayerList[row] != undefined) {
                    str += "<td>";
                    str += PlayerList[row];
                    str += "</td>";
                }
                else {
                    str += "<td>";
                    str += "-";
                    str += "</td>";
                }

                str += "</tr>";
            }

            str += "</table>";

            return str;
        } //buildBoard

    </script>
</asp:Content>
