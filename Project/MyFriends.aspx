<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MyFriends.aspx.cs" Inherits="MyFriends" %>


    


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

 <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="Styles/scrolltest/perfect-scrollbar.js" type="text/javascript"></script>
    <link href="Styles/scrolltest/perfect-scrollbar.css" rel="stylesheet" type="text/css" />
    <script src="Styles/scrolltest/jquery.mousewheel.js" type="text/javascript"></script>
<style>
      #description {
        border: 3px solid gray;
        height:300px;
        width: 280px;
        overflow: hidden;
        position: absolute;
        background-color:White;
      }
      
      
      
    </style>
    <script type="text/javascript">
        $(document).ready(function ($) {
            $('#description').perfectScrollbar({
                wheelSpeed: 20,
                wheelPropagation: false
            });
        });
    </script>



    
        <div id="buildGroupsDiv" runat="server" style="float: right; padding-right: 30px;">
            <asp:Label ID="Label1" runat="server" Text="Create new group"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="group name:"></asp:Label>
            <asp:TextBox ID="groupnameTb" runat="server"></asp:TextBox><asp:Button ID="creategroupBtn"
                runat="server" Text="Add group" OnClick="creategroupBtn_Click" />
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="SqlDataSource1"
                DataTextField="Email" DataValueField="Email">
            </asp:CheckBoxList>
            <asp:TextBox ID="emailaddtb" runat="server"></asp:TextBox>
            <asp:Button ID="Addtochek" runat="server" Text="Add" OnClick="Button1_Click1" />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:bgroup14_test1ConnectionString %>"
                SelectCommand="SELECT [Email], [Fname], [Lname] FROM [Users]"></asp:SqlDataSource>
        </div>
   

    <div id="description">
    <div id="userGvdiv" style="float: left; margin-left:4px;" runat="server">
        <asp:GridView ID="userGride" runat="server" BackColor="White" RowStyle-BorderColor="Black"    
            BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            ForeColor="Black" GridLines="Horizontal">
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <asp:Button ID="SendFListbtn" runat="server" Text="Invite" OnClick="Button1_Click" />
    </div>
    <div id="tbInvite" style="float: left" runat="server">
        <asp:TextBox ID="emailTb" runat="server"></asp:TextBox>
        <asp:Button ID="AddBtn" runat="server" Text="Add" OnClick="AddBtn_Click" />
        <asp:CheckBoxList ID="userBuletListe" runat="server" BorderStyle="Solid" BulletImageUrl="~/pic/blue-dot.png"
            Font-Bold="True" Font-Italic="True" BorderColor="White" ForeColor="White">
        </asp:CheckBoxList>
        <asp:Button ID="SendBTn" runat="server" Text="Send To List" OnClick="SendBTn_Click" />
    </div>
     </div>
</asp:Content>
