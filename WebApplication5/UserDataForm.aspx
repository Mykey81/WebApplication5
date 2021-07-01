<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDataForm.aspx.cs" Inherits="WebApplication5.UserDataForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="Content/myStyles.css" />
    <script src="Scripts/validateForm.js" defer="defer"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form">
            <h2>Add new contact person</h2>
            <form id="user-data" method="post">
                <label for="fname">First name</label><br />
                <input type="text" id="fname" name="fname" /><br />
                <label for="fname" id="fname_err" class="error"></label><br />

                <label for="lname">Last name</label><br />
                <input type="text" id="lname" name="lname" /><br />
                <label for="lname" id="lname_err" class="error"></label><br />

                <label for="phone">Phone number</label><br />
                <input type="text" id="phone" name="phone" /><br />
                <label for="phone" id="phone_err" class="error"></label><br />

                <label for="email">Email address</label><br />
                <input type="text" id="email" name="email" /><br />
                <label for="fname" id="email_err" class="error"></label><br />

                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" OnClientClick="return Person.validateForm()" Text="Add" />                
            </form>    
        </div>
        <div class="table">
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
