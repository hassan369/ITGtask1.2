using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace task1_3_4_2023
{
    public partial class MainLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] == null)
            {
                logOut.Attributes.Add("style", "display:none");
                

            }

            if (Session["name"] != null)
            {
                headerText.InnerHtml = $" User: {Session["name"]}";
                login.Attributes.Add("style", "display:none");
                register.Attributes.Add("style", "display:none");

            }
          
            string currentPage = ContentPlaceHolder1.Page.GetType().Name.Replace("_aspx", "");

            header.InnerHtml = currentPage;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void logOut_Click(object sender, EventArgs e)
        {
          
            Session["name"] = null;
           
            Response.Redirect("login.aspx");
        }
    }
}