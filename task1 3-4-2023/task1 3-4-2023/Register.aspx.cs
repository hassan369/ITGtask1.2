using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using task1_3_4_2023.Classes;

namespace task1_3_4_2023
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlGender.AppendDataBoundItems = true;
            ListItem selectGender = new ListItem("Select Gender", "-1");
            ddlGender.Items.Insert(0, selectGender);

            ddlRole.AppendDataBoundItems = true;
            ListItem selectRole = new ListItem("Select Role", "-1");
            ddlRole.Items.Insert(0, selectRole);

            ddlLanguage.AppendDataBoundItems = true;
            ListItem selectLanguage = new ListItem("Select Language", "-1");
            ddlLanguage.Items.Insert(0, selectLanguage);

            SqlConnection con = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

                con = new SqlConnection(connectionString);

                SqlCommand cm = new SqlCommand("GetGenders", con);
                cm.CommandType = CommandType.StoredProcedure;

                SqlCommand cm2 = new SqlCommand("GetRoles", con);
                cm2.CommandType = CommandType.StoredProcedure;


                SqlCommand cm3 = new SqlCommand("GetLanguages", con);
                cm3.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable table = new DataTable();
                adapter.Fill(table);


                SqlDataAdapter adapter2 = new SqlDataAdapter(cm2);
                DataTable table2 = new DataTable();
                adapter2.Fill(table2);

                SqlDataAdapter adapter3 = new SqlDataAdapter(cm3);
                DataTable table3 = new DataTable();
                adapter3.Fill(table3);



                //fill ddlGender with the values form GetGenders procedure 
                ddlGender.DataSource = table;
                ddlGender.DataTextField = "Name";
                ddlGender.DataValueField = "id";
                ddlGender.DataBind();

                //fill ddlRole with the values form GetRoles procedure 
                ddlRole.DataSource = table2;
                ddlRole.DataTextField = "Name";
                ddlRole.DataValueField = "id";
                ddlRole.DataBind();

                //fill ddlLanguage with the values form GetLanguage procedure 
                ddlLanguage.DataSource = table3;
                ddlLanguage.DataTextField = "Name";
                ddlLanguage.DataValueField = "id";
                ddlLanguage.DataBind();
            }
            catch (Exception E)
            {
                //Console.WriteLine("something went wrong." + E);
                lblErrorMessage.Text = "OOPs, something went wrong." + E;
            }
          
            finally
            {
                con.Dispose();
                con.Close();
            }
        }

        protected void btnRegisterSubmit_Click(object sender, EventArgs e)
        {

            if (tbName.Text == "" || tbUserName.Text == "" || tbPassword.Text == "" || ddlLanguage.SelectedValue == "-1" || ddlRole.SelectedValue == "-1" || ddlGender.SelectedValue == "-1")
            {
                lblErrorMessage.Text += "please fill all fields";
                return;
            }

            try
            {
                Users newUser = new Users(
                    tbName.Text,
                    tbUserName.Text,
                    ddlGender.SelectedValue,
                    ddlRole.SelectedValue,
                    Convert.ToDateTime(tbBirthdate.Value),
                    ddlLanguage.SelectedValue,
                    tbPassword.Text
                );

                newUser.RegisterUser();
            }
            catch (Exception ex)
            {
                string message = "Error: " + ex.Message.Replace("'", "\\'");
                string script = "<script type=\"text/javascript\">alert('" + message + "');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", script);
            }
        }

        protected void CvGender_server(object source, ServerValidateEventArgs args)
        {
            if (ddlGender.SelectedValue == "-1")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void CvRole_server(object source, ServerValidateEventArgs args)
        {
            if (ddlRole.SelectedValue == "-1")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void CvLanguage_server(object source, ServerValidateEventArgs args)
        {
            if (ddlLanguage.SelectedValue == "-1")
            {
                args.IsValid = false;
               
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}