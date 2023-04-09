using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace task1_3_4_2023.Classes
{
    public class ShowUsers
    {
        //in this case it's better to have all the proprites as the same dataType as in the database
        //if you need to have the id as string just add another property with as ID but string 
        //also for date time and all other dataTypes
        public string ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string UserType { get; set; }
        public string DateOfBirth { get; set; }
        public DateTime DateOfBirthDate { get; set; }
        public string Language { get; set; }

        public static List<ShowUsers> GetUsers(string nameFilter, string genderFilter)
        {
            List<ShowUsers> users = new List<ShowUsers>();

            // Connect to the database
            string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //string query = "SELECT * FROM clients"; 
                using (SqlCommand command = new SqlCommand("getClientsWithFilter2", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (string.IsNullOrEmpty(nameFilter))
                    {
                        command.Parameters.AddWithValue("@nameFilter", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@nameFilter", nameFilter);
                    }
                    if (string.IsNullOrEmpty(genderFilter))
                    {
                        command.Parameters.AddWithValue("@genderFilter", DBNull.Value);
                    }
                    else
                    {
                        int genderFilterInt = Convert.ToInt32(genderFilter);
                        command.Parameters.AddWithValue("@genderFilter", genderFilterInt);
                    }
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShowUsers user = new ShowUsers();
                            user.ID = reader["ID"].ToString();
                            user.Name = reader["Name"].ToString();
                            user.UserName = reader["UserName"].ToString();
                            // befor it was retriving text. so when is say ddl.selectedValue = user.gender (it wont work)
                            user.Gender = reader["Gender"].ToString(); 
                            user.UserType = reader["UserType"].ToString(); 
                            user.DateOfBirth = reader["DateOfBirth"].ToString();
                            user.DateOfBirthDate = DateTime.Parse(reader["DateOfBirth"].ToString()); 
                            user.Language = reader["Language"].ToString(); 
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public static string GetGenderText(string genderId)
        {
            // Implement logic to get gender text by ID
            
             if (genderId == "1") return "Male";
             if (genderId == "2") return "Female";
             return "Unknown";
        }

        public static string GetUserTypeText(string userTypeId)
        {
            // Implement logic to get user type text by ID
            
             if (userTypeId == "1") return "Admin";
             if (userTypeId == "2") return "Techer";
             if (userTypeId == "3") return "Studient";
             return "Unknown";
        }

        public static string GetLanguageText(string languageId)
        {
            // Implement logic to get language text by ID
            
             if (languageId == "1") return "Arabic";
             if (languageId == "2") return "English";
             return "Unknown";
        }

        public void UpdateUserInDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdateClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", this.ID);
                    command.Parameters.AddWithValue("@Name", this.Name);
                    command.Parameters.AddWithValue("@UserName", this.UserName);
                    command.Parameters.AddWithValue("@Gender", this.Gender);
                    command.Parameters.AddWithValue("@UserType", this.UserType);
                    command.Parameters.AddWithValue("@DateOfBirth", this.DateOfBirth);
                    command.Parameters.AddWithValue("@Language", this.Language);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteUserInDatabase(string userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DeleteClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<ShowUsers> GetDataSorted(string sortExpression)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            List<ShowUsers> users = new List<ShowUsers>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM clients ORDER BY {sortExpression}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ShowUsers user = new ShowUsers
                            {
                                ID = reader["ID"].ToString(),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                Gender = reader["Gender"].ToString(),
                                UserType = reader["UserType"].ToString(),
                                DateOfBirth = reader["DateOfBirth"].ToString(),
                                Language = reader["Language"].ToString(),
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
   

    }
}