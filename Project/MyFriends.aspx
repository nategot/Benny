<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MyFriends.aspx.cs" Inherits="MyFriends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
   
    <div id=buildGroupsDiv  runat="server" 
        style="float:right; padding-right: 30px;">     
        <asp:Label ID="Label1" runat="server" Text="Create new group"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="group name:"></asp:Label>
        <asp:TextBox ID="groupnameTb" runat="server"></asp:TextBox><asp:Button
            ID="creategroupBtn" runat="server" Text="Add group" 
            onclick="creategroupBtn_Click" />
     
       <asp:CheckBoxList ID="CheckBoxList1"
            runat="server" DataSourceID="SqlDataSource1" DataTextField="Email" 
            DataValueField="Email">
        </asp:CheckBoxList>
         <asp:TextBox ID="emailaddtb" runat="server"></asp:TextBox>
        <asp:Button ID="Addtochek" runat="server" Text="Add" onclick="Button1_Click1" />
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:bgroup14_test1ConnectionString %>" 
            SelectCommand="SELECT [Email], [Fname], [Lname] FROM [Users]">
        </asp:SqlDataSource>
      
    </div>

    <div id="userGvdiv" style="float:left"  runat="server">
        <asp:GridView ID="userGride" runat="server">
        </asp:GridView>
        <asp:Button ID="SendFListbtn" runat="server" Text="Invite" OnClick="Button1_Click" />
     
    </div>

     <div id="tbInvite" style="float: left "  runat="server" >
        <asp:TextBox ID="emailTb" runat="server"></asp:TextBox>
        <asp:Button ID="AddBtn" runat="server" Text="Add" OnClick="AddBtn_Click" />
         <asp:CheckBoxList ID="userBuletListe" runat="server" BorderStyle="Solid" BulletImageUrl="~/pic/blue-dot.png"
            Font-Bold="True" Font-Italic="True" BorderColor="White" ForeColor="White">
         </asp:CheckBoxList>
        <asp:Button ID="SendBTn" runat="server" Text="Send To List" OnClick="SendBTn_Click" />

    </div>
</asp:Content>
