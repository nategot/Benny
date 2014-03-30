<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false" NavigateUrl="~/RegisterNewUser.aspx">Register</asp:HyperLink>
        if you don't have an account.
    </p>
    <div class="accountInfo">
        <fieldset class="login">
            <legend>Account Information</legend>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="Email">Email:</asp:Label>
                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="Email"
                    CssClass="failureNotification" ErrorMessage="Email is required." ToolTip="Email is required."
                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:CheckBox ID="RememberMe" runat="server" />
                <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
            </p>
            <p>
                <asp:Label ID="AnsLogInLBL" CssClass="ErroerLBL" runat="server"></asp:Label>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"
                OnClick="LoginBTN_Load" />
        </p>
    </div>
</asp:Content>
