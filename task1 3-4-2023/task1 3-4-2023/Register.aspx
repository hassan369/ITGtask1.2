<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="task1_3_4_2023.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="main">

     <!-- Sign up form -->
        <section class="signup">
            <div class="container">
                <div class="signup-content">
                    <div class="signup-form">
                        <h2 class="form-title">Sign up</h2>
                        <div class="register-form" id="register-form">
                            <div class="form-group">
                                <%--<label for="name">Your Name</label>--%>
                                <asp:RequiredFieldValidator ID="rvName" runat="server" ErrorMessage="Name is required" ControlToValidate="tbName" ValidationGroup="RegistrationGroup">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbName" runat="server" Placeholder="Your Name"></asp:TextBox>
                                <%--<input type="text" ID="name" runat="server" placeholder="Your Name"/>--%>
                            </div>
                            <div class="form-group">
                                 <asp:RequiredFieldValidator ID="rvUserName" runat="server" ErrorMessage="UserName is required" ControlToValidate="tbUserName" ValidationGroup="RegistrationGroup">*</asp:RequiredFieldValidator>
                                  <asp:TextBox ID="tbUserName" runat="server" Placeholder="Your UserName"></asp:TextBox>
                                  <%--                                <label for="email"><i class="zmdi zmdi-email"></i></label>--%><%--<input type="email" name="email" id="email" placeholder="Your UserName"/>--%>
                            </div>
                            <div class="form-group">
                                <%--<asp:RangeValidator ID="rvPassword2" runat="server" ControlToValidate="tbPassword" ErrorMessage="Password length must be between 7 and 25" MinimumValue="7" MaximumValue="25" ValidationGroup="RegistrationGroup"></asp:RangeValidator>--%>
                                <asp:RequiredFieldValidator ID="rvPassword" runat="server" ErrorMessage="password is required" ControlToValidate="tbPassword" ValidationGroup="RegistrationGroup">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbPassword" runat="server" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                <%-- <input type="password" name="pass" id="pass" placeholder="Password"/>--%>
                            </div>
                            <div class="form-group">
                                <asp:RequiredFieldValidator ID="rvBirthdate" runat="server" ErrorMessage="BirthDate is required" ControlToValidate="tbBirthdate" ValidationGroup="RegistrationGroup">*</asp:RequiredFieldValidator>

                                <input type="date" id="tbBirthdate" runat="server" />
                            </div>
                             <div class="form-group">
                                 <asp:CustomValidator ID="cvGender" runat="server" ControlToValidate="ddlGender"
                                     ErrorMessage="Please select gender" ValidationGroup="RegistrationGroup"
                                     OnServerValidate="CvGender_server"></asp:CustomValidator>
                                 <asp:DropDownList CssClass="dbStyle" ID="ddlGender" runat="server">
                                    
                                 </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:CustomValidator ID="cvRole" runat="server" ControlToValidate="ddlRole"
                                     ErrorMessage="Please select Role" ValidationGroup="RegistrationGroup"
                                     OnServerValidate="CvRole_server"></asp:CustomValidator>
                                 <asp:DropDownList CssClass="dbStyle" ID="ddlRole" runat="server">
                                    
                                 </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:CustomValidator ID="cvLanguage" runat="server" ControlToValidate="ddlLanguage"
                                     ErrorMessage="Please select Language" ValidationGroup="RegistrationGroup"
                                     OnServerValidate="CvLanguage_server"></asp:CustomValidator>
                                 <asp:DropDownList CssClass="dbStyle" ID="ddlLanguage" runat="server">
                                    
                                 </asp:DropDownList>
                            </div>
                            <%-- <div class="form-group">
                                <input type="checkbox" name="agree-term" id="agree-term" class="agree-term" />
                                <label for="agree-term" class="label-agree-term"><span><span></span></span>I agree all statements in  <a href="#" class="term-service">Terms of service</a></label>
                            </div>--%>
                            <div class="form-group form-button">
                                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:ValidationSummary ID="vsRegister" runat="server" ValidationGroup="RegistrationGroup" />
                                <br />
                                <asp:Button ID="btnRegisterSubmit"  class="form-submit" runat="server" Text="Register" OnClick="btnRegisterSubmit_Click" ValidationGroup="RegistrationGroup"/>
                                <%--<input type="submit" name="signup" id="signup" class="form-submit" value="Register"/>--%>
                            </div>
                        </div>
                    </div>
                    <div class="signup-image">
                        <figure><img src="images/signup-image.jpg" alt="sing up image"></figure>
<%--                        <a href="#" class="signup-image-link">I am already member</a>--%>
                    </div>
                </div>
            </div>
        </section>
         </div>
</asp:Content>
