using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using task1_3_4_2023.Classes;

namespace task1_3_4_2023
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbName.Value == "" || tbPassword.Value == "" )
            {
                lblResponseMessage.Text += "please fill all fields";
                return;
            }
            try
            {
                Login2 login = new Login2();
                Login2 res = login.LoginAttempet(tbName.Value, tbPassword.Value);
                Session["userId"] = res.Id;
                Session["name"] = res.Name;
                Session["UserName"] = res.Username;
                Response.Redirect("users.aspx");
            }
            catch (Exception ex)
            {
                lblResponseMessage.Text  = ex.Message;
                
            }

        }
    }
}