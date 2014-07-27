<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MyFriends.aspx.cs" Inherits="MyFriends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="Styles/scrolltest/perfect-scrollbar.js" type="text/javascript"></script>
    <link href="Styles/scrolltest/perfect-scrollbar.css" rel="stylesheet" type="text/css" />
    <script src="Styles/scrolltest/jquery.mousewheel.js" type="text/javascript"></script>
        <script src="Scripts/SmallPopUpScript.js" type="text/javascript"></script>
    
    <style>
        #description
        {
            border: 3px solid gray;
            height: 350px;
            width: 280px;
            overflow: hidden;
            position: absolute;
            background-color: White;
            margin-left: 15%;
            margin-top: 8px;
            margin-bottom: 2%;
        }
        #group
        {
            border: 3px solid gray;
            height: 350px;
            width: 450px;
            overflow: hidden;
            position: absolute;
            background-color: White;
            margin-left: 50%;
            margin-top: 8px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function ($) {
            $('#description').perfectScrollbar({
                wheelSpeed: 20,
                wheelPropagation: false
            });
        });

        $(document).ready(function ($) {
            $('#group').perfectScrollbar({
                wheelSpeed: 20,
                wheelPropagation: false
            });
        });

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <div id="dialog" style="display: none">
    </div>
    <h1 style="text-align: center; font-size: xx-large; font-weight: bold; color: White;
        font-family: Narkisim;">
        Invite Friends
    </h1>
   
    <div id="group" style="float: right">
        &nbsp;
        <asp:Label ID="Label3" Text="Group name:" CssClass="lbltxtFriends" runat="server" />&nbsp;&nbsp;
        <asp:DropDownList ID="groupnameDDL" runat="server" DataSourceID="SqlDataSourcegroup"
            DataTextField="GroupName" DataValueField="GroupName" OnSelectedIndexChanged="groupnameDDL_SelectedIndexChanged"
            OnTextChanged="groupnameDDL_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
        &nbsp;
        <asp:Button ID="Button1" Text="invite group" CssClass="btnFriendsPage" runat="server"
            OnClick="Unnamed2_Click" />
        <br />
        <asp:GridView ID="userIngroupGv" runat="server" AutoGenerateColumns="False" DataSourceID="userInGroup"
            DataKeyNames="Num" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:ImageField DataImageUrlField="Picture">
                    <ControlStyle CssClass="imgFrinds" />
                </asp:ImageField>
                <asp:BoundField DataField="Fname" HeaderText="Fname" SortExpression="Fname" />
                <asp:BoundField DataField="Lname" HeaderText="Lname" SortExpression="Lname" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <asp:SqlDataSource ID="userInGroup" runat="server" ConnectionString="<%$ ConnectionStrings:bgroup14_test1ConnectionString %>"
            SelectCommand="SELECT [Picture], [Fname], [Lname], [Email], [Num] FROM [Groups] WHERE ([GroupName] = @GroupName)"
            DeleteCommand="DELETE FROM [Groups] WHERE [Num] = @Num" InsertCommand="INSERT INTO [Groups] ([Picture], [Fname], [Lname], [Email]) VALUES (@Picture, @Fname, @Lname, @Email)"
            UpdateCommand="UPDATE [Groups] SET [Picture] = @Picture, [Fname] = @Fname, [Lname] = @Lname, [Email] = @Email WHERE [Num] = @Num">
            <DeleteParameters>
                <asp:Parameter Name="Num" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Picture" Type="String" />
                <asp:Parameter Name="Fname" Type="String" />
                <asp:Parameter Name="Lname" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="groupnameDDL" Name="GroupName" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="Picture" Type="String" />
                <asp:Parameter Name="Fname" Type="String" />
                <asp:Parameter Name="Lname" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Num" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourcegroup" runat="server" ConnectionString="<%$ ConnectionStrings:bgroup14_test1ConnectionString %>"
            SelectCommand="SELECT DISTINCT [GroupName] FROM [Groups] WHERE ([AdminId] = @AdminId)">
            <SelectParameters>
                <asp:SessionParameter Name="AdminId" SessionField="UserId" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Button Text="Add New Users To Group" CssClass="btnFriendsPage-biger" runat="server"
            OnClick="Unnamed1_Click" />
        <br />
        <br />
        <asp:GridView ID="addtogroupGV" CssClass="TRTRT" runat="server" Visible="False" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Horizontal"
            ForeColor="Black">
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <asp:Button ID="addNewTogroup" runat="server" Text="Add To Group" Visible="false"
            CssClass="btnFriendsPage-big" OnClick="addNewTogroup_Click" />
        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Email is required field"
            ControlToValidate="newEmailTb"></asp:RequiredFieldValidator>--%>
    </div>
    

    <div id="description">
        <div style="margin-left: 1px;">
            &nbsp;
            <asp:Label ID="Label4" Text="  Invite From List  " runat="server" CssClass="lbltxtFriends" />
            &nbsp; &nbsp;
            <asp:Button ID="changeBtn" Text="create group" CssClass="btnFriendsPage" runat="server"
                OnClick="changeBtn_Click" />
        </div>
        <div id="userGvdiv" style="float: left; margin-left: 4px;" runat="server">
            <asp:GridView ID="userGride" runat="server" BackColor="White" RowStyle-BorderColor="Black"
                BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Horizontal">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </div>
        <asp:PlaceHolder ID="buildgroupPH" runat="server" Visible="false">
            <div id="buildGroupsDiv" runat="server" style="float: right; padding-right: 30px;">
                <asp:Label ID="Label1" runat="server" CssClass="lbltxtFriends" Text="Create New Group"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" CssClass="lbltxtFriends" Text="Group Name:"></asp:Label>
                <asp:TextBox Width="100px" ID="groupnameTb" runat="server" Text=""></asp:TextBox>
                <asp:Button ID="creategroupBtn" runat="server" CssClass="btnFriendsPage-big" Text="Add Group"
                    OnClick="creategroupBtn_Click" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="group name is a Required Field"
                    ControlToValidate="groupnameTb"></asp:RequiredFieldValidator>
                <%-- <asp:TextBox ID="emailaddtb" runat="server"></asp:TextBox>
                <asp:Button ID="Addtochek" runat="server" Text="Add" OnClick="Button1_Click1" />--%>
            </div>
        </asp:PlaceHolder>
        <%--build group--%>
        <asp:PlaceHolder runat="server" ID="invitPH">
            <div id="tbInvite" style="float: left" runat="server">
                <asp:TextBox ID="emailTb" placeholder="Insert email..." runat="server"></asp:TextBox>
                <asp:Button ID="AddBtn" runat="server" CssClass="btnFriendsPage-small" Text="Add"
                    OnClick="AddBtn_Click" />
                <asp:CheckBoxList ID="userBuletListe" runat="server" BorderStyle="Solid" BulletImageUrl="~/pic/blue-dot.png"
                    Font-Bold="True" Font-Italic="True" BorderColor="White" ForeColor="White">
                </asp:CheckBoxList>
                <%--  <asp:Button ID="SendBTn" runat="server" Text="Send To List" OnClick="SendBTn_Click" />--%>
                <p>
                    <asp:Button ID="SendFListbtn" runat="server" CssClass="btnFriendsPage-big" Text="Invite"
                        OnClick="Button1_Click" /></p>
            </div>
        </asp:PlaceHolder>
        <%--invite list--%>
    </div>
    <br />
    <br />
    <br />
    <br />
</asp:Content>
