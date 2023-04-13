using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using task1_3_4_2023.Classes;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;

namespace task1_3_4_2023
{
    public partial class users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //for gridView1
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

       
        //for the first gridview (the sone with sqldatasource)
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            // Update the parameters of the SqlDataSource with the new values in the textbox
            SqlDataSource1.SelectParameters["nameFilter"].DefaultValue = txtNameFilter.Text;
            SqlDataSource1.SelectParameters["genderFilter"].DefaultValue = ddlGender.SelectedValue;

            // Refresh the data by calling the Select method of the SqlDataSource
            gvShowClients.DataBind();

            //this to bind gridView1
            BindGridView();


        }
        //for the first gridview
        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNameFilter.Text))
            {
                e.Command.Parameters["@nameFilter"].Value = DBNull.Value;
            }
            if (string.IsNullOrEmpty(ddlGender.SelectedValue))
            {
                e.Command.Parameters["@genderFilter"].Value = DBNull.Value;
            }
        }

        

        
        private void BindGridView()
        {
            string nameFilter = txtNameFilter.Text;
            string genderFilter = ddlGender.SelectedValue;

            List<ShowUsers> users = ShowUsers.GetUsers(nameFilter, genderFilter);
            if(users.Count == 0 )
            {
                emptyDiv.InnerHtml = "<p> there is no data </p>";
            }
            GridView1.DataSource = users;
            GridView1.DataBind();
        }




        /*
            object sender: object that raised the event (in this case GridView)
            e, an instance of GridViewEditEventArgs: containing event data.

            EditIndex: determine the index of the row currently in edit mode.
            e.NewEditIndex:  index of the row being edited.

            BindGridView():  refreshing the GridView, which will now display the specified row in edit mode
         */
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
            

        }


        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // he didn't just call ddlGender, because there is more than one, one in each row
            DropDownList ddlGender = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlGender");
            DropDownList ddlUserType = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlUserType");
            DropDownList ddlLanguage = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlLanguage");

            ShowUsers user = new ShowUsers
            {
                //with boundField we use rows.cells.controls, but with templateField we use findControl
                ID = GridView1.DataKeys[e.RowIndex].Value.ToString(),
                Name = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text,
                UserName = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblUserName")).Text,
                Gender = ddlGender.SelectedValue,
                UserType = ddlUserType.SelectedValue,
                DateOfBirth = ((HtmlInputGenericControl)GridView1.Rows[e.RowIndex].FindControl("inputDateOfBirth")).Value,
                Language = ddlLanguage.SelectedValue
            };

            UpdateUser(user);
            // seting the editIndex to -1, where -1 will never match any row cuase it's 0 index , so no row will be in edit mode  
            GridView1.EditIndex = -1;
            BindGridView();
        }


        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string userId = GridView1.DataKeys[e.RowIndex].Value.ToString();
            DeleteUser(userId);
            BindGridView();
        }

        public static void UpdateUser(ShowUsers user)
        {
            user.UpdateUserInDatabase();
        }

        public static void DeleteUser(string userId)
        {
            ShowUsers.DeleteUserInDatabase(userId);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
    {
                
        ShowUsers user = (ShowUsers)e.Row.DataItem;


                
        DropDownList ddlGender = (DropDownList)e.Row.FindControl("ddlGender");
        DropDownList ddlUserType = (DropDownList)e.Row.FindControl("ddlUserType");
        DropDownList ddlLanguage = (DropDownList)e.Row.FindControl("ddlLanguage");



        HtmlInputGenericControl inputDateOfBirth = (HtmlInputGenericControl)e.Row.FindControl("inputDateOfBirth");
        inputDateOfBirth.Value = user.DateOfBirthDate.ToString("yyyy-MM-dd");
        // Populate Gender DropDownList
        ddlGender.Items.Add(new ListItem("Male", "1"));
        ddlGender.Items.Add(new ListItem("Female", "2"));

        // Populate UserType DropDownList
        ddlUserType.Items.Add(new ListItem("Teacher", "2"));
        ddlUserType.Items.Add(new ListItem("Student", "3"));

        // Populate Language DropDownList
        ddlLanguage.Items.Add(new ListItem("Arabic", "1"));
        ddlLanguage.Items.Add(new ListItem("English", "2"));

        // Set the selected values
        ddlGender.SelectedValue = user.Gender;
        ddlUserType.SelectedValue = user.UserType;
        ddlLanguage.SelectedValue = user.Language;
    }

    if (e.Row.RowType == DataControlRowType.DataRow)
    {
        LinkButton deleteButton = e.Row.Cells[e.Row.Cells.Count - 1].Controls[0] as LinkButton;
        if (deleteButton != null && deleteButton.CommandName == "Delete")
        {
            // Add the command-argument attribute to store the row index
            deleteButton.Attributes["command-argument"] = e.Row.RowIndex.ToString();

            // Add the onclick attribute to show a normal JavaScript alert
            deleteButton.Attributes["onclick"] = "return confirm('Are you sure you want to delete this user?');";
        }
    }
}



        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Get the data with the specified sort expression.
            List<ShowUsers> data = ShowUsers.GetDataSorted(e.SortExpression);

            // Bind the sorted data to the GridView.
            GridView1.DataSource = data;
            GridView1.DataBind();
        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }



        public string GetGenderTextWrapper(string genderId)
        {
            return ShowUsers.GetGenderText(genderId);
        }
        public string GetUserTypeTextWrapper(string UserTypeId)
        {
            return ShowUsers.GetUserTypeText(UserTypeId);
        }
        public string GetLanguageTextWrapper(string LanguageId)
        {
            return ShowUsers.GetLanguageText(LanguageId);
        }

        
    }
}