<%@ Page Title="Join Event" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="JoinEvent.aspx.cs" Inherits="joinEvent" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false" type="text/javascript"></script>
    <script src="Scripts/MapScriptJoinEvent.js" type="text/javascript"></script>
    <link href="Styles/JoinEventStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 259px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
                <td class="style1">
                    <asp:Label ID="AdminLbl" runat="server" Text="Admin:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="ANS_AdminLbl" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="MaxPlayerLbl" runat="server" Text="Max Participants:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="ANS_MaxPlayerLbl" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="datatimelbl" runat="server" Text="Date & Time:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="ANS_datatimelbl" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="EventTypelbl" runat="server" Text="Event Type:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="ANS_EventTypelbl" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="AgeLbl" runat="server" Text="Age Range:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="ANS_AgeLbl" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="locationLbl" runat="server" Text="Location:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="ANS_locationLbl" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="Frequencylbl" runat="server" Text="Frequency:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="ANS_Frequency" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="commentLbl" runat="server" Text="Admin Comments:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="ANS_commentLbl" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:HiddenField ID="latHF" runat="server" />
                    <asp:HiddenField ID="lngHF" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td id="map-canvas" class="style1" colspan="2">
                </td>
            </tr>
        </table>
    </div>
    <div id="rightdiv" style="float: right; padding-right: 80px;">
        <br />
        <asp:GridView ID="playerTableGrv" runat="server" BackColor="White" BorderColor="White"
            BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>
    </div>
    <div class="divvv">
        <asp:Button ID="joinBTN" runat="server" Text="Join Now!" OnClick="joinBTN_Click" />
    </div>
</asp:Content>
