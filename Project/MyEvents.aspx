﻿<%@ Page Title="My Events" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MyEvents.aspx.cs" Inherits="MyEvents" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&language=he"
        type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="Scripts/scriptMyEvents.js" type="text/javascript"></script>
    <link href="Styles/HomeCss.css" rel="stylesheet" type="text/css" />
    <link href="Styles/reveal.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.reveal.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.6.min.js"></script>
    <link href="Styles/JoinEventStyle.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script src="Scripts/DIVPOPUPscript.js" type="text/javascript"></script>
    <link href="Styles/listOfuSERS.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/SmallPopUpScript.js" type="text/javascript"></script>
    <link href="Styles/accordion.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="adminEmailHIde" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="adminIDHIde" runat="server"></asp:HiddenField>
    <br />
    <br />
    <div id="Div1" name="popupdiv" style="display: none">
    </div>
   
    <asp:PlaceHolder ID="searchPholder" runat="server">
        <div id="search" class="search" style="margin-left: 155px">
            <table>
                <tr>
                    <td>
                      <asp:Label CssClass="lblHome"  runat="server" Text=" Category:"> Category: &nbsp</asp:Label> 
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
                        <asp:Label   CssClass="lblHome" runat="server"   Text="Age: "> Age: &nbsp</asp:Label> 
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
                       <asp:Label CssClass="lblHome" runat="server"   Text=" City: "> City: &nbsp</asp:Label> 
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
                Font-Size="Medium" CellPadding="4" GridLines="Horizontal" ForeColor="Black" HorizontalAlign="Center"
                OnRowDataBound="GridView1_RowDataBound" BorderColor="#CCCCCC" BorderStyle="None"
                BackColor="White" BorderWidth="1px">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
            <div id="myModal" class="reveal-modal" style="float: left">
                <div>
                    <div id="toPopup">
                        <div class="close">
                        </div>
                        <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
                        <div id="popup_content">
                        </div>
                        <div id="prtis2">
                        </div>
                    </div>
                    <div class="loader">
                    </div>
                    <div id="backgroundPopup">
                    </div>
                    <div id="contect" style="float: left; width: 50%">
                    </div>
                    <div id="map-canvas" class="map" style="float: left">
                    </div>
                    <a href="#" class="topopup">
                        <asp:Button ID="Participantsbtn" CssClass="myButton" runat="server" Text="Participants" /></a>
                    <asp:Button ID="LeaveBtn" class="myButton" runat="server" Text="Leave event" OnClick="LeaveBtn_Click" />
                    <div id="try">
                    </div>
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
        var isAdmin = false;
        var url = 'http://proj.ruppin.ac.il/bgroup14/prod/tar6/WebService.asmx/';
        var idUser = 0;
        var eventNum;

        function JoinEvent(num, lat, lng) {

            var a = document.getElementById("MainContent_eventNumHF");
            a.value = num;
            var EventPos = new Object();
            EventPos.lat = lat;
            EventPos.long = lng;
            var pos = new google.maps.LatLng(EventPos.lat, EventPos.long);

            var contster = '<div style="height:100px" id="content" ><h5> Are you sure you want to join? <h5/> <input type="button" class="myButton" onclick="CloseInfo()" id="btnNo" value="No"/> <asp:Button ID="joinBtnInfo2" class="myButton" runat="server" Text="Yes"  /><div/>';

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
            email = document.getElementById("MainContent_adminEmailHIde").value;
            eventNum = num;
            getOneEvent(num, email);
        }

        function getOneEvent(eventNum, UserEmail) {
            var dataString = '{eventNum:"' + eventNum + '",UserEmail:"' + UserEmail + '"}';
            $.ajax({ // ajax call starts
                url: url + 'getOneMyEvent',   // server side method
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

        function backgroundImage(categ) {

            switch (categ) {

                case "Soccer":
                    document.getElementById('myModal').style.backgroundImage = 'url(Styles/pic/1.jpg)';
                    break;
                case "Basketball":
                    document.getElementById('myModal').style.backgroundImage = 'url(Styles/pic/2.jpg)';
                    break;
                case "Tennis":
                    document.getElementById('myModal').style.backgroundImage = 'url(Styles/pic/3.jpg)';
                    //document.getElementById('myModal').style.backgroundPosition = '95% 40%';
                    break;
                case "Running":
                    document.getElementById('myModal').style.backgroundImage = 'url(Styles/pic/4.jpg)';
                    break;
                case "Swimming":
                    document.getElementById('myModal').style.backgroundImage = 'url(Styles/pic/5.jpg)';
                    break;
                case "Cycling":
                    document.getElementById('myModal').style.backgroundImage = 'url(Styles/pic/6.jpg)';
                    break;
                case "Surfing":
                    document.getElementById('myModal').style.backgroundImage = 'url(Styles/pic/7.jpg)';
                    break;
                case "Volleyball":
                    document.getElementById('myModal').style.backgroundImage = 'url(Styles/pic/8.jpg)';
                    break;
                default:

            }

        }


        function buildListItem(poiPoint) {
            //check if user is admin to add edit event btn 
            var b = document.getElementById("MainContent_adminIDHIde");
            if (poiPoint.AdminID == b.value) {
                isAdmin = true;
                
            }
            else {
                isAdmin = false;
                
            }

            var strT = "";
            strT += '  <table>';
            strT += ' <tr> <td> <asp:Label ID="Label1" runat="server" CssClass="titlePop"> ' + poiPoint.Description + '</asp:Label> </td> </tr>';
            strT += ' <tr>  <td><asp:Label  runat="server" CssClass="aa" Text="Admin:"></asp:Label>&nbsp;&nbsp; <asp:Label runat="server" CssClass="bbb" >' + poiPoint.AdminFullName + ' </asp:Label></td> </tr>';
            strT += ' <tr>  <td><asp:Label  runat="server" CssClass="aa" Text="Number Participants:"></asp:Label>&nbsp;&nbsp; <asp:Label  runat="server" CssClass="bbb" >' + poiPoint.NumOfRegis + '/'+poiPoint.NumOfParti + '</asp:Label>  </td> </tr>';
            strT += ' <tr>  <td><asp:Label  runat="server" CssClass="aa" Text="Date & Time:"></asp:Label>&nbsp;&nbsp; <asp:Label  runat="server" CssClass="bbb" >' + poiPoint.DateTimeStr + ' </asp:Label>  </td> </tr>';
            strT += ' <tr>  <td><asp:Label  runat="server" CssClass="aa" Text="Age Range:"></asp:Label>&nbsp;&nbsp; <asp:Label  runat="server" CssClass="bbb">' + poiPoint.MinAge + '-' + poiPoint.MaxAge + '</asp:Label> </td></tr>';
            strT += ' <tr>  <td><asp:Label  runat="server" CssClass="aa" Text="Location:"></asp:Label>&nbsp;&nbsp; <asp:Label  runat="server" CssClass="bbb" >' + poiPoint.Address + '</asp:Label>  </td> </tr>';
            strT += ' <tr>  <td><asp:Label  runat="server" CssClass="aa" Text="Frequency:"></asp:Label>&nbsp;&nbsp;<asp:Label  runat="server" CssClass="bbb" >' + poiPoint.FrequencyStr + '</asp:Label> </td> </tr>';
            strT += ' <tr>  <td><asp:Label runat="server" CssClass="aa" Text="Admin Comments:"></asp:Label>&nbsp;&nbsp; <asp:Label  runat="server" CssClass="bbb" >' + poiPoint.Comments + '</asp:Label></td> </tr>';
            strT += ' </table>';
            backgroundImage(poiPoint.Description);


            strT += buildBoard(poiPoint.PlayerUserList, poiPoint.NumOfParti);

            //save the event num
            var a = document.getElementById("MainContent_eventNumHF");
            a.value = poiPoint.EventNum;

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


            if (isAdmin) {
                var a = document.getElementById("try");
                a.innerHTML = '</br><asp:Button ID="EditEventBTn" class="myButton" runat="server" Text="Edit" onclick="EditEventBTn_Click" /><asp:Button ID="InvitBtn" class="myButton" runat="server" Text="Invite From List" onclick="InviteBTn_Click" />';

            }
            else {
                var a = document.getElementById("try");
                a.innerHTML = "";

            }
            return strT;
        }

        function buildBoard(PlayerList, numRows) {

            st = '';

            str += ' <div id="chatlist" class="mousescroll"><div id="containerAccordion"><section id="accordion">';
            for (row = 0; row < numRows; row++) {
                if (PlayerList[row] != undefined) {

                    str += ' <div> <input type="checkbox" id="check-' + row + 1 + '" /> <label for="check-' + row + 1 + '">' + (row + 1) + ". " + PlayerList[row].UserName + '</label>';
                    str += ' <article>';
                    str += '<div style="width:100%"><div style="width:65%; float:left"></br>'
                    str += '&nbsp;<asp:Label  runat="server" CssClass="fontacor1" Text="Name:"></asp:Label>'
                    str += ' <asp:Label  runat="server" CssClass="fontacor" >' + PlayerList[row].Fname + '</asp:Label>';
                    str += ' <asp:Label  runat="server" CssClass="fontacor" >' + PlayerList[row].Lname + '</asp:Label></br>';
                    str += '&nbsp;<asp:Label  runat="server" CssClass="fontacor1" Text="Age:"></asp:Label>'
                    str += ' <asp:Label  runat="server" CssClass="fontacor" >' + PlayerList[row].Age + ' </asp:Label></br>';
                    str += '<table style="whith:100%;"><tr >';
                    str += '   <td> <asp:Label  runat="server" CssClass="fontacor1" Text="Rating:"></asp:Label></td>';
                    str += '   <td >  <legend id="ratinglab' + PlayerList[row].UserId + '" class="fontacorrr">' + PlayerList[row].Rating + '</legend></td> ';
                    str += '   </tr> </table>';
                    str += '&nbsp;<asp:Label  runat="server" CssClass="fontacor1" Text="City:"></asp:Label>&nbsp;'
                    str += '&nbsp;<asp:Label  runat="server" CssClass="fontacor" >' + PlayerList[row].City + ' </asp:Label></br>';
                    if (isAdmin) {
                        str += '&nbsp;<asp:Label  runat="server" CssClass="fontacor1" Text="Rat this User: "></asp:Label>&nbsp;';
                        str += '<input type="button" class="ratingbutton" onclick="RatingUp(' + PlayerList[row].UserId + ',' + PlayerList[row].Rating + ')" id="btnup" value="+"/>';
                        str += '<input type="button" class="ratingbutton" onclick="RatingDown(' + PlayerList[row].UserId + ',' + PlayerList[row].Rating + ')" id="btndown" value="-"/>';
                    }


                    str += '</br></div><div style="width:35%; float:left"><img class="accordionimg" src="' + PlayerList[row].ImageUrl + '" ></br>'
                    str += '</div></div>';
                    str += ' </article></div>';

                }
                else {
                    str += ' <div><input type="checkbox" id="check-' + row + 1 + '" /> <label for="check-' + row + 1 + '">' + (row + 1) + '. Available</label>';
                    str += ' <article>	';
                    str += '';
                    str += ' </article></div>';
                }
            }
            str += ' </section></div></div>';
            document.getElementById("prtis2").innerHTML = str;

            return st;

        } //buildBoard

        function RatingDown(id, rating) {
            if (rating > 0 && idUser != id) {
               
                idUser = id;
                idstr = "ratinglab" + id;
                var dataString = '{id:"' + id + '"}';
                $.ajax({ // ajax call starts
                    url: url + 'RatingDown',   // server side method
                    // parameters passed to the server
                    type: 'POST',
                    data: dataString,
                    dataType: 'json', // Choosing a JSON datatype
                    contentType: 'application/json; charset = utf-8',
                    success: function (data) // Variable data contains the data we get from server side
                    {
                        poiList = $.parseJSON(data.d);
                    }, // end of success
                    error: function (e) {
                        alert("failed in getTarget :( " + e.responseText);
                    } // end of error
                }) // end of ajax call
                document.getElementById(idstr).innerHTML = document.getElementById(idstr).innerHTML - 5;
            }
        }

        function RatingUp(id, rating) {

            if (rating < 100 && idUser != id) {
                idUser = id; 
                idstr = "ratinglab" + id;
                var dataString = '{id:"' + id + '"}';
                $.ajax({ // ajax call starts
                    url: url + 'RatingUp',   // server side method
                    // parameters passed to the server
                    type: 'POST',
                    data: dataString,
                    dataType: 'json', // Choosing a JSON datatype
                    contentType: 'application/json; charset = utf-8',
                    success: function (data) // Variable data contains the data we get from server side
                    {
                        poiList = $.parseJSON(data.d);

                    }, // end of success
                    error: function (e) {
                        alert("failed in getTarget :( " + e.responseText);
                    } // end of error

                }) // end of ajax call
                var num = parseInt(document.getElementById(idstr).innerHTML)
                document.getElementById(idstr).innerHTML = num + 5;
            }
        }
    </script>
</asp:Content>
