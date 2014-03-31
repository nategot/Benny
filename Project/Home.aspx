<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&language=he" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="Scripts/MapScriptHome.js" type="text/javascript"></script>
    <link href="Styles/HomeCss.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" ClientIDMode="Inherit">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
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
                    <%--   <td>
                    <asp:Button ID="searchBtn" class="myButton" runat="server" Text="Search" OnClick="searchBtn_Click"
                        Height="35px" Width="100px" />&nbsp&nbsp
                </td>--%>
                    <%--   <td>
                    <asp:Button ID="MapviewBTN" runat="server" class="myButton" Text="Map View" OnClick="MapviewBTN_Click"
                        Height="35px" Width="100px" />
                </td>--%>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:PlaceHolder ID="MapPlaceHolder" runat="server">
        <div class="HomeMap" id="mapholder">
        </div>
        <</asp:PlaceHolder>
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

    </script>
</asp:Content>
