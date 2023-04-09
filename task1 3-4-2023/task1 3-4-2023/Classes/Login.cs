using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace task1_3_4_2023.Classes
{
    public class Login2
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }


        //public login(string username, string password)
        //{
        //    Username = username;
        //    Password = password;
        //}

        public Login2 LoginAttempet(string username, string password)
        {
            SqlConnection con = null;
            
                // Creating Connection  
                string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                con = new SqlConnection(connectionString);

                //call the procedures

                SqlCommand command2 = new SqlCommand("GetClients", con);
                command2.CommandType = System.Data.CommandType.StoredProcedure;


                // Opening Connection  
                con.Open();

                // Executing the SQL query  
                SqlDataReader sdr = command2.ExecuteReader();
                // Iterating Data  
                
                while (sdr.Read())
                {
                    string UserName = Convert.ToString(sdr[2]);
                    string RightPassword = Convert.ToString(sdr[7]);
                    if (username == UserName && password == RightPassword)
                    {


                        //SET isUserAuthenticated to true
                        

                        //Save user info to the session 
                        Login2 logedUser = new Login2();
                        logedUser.Id = Convert.ToInt32(sdr[0]);
                        logedUser.Name = Convert.ToString(sdr[1]);
                        logedUser.Username = Convert.ToString(sdr[2]);
                       

                        return logedUser;

                       
                    }

                }
                
                    //lblResponseMessage.Text = "wrong username or password";
                    throw new Exception("wrong username or password");
                    
               

           
            
        }
    }
}