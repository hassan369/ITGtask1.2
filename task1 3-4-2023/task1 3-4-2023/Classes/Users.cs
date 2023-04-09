using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace task1_3_4_2023.Classes
{
    public class Users
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Language { get; set; }
        public string Password { get; set; }


        // Constructor to set property values when an object is instantiated
        public Users(string name, string userName, string gender, string userType, DateTime dateOfBirth, string language, string password)
        {
            Name = name;
            UserName = userName;
            Gender = gender;
            UserType = userType;
            DateOfBirth = dateOfBirth;
            Language = language;
            Password = password;
        }


        public void RegisterUser()
        {
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                con = new SqlConnection(connectionString);

                //call the procedures
                SqlCommand command = new SqlCommand("Registeration", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlCommand command2 = new SqlCommand("GetClients", con);
                command2.CommandType = System.Data.CommandType.StoredProcedure;

                //add properties to command
                command.Parameters.AddWithValue("@name", this.Name);
                command.Parameters.AddWithValue("@userName", this.UserName);
                command.Parameters.AddWithValue("@gender", this.Gender);
                command.Parameters.AddWithValue("@userType", this.UserType);
                command.Parameters.AddWithValue("@dateOfBirth", this.DateOfBirth);
                command.Parameters.AddWithValue("@language", this.Language);
                command.Parameters.AddWithValue("@password", this.Password);

                // Opening Connection  
                con.Open();
                // Executing the SQL query
                SqlDataReader sdr = command2.ExecuteReader();
                // Iterating Data  
                while (sdr.Read())
                {
                    if (sdr[2].ToString() == this.UserName)
                    {
                        throw new Exception("UserName already exists");
                    }
                }
                con.Close();
                con.Open();

                command.ExecuteNonQuery();
            }
            //if created two catch one for the sqlexception errors if there is conflex with foriegn key for example,
            //cause the normal catch wan't show me the error or throw any exception to the catch when calling the method
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key constraint violation error
                {
                    throw new Exception("Foreign key constraint violation.");
                }
                else // Other SQL error
                {
                    throw new Exception("SQL error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("OOPs, something went wrong: " + ex.Message);
            }
            finally
            {
                // Closing the connection  
                con.Close();
            }
        }
    }
}