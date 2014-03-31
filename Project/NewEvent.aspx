<%@ Page Title="New Event" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NewEvent.aspx.cs" Inherits="NewEvent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="http://maps.google.com/maps/api/js?sensor=false&language=he"></script>
    <script src="Scripts/MapScriptNewEvent2.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <h1>
        Create New Event</h1>
    <br />
    <br />
    <table style="float: left" id="search">
        <tr>
            <td>
                Category:
            </td>
            <td colspan="3">
                <asp:DropDownList ID="categoryDDL" runat="server" CssClass="ggg">
                    <asp:ListItem Value="1">Soccer</asp:ListItem>
                    <asp:ListItem Value="2">Basketball</asp:ListItem>
                    <asp:ListItem Value="3">Tennis</asp:ListItem>
                    <asp:ListItem Value="4">Running</asp:ListItem>
                    <asp:ListItem Value="5">Cycling</asp:ListItem>
                    <asp:ListItem Value="6">Swimming</asp:ListItem>
                    <asp:ListItem Value="7">Volleyball</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Number of Participants:
            </td>
            <td colspan="3">
                <asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="NOP"
                    Minimum="0" Maximum="100" TargetButtonDownID="downArrow" TargetButtonUpID="upArrow">
                </asp:NumericUpDownExtender>
                <asp:TextBox ID="NOP" Text="8" runat="server" Width="40"></asp:TextBox>
                <input type="image" class="btnCh" id="downArrow" src="pic/down.gif"  />
                <input type="image" class="btnCh" id="upArrow" src="pic/up.gif"  />
            </td>
        </tr>
        <tr>
            <td>
                Location:
            </td>
            <td colspan="3">
                <input type="text" value="" id="locationTB" />
                <input type="button"  id="getPosition" class="myButton" value="Find" />
            </td>
        </tr>
        <tr>
            <td>
                Date & Time:
            </td>
            <td colspan="3">
                <asp:TextBox ID="timeTB" runat="server" Width="40" Text="18:00"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dateTB"
                    PopupButtonID="calanderBTN" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <asp:TextBox ID="dateTB" runat="server" CssClass="ggg"></asp:TextBox>
                <asp:ImageButton ID="calanderBTN" runat="server" ImageUrl="pic/Calendar.png" Width="16px" />
            </td>
        </tr>
        <tr>
            <td>
                Age Range:
            </td>
            <td>
                <asp:TextBox ID="MinAgeTxt" runat="server" Width="30" Text="25" />
            </td>
            <td style="width: 163px; height: 20px; vertical-align: top;">
                <asp:TextBox ID="sliderTwo" runat="server" />
                <asp:MultiHandleSliderExtender ID="multiHandleSliderExtenderTwo" runat="server" BehaviorID="multiHandleSliderExtenderTwo"
                    TargetControlID="sliderTwo" Minimum="0" Maximum="100" TooltipText="{0}" Orientation="Horizontal"
                    EnableHandleAnimation="true" EnableKeyboard="false" EnableMouseWheel="false"
                    ShowHandleDragStyle="true" ShowHandleHoverStyle="true" Length="160">
                    <MultiHandleSliderTargets>
                        <asp:MultiHandleSliderTarget ControlID="MinAgeTxt" />
                        <asp:MultiHandleSliderTarget ControlID="MaxAgeTxt" />
                    </MultiHandleSliderTargets>
                </asp:MultiHandleSliderExtender>
            </td>
            <td>
                <asp:TextBox ID="MaxAgeTxt" runat="server" Width="30" Text="30" />
            </td>
        </tr>
        <tr>
            <td>
                Event Type:
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="EventTypeRBL" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="false" Selected>Public</asp:ListItem>
                    <asp:ListItem Value="true">Private</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                Frequency:
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="FrequRBL" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected>Once</asp:ListItem>
                    <asp:ListItem Value="2">Every Week</asp:ListItem>
                    <asp:ListItem Value="3">Every Month</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                Comment:
            </td>
            <td colspan="3">
                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="commentsTB"
                    WatermarkText="Add your comment here">
                </asp:TextBoxWatermarkExtender>
                <asp:TextBox ID="commentsTB" runat="server" TextMode="MultiLine" CssClass="ggg"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Button ID="confirmBTN" CssClass="myButton"  runat="server" Text="Confirm & Publish"
                    OnClick="confirmBTN_Click" />
            </td>
            <td colspan="3">
                <br />
                <asp:HiddenField ID="CityHIde" runat="server" />
                <asp:HiddenField ID="LatLOngHIde" runat="server" />
                <asp:Button ID="inviteBTN" CssClass="myButton" runat="server" Text="Invite from list" Visible="false" />
            </td>
        </tr>
    </table>
    <body onload="start()">
        <div id="mapholder" style="border: 1px ridge #999999; height: 300px; width: 433px;">
        </div>
    </body>
</asp:Content>
