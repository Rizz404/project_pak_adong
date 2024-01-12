<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UAS_tipe_A.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login 01</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />

    <link rel="stylesheet" href="login-form-11/css/style.css" />
</head>
<body>
    <section class="ftco-section">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-7 col-lg-5">
                    <div class="login-wrap p-4 p-md-5">
                        <div class="icon d-flex align-items-center justify-content-center">
                            <span class="fa fa-user-o"></span>
                        </div>
                        <h3 class="text-center mb-4">Sign In</h3>
                        <form runat="server" class="login-form">
                            <div class="form-group">
                                <asp:TextBox ID="TxtUsername" runat="server" placeholder="Username" CssClass="form-control rounded-left"></asp:TextBox>
                            </div>
                            <div class="form-group d-flex">
                                <asp:TextBox ID="TxtPassword" runat="server" placeholder="Password" CssClass="form-control rounded-left" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="BtnLogin" runat="server" Text="Login" CssClass="form-control btn btn-primary rounded submit px-3" OnClick="BtnLogin_Click" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script src="login-form-11/js/jquery.min.js"></script>
    <script src="login-form-11/js/popper.js"></script>
    <script src="login-form-11/js/bootstrap.min.js"></script>
    <script src="login-form-11/js/main.js"></script>
</body>
</html>
