<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NewProfile.aspx.cs" Inherits="NewProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <h1 style="text-align: center">
        Create Your Profile</h1>
    <br />
    <br />
    <table style="float: left">
        <tr>
            <td>
                First Name:
            </td>
            <td>
                <asp:TextBox ID="fnameTB" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Last Name:
            </td>
            <td>
                <asp:TextBox ID="lnameTB" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Age:
            </td>
            <td>
                <asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="NOP"
                    Minimum="0" Maximum="100" TargetButtonDownID="downArrow" TargetButtonUpID="upArrow">
                </asp:NumericUpDownExtender>
                <asp:TextBox ID="NOP" Text="25" runat="server" Width="40"></asp:TextBox>
                <input type="image" id="downArrow" src="Images/down.gif" style="height: 15px; width: 15px;" />
                <input type="image" id="upArrow" src="Images/up.gif" style="height: 15px; width: 15px;" />
            </td>
        </tr>
        <tr>
            <td>
                E-mail:
            </td>
            <td>
                <asp:TextBox ID="emailTB" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                User Name:
            </td>
            <td>
                <asp:TextBox ID="unameTB" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Password:
            </td>
            <td>
                <asp:TextBox ID="passTB" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Verify Password:
            </td>
            <td>
                <asp:TextBox ID="vpassTB" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <div style="float: right;">
        <asp:Image ID="Image1" runat="server" ImageUrl="Images/EmptyProfile.jpg" />
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
    <div style="margin: 300px 0px 0px 0px; text-align: center">
        <asp:Button ID="saveBTN" runat="server" Text="Save Profile" 
            onclick="saveBTN_Click" />
    </div>
</asp:Content>
