using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ticketbooking
{
    public class Users
    {
        int _UserId;
        int _CustomerId;
        string _email;
        string _password;

        public int CustomerId { get => _CustomerId; set => _CustomerId = value; }
        public string email { get => _email; set => _email = value; }
        public string password { get => _password; set => _password = value; }
        public int UserId { get => _UserId; set => _UserId = value; }

        public Users(DataRow row)
        {
            _UserId = int.Parse(row["UserId"].ToString());
            _CustomerId = int.Parse(row["CustomerId"].ToString());
            _email = row["email"].ToString();
            _password = row["password"].ToString();

        }

        public Users()
        {
            _UserId = -1;
        }
        public void Save()
        {
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            Connect.Open();
            if (_CustomerId == -1) // first time
            {

                string isql = "INSERT INTO UserLogins (CustomerId,email,password) VALUES (?,?,?)";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("CustomerId", _CustomerId);
                Cmd.Parameters.AddWithValue("email", _email);
                Cmd.Parameters.AddWithValue("password", _password);


                Cmd.ExecuteNonQuery();
                //get the generated customerid
                Cmd.CommandText = "Select @@Identity";
                _CustomerId = (int)Cmd.ExecuteScalar();
            }
            else
            {
                string isql = "UPDATE UserLogins SET CustomerId =?,email=?,password=? WHERE UserId=?";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("CustomerId", _CustomerId);
                Cmd.Parameters.AddWithValue("email", _email);
                Cmd.Parameters.AddWithValue("password", _password);
                Cmd.Parameters.AddWithValue("UserId", _UserId);
                Cmd.ExecuteNonQuery();
            }
        }
    }
}
