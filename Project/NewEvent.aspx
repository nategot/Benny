<%@ Page Title="New Event" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NewEvent.aspx.cs" Inherits="NewEvent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&libraries=places&language=he"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script src="Scripts/MapScriptNewEvent2.js" type="text/javascript"></script>
    <script src="Scripts/SmallPopUpScript.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <h1 class="addeventtitle">
        Create New Event</h1>
    <br />
    <br />
    <div id="dialog" style="display: none">
    </div>
    <table class="addeventable" style="float: left">
        <tr>
            <td>
                <asp:Label CssClass="addeventlabla" runat="server" Text="Label"> Category:</asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="categoryDDL" runat="server" CssClass="addeventinput">
                    <asp:ListItem Value="1">Soccer</asp:ListItem>
                    <asp:ListItem Value="2">Basketball</asp:ListItem>
                    <asp:ListItem Value="3">Tennis</asp:ListItem>
                    <asp:ListItem Value="4">Running</asp:ListItem>
                    <asp:ListItem Value="5">Cycling</asp:ListItem>
                    <asp:ListItem Value="6">Swimming</asp:ListItem>
                    <asp:ListItem Value="7">Volleyball</asp:ListItem>
                    <asp:ListItem Value="14">Surfing</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass="addeventlabla" runat="server" Text="Label">  Number of Participants:</asp:Label>
            </td>
            <td colspan="3">
                <asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="NOP"
                    Minimum="2" Maximum="100" TargetButtonDownID="downArrow" TargetButtonUpID="upArrow">
                </asp:NumericUpDownExtender>
                <asp:TextBox ID="NOP" Text="8" runat="server" Width="40" CssClass="addeventinput"></asp:TextBox>
                <input type="image" class="btnCh" id="downArrow" src="pic/down.gif" />
                <input type="image" class="btnCh" id="upArrow" src="pic/up.gif" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass="addeventlabla" runat="server" Text="Label">   Location:</asp:Label>
            </td>
            <td colspan="3">
                <input type="text" value="" style="width: 160px" class="addeventinput" id="locationTB"
                    placeholder="Enter a location" />
                <input type="button" id="getPosition" class="myButton" value="Find" style="width: 70px; height: 25px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass="addeventlabla" runat="server" Text="Label"> Date & Time:</asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="timeTB" runat="server" CssClass="addeventinput" Width="40" Text="18:00"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dateTB"
                    PopupButtonID="calanderBTN" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <asp:TextBox ID="dateTB" runat="server" CssClass="addeventinput"></asp:TextBox>
                <asp:ImageButton ID="calanderBTN" runat="server" ImageUrl="pic/Calendar.png" Width="16px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass="addeventlabla" runat="server" Text="Label"> Age Range: </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="MinAgeTxt" runat="server" Width="30" Text="25" />
            </td>
            <td style="width: 163px; height: 20px; vertical-align: top;">
                <asp:TextBox ID="sliderTwo" runat="server" />
                <asp:MultiHandleSliderExtender ID="multiHandleSliderExtenderTwo" runat="server" BehaviorID="multiHandleSliderExtenderTwo"
                    TargetControlID="sliderTwo" Minimum="8" Maximum="70" TooltipText="{0}" Orientation="Horizontal"
                    EnableHandleAnimation="true" EnableKeyboard="false" EnableMouseWheel="false"
                    ShowHandleDragStyle="true" ShowHandleHoverStyle="true" Length="160" IsReadOnly="False">
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
                <asp:Label CssClass="addeventlabla" runat="server" Text="Label">  Event Type: </asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="EventTypeRBL" Width="200px" runat="server" CssClass="addeventinput">
                    <asp:ListItem Value="false">Public</asp:ListItem>
                    <asp:ListItem Value="true">Private</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass="addeventlabla" runat="server" Text="Label">  Frequency: </asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="FrequRBL" Width="200px" runat="server" CssClass="addeventinput">
                    <asp:ListItem Value="1">Once</asp:ListItem>
                    <asp:ListItem Value="2">Every Week</asp:ListItem>
                    <asp:ListItem Value="3">Every Month</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" CssClass="addeventlabla" runat="server" Text="Label">  Comment: </asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="commentsTB"
                    WatermarkText="Add your comment here">
                </asp:TextBoxWatermarkExtender>
                <asp:TextBox ID="commentsTB" runat="server" class="addeventinput" TextMode="MultiLine"
                    Style="width: 200px; height: 40px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Button ID="confirmBTN" CssClass="myButton" runat="server" Text="Confirm & Publish"
                    OnClick="confirmBTN_Click" Width="150px" />
            </td>
            <td colspan="3">
                <br />
                <asp:HiddenField ID="CityHIde" runat="server" />
                <asp:HiddenField ID="LatLOngHIde" runat="server" />
                <asp:Button ID="inviteBTN" CssClass="myButton" runat="server" Text="Invite from list"
                    Visible="false" />
            </td>
        </tr>
    </table>
      <body onload="start()">
        <div id="mapholder" style="border: 1px ridge #999999; height: 300px; width: 433px; ">
           
        </div>
    </body>
</asp:Content>
