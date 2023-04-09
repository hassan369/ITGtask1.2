<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="task1_3_4_2023.users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- SweetAlert2 CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert2/11.5.5/sweetalert2.min.css" integrity="sha512-tx5Ex5r5Gqf5rl+Rp5w13u5ilm+z6er7+bH9j9l8+GQJfUDSLx7dJJ/QvJ8kvjF9uGIXD+V7pYiNfJg7PGzsdw==" crossorigin="anonymous" referrerpolicy="no-referrer" />

    <!-- SweetAlert2 JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert2/11.5.5/sweetalert2.min.js" integrity="sha512-0cI4M4ZZenSRgUaD8WQ1I0SblmRy9XUvHW6bnlY6b+TfGOQXC6btlj8Bw+m6UOgzoEA7K9Xt6CJU6VE48b6T2Q==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="form-row">
    <div class="form-group col-md-4">
        <span class="form-check-label" for="txtNameFilter">UserName</span>
        <asp:TextBox CssClass="form-control" ID="txtNameFilter" runat="server"></asp:TextBox>
    </div>
    <div class="form-group col-md-4">
        <span class="form-check-label" for="ddlGender">Gender</span>
        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
            <asp:ListItem Text="Both" Value=""></asp:ListItem>
            <asp:ListItem Text="Male" Value="1"></asp:ListItem>
            <asp:ListItem Text="Female" Value="2"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="form-group col-md-4">
        <label>&nbsp;</label>
<asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="filter" CssClass="btn btn-primary form-control" style="margin-top: 23px;" />
    </div>
</div>






    <br />


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connection %>" DeleteCommand="DELETE FROM [clients] WHERE [id] = @id" InsertCommand="INSERT INTO [clients] ([name], [userName], [gender], [userType], [dateOfBirth], [language], [password]) VALUES (@name, @userName, @gender, @userType, @dateOfBirth, @language, @password)" SelectCommand="getClientsWithFilter" SelectCommandType="StoredProcedure" UpdateCommand="UPDATE [clients] SET [name] = @name, [userName] = @userName, [gender] = @gender, [userType] = @userType, [dateOfBirth] = @dateOfBirth, [language] = @language, [password] = @password WHERE [id] = @id" OnSelecting="SqlDataSource1_Selecting">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="userName" Type="String" />
            <asp:Parameter Name="gender" Type="Int32" />
            <asp:Parameter Name="userType" Type="Int32" />
            <asp:Parameter DbType="Date" Name="dateOfBirth" />
            <asp:Parameter Name="language" Type="Int32" />
            <asp:Parameter Name="password" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtNameFilter" Name="nameFilter" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="ddlGender" Name="genderFilter" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="userName" Type="String" />
            <asp:Parameter Name="gender" Type="Int32" />
            <asp:Parameter Name="userType" Type="Int32" />
            <asp:Parameter DbType="Date" Name="dateOfBirth" />
            <asp:Parameter Name="language" Type="Int32" />
            <asp:Parameter Name="password" Type="String" />
            <asp:Parameter Name="id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>



    
     



    <asp:GridView Visible="false" ID="gvShowClients" CssClass="table table-striped table-bordered table-responsive" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="False" DataKeyNames="id,id1,id2,id3" DataSourceID="SqlDataSource1">
        <Columns>
           
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" Visible="False" />
            <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
            <asp:BoundField DataField="userName" HeaderText="Username" SortExpression="userName" />
            <asp:TemplateField HeaderText="Gender" SortExpression="gender">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="id" SelectedValue='<%# Bind("gender") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connection %>" SelectCommand="GetGenders" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Role" SortExpression="userType">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource3" DataTextField="Name" DataValueField="id" SelectedValue='<%# Bind("userType") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:connection %>" SelectCommand="GetRoles" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("name2") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="dateOfBirth" HeaderText="Birth Daye Date" SortExpression="dateOfBirth" />
            <asp:TemplateField HeaderText="Language" SortExpression="language">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="SqlDataSource4" DataTextField="Name" DataValueField="id" SelectedValue='<%# Bind("language") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:connection %>" SelectCommand="GetLanguages" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("name3") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id1" HeaderText="id1" InsertVisible="False" ReadOnly="True" SortExpression="id1" Visible="False" />
            <asp:BoundField DataField="Name1" HeaderText="Name1" SortExpression="Name1" Visible="False" />
            <asp:BoundField DataField="id2" HeaderText="id2" InsertVisible="False" ReadOnly="True" SortExpression="id2" Visible="False" />
            <asp:BoundField DataField="Name2" HeaderText="Name2" SortExpression="Name2" Visible="False" />
            <asp:BoundField DataField="id3" HeaderText="id3" InsertVisible="False" ReadOnly="True" SortExpression="id3" Visible="False" />
            <asp:BoundField DataField="Name3" HeaderText="Name3" SortExpression="Name3" Visible="False" />
            <asp:CommandField HeaderText="Options" ShowDeleteButton="True" ShowEditButton="True" ControlStyle-CssClass="btn btn-primary">
                <ControlStyle CssClass="btn btn-primary"></ControlStyle>
            </asp:CommandField>
        </Columns>
    </asp:GridView>
    <br />
  

    <%-- ------------------------------------------------------------------------------ --%>
    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-responsive" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting" AllowSorting="true" AllowPaging="True" PageSize="2" OnPageIndexChanging="GridView1_PageIndexChanging">
        <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" Visible="false" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:TemplateField HeaderText="User Name" SortExpression="UserName">
                <ItemTemplate>
                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                <ItemTemplate>
                    <asp:Label ID="lblGender" runat="server" Text='<%# GetGenderTextWrapper(Eval("Gender").ToString()) %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlGender" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Role" SortExpression="UserType">
                <ItemTemplate>
                    <asp:Label ID="lblUserType" runat="server" Text='<%# GetUserTypeTextWrapper(Eval("UserType").ToString()) %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlUserType" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Date Of Birth" SortExpression="DateOfBirth" >
                <ItemTemplate>
                    <asp:Label ID="lblDateOfBirth" runat="server" Text='<%# Convert.ToDateTime(Eval("DateOfBirth")).ToString("yyyy-MM-dd") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <input id="inputDateOfBirth" runat="server" type="date" value='<%# Bind("DateOfBirth", "{0:yyyy-MM-dd}") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Language" SortExpression="Language">
                <ItemTemplate>
                    <asp:Label ID="lblLanguage" runat="server" Text='<%# GetLanguageTextWrapper(Eval("Language").ToString()) %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlLanguage" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Options">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn btn-primary" Text="Edit"></asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn btn-danger" Text="Delete" OnClientClick="return confirmDelete(this);"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" CssClass="btn btn-primary" Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CssClass="btn btn-secondary" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>


   
    <br />
   
    <div id="emptyDiv" runat="server"></div>
    <script type="text/javascript">
        function confirmDelete(deleteButton) {
            var result = confirm("Are you sure you want to delete this record?");
            return result;
            //if the result was false the onRowDeleting method will not be called
            //the CommandName in the linkbutton connect the button with the OnRowDeleting method
        }
    </script>
</asp:Content>
