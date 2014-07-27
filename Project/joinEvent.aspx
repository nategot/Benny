<%@ Page Title="Join Event" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="JoinEvent.aspx.cs" Inherits="joinEvent" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&language=he" type="text/javascript"></script>
    <script src="Scripts/MapScriptJoinEvent.js" type="text/javascript"></script>
    <link href="Styles/JoinEventStyle.css" rel="stylesheet" type="text/css" />
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
        <asp:GridView ID="playerTableGrv" runat="server" CellPadding="4" 
            GridLines="None" ForeColor="#333333">
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
</asp:Content>
