<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" %>

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
    <%--
    <script src="Scripts/MapScriptJoinEvent.js" type="text/javascript"></script> --%>
    <link href="Styles/JoinEventStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" ClientIDMode="Inherit">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:PlaceHolder ID="searchPholder" runat="server">
        <div id="search" class="search">
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
            <div id="myModal" class="reveal-modal">
                <div class="title">
                    Join Event
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:bgroup14_test1ConnectionString %>"
                    SelectCommand="SELECT [UserName] FROM [UsersInEvent] WHERE ([EventNumber] = @EventNumber)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="EventNumber" SessionField="EventNumber"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <div>
                    <asp:Label ID="EventNameLbl" CssClass="EventNameLbl" runat="server" Font-Size="XX-Large"
                        Font-Italic="True" Font-Bold="True"></asp:Label>
                    <asp:Image ID="iconImg" runat="server" />
                </div>
                <div id="leftdiv" style="float: left">
                    <br />
                    <table id="eventDetailTable">
                        <tr>
                            <td>
                                <asp:Label ID="AdminLbl" runat="server" Text="Admin:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ANS_AdminLbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="MaxPlayerLbl" runat="server" Text="Max Participants:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ANS_MaxPlayerLbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="datatimelbl" runat="server" Text="Date & Time:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ANS_datatimelbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="EventTypelbl" runat="server" Text="Event Type:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ANS_EventTypelbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="AgeLbl" runat="server" Text="Age Range:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ANS_AgeLbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="locationLbl" runat="server" Text="Location:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ANS_locationLbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Frequencylbl" runat="server" Text="Frequency:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ANS_Frequency" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="commentLbl" runat="server" Text="Admin Comments:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ANS_commentLbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="latHF" runat="server" />
                                <asp:HiddenField ID="lngHF" runat="server" />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td id="map-canvas" colspan="2">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="rightdiv" style="float: right; padding-right: 80px;">
                    <br />
                    <asp:GridView ID="playerTableGrv" runat="server" CellPadding="4" GridLines="None"
                        ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
                <br />
                <div class="divvv">
                    <asp:Button ID="joinBTN" CssClass="myButton" runat="server" Text="Join Now!" OnClick="joinBTN_Click" />
                </div>
                <a class="close-reveal-modal">&#215;</a>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:PlaceHolder ID="MapPlaceHolder" runat="server">
        <div class="HomeMap" id="mapholder">
        </div>
        <</asp:PlaceHolder>
    <a href="#" class="big-link" data-reveal-id="myModal" onclick="SmallMap()">Fade and
        Pop </a>
    <br />
    <br />
    <a href="#" class="big-link" data-reveal-id="myModal" data-animation="fade">Fade
    </a>
    <br />
    <asp:HyperLink ID="HyperLink1" class="big-link" runat="server"  onclick="SmallMap()" data-reveal-id="myModal" >HyperLink</asp:HyperLink>
    <br />
    <a href="#" class="big-link" data-reveal-id="myModal" data-animation="none">None
    </a>
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



        function SmallMap() {

   alert("df");
    var ruppinPos = new Object();
    var a = document.getElementById("MainContent_latHF");
    var latH = a.value;
    var b = document.getElementById("MainContent_lngHF");
    var lngH = b.value;

    ruppinPos.lat = latH;
    ruppinPos.long = lngH;
    var myLatlng = new google.maps.LatLng(ruppinPos.lat, ruppinPos.long);
    var mapOptions = {
        zoom: 17,
        center: myLatlng,
        mapTypeId: google.maps.MapTypeId.Map

    }
    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

    var marker1 = new google.maps.Marker({
        position: myLatlng,
        map: map,
        title: 'Ruppin'
    });

}

google.maps.event.addDomListener(window, 'load', initialize);




function findme(pathList2) {
    pos = new google.maps.LatLng(pathList2[0].Lat, pathList2[0].Lng);
    map.setCenter(pos);
}

    </script>
</asp:Content>
