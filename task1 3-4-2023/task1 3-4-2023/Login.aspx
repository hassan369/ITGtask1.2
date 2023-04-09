<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="task1_3_4_2023.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="main">

       

        <!-- Sing in  Form -->
        <section class="sign-in">
            <div class="container">
                <div class="signin-content">
                    <div class="signin-image">
                        <figure><img src="images/signin-image.jpg" alt="sing up image"></figure>
                        <a href="Register.aspx" class="signup-image-link">Create an account</a>
                    </div>

                    <div class="signin-form">
                        <h2 class="form-title">Sign in</h2>
                        <div class="register-form" id="login-form">
                            <div class="form-group">
                                <asp:RequiredFieldValidator ID="rvUserName" runat="server" ErrorMessage="UserName is required" ControlToValidate="tbName" ValidationGroup="LoginGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <label for="your_name"><i class="zmdi zmdi-account material-icons-name"></i></label>
                                <input type="text"  id="tbName" runat="server" placeholder="Your Name"/>
                            </div>
                            <div class="form-group">
                              <asp:RequiredFieldValidator ID="rvPassword" runat="server" ErrorMessage="Password is required" ControlToValidate="tbPassword" ValidationGroup="LoginGroup" ForeColor="Red">*</asp:RequiredFieldValidator>

                                <label for="your_pass"><i class="zmdi zmdi-lock"></i></label>
                                <input type="password"  id="tbPassword" runat="server" placeholder="Password"/>
                            </div>
                           
                            <div class="form-group form-button">
                                <asp:Label ID="lblResponseMessage" runat="server"></asp:Label>
                                <br />
                                <asp:ValidationSummary ID="vsLogin" runat="server" ValidationGroup="LoginGroup" ForeColor="Red" />

                                <asp:Button ID="btnLogin" CssClass="form-submit" runat="server" Text="Log in" OnClick="btnLogin_Click" ValidationGroup="LoginGroup" />
                                <%--<input type="submit" name="signin" id="signin" class="form-submit" value="Log in"/>--%>
                            </div>
                        </div>
                        <div class="social-login">
                            <span class="social-label">Or login with</span>
                            <ul class="socials">
                                <li><a href="#"><i class="display-flex-center zmdi zmdi-facebook"></i></a></li>
                                <li><a href="#"><i class="display-flex-center zmdi zmdi-twitter"></i></a></li>
                                <li><a href="#"><i class="display-flex-center zmdi zmdi-google"></i></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
