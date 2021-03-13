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
    public class Customers
    {
        int _CustomerId;
        string _Firstname;
        string _Lastname;
        string _Email;

        public int CustomerId { get => _CustomerId; set => _CustomerId = value; }
        public string Firstname { get => _Firstname; set => _Firstname = value; }
        public string Lastname { get => _Lastname; set => _Lastname = value; }
        public string Email { get => _Email; set => _Email = value; }

        public Customers(DataRow row)
        {
            _CustomerId = int.Parse(row["CustomerID"].ToString());
            _Firstname = row["Firstname"].ToString();
            _Lastname = row["Lastname"].ToString();
            _Email = row["Email"].ToString();
        }

        public Customers()
        {
            _CustomerId = -1;
        }
        public void Save()
        {
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            Connect.Open();
            if (_CustomerId == -1) // first time
            {

                string isql = "INSERT INTO Customers (FirstName,LastName,Email,) VALUES (?,?,?)";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("FirstName", _Firstname);
                Cmd.Parameters.AddWithValue("LastName", _Lastname);
                Cmd.Parameters.AddWithValue("Email", _Email);

                Cmd.ExecuteNonQuery();
                //get the generated customerid
                Cmd.CommandText = "Select @@Identity";
                _CustomerId = (int)Cmd.ExecuteScalar();
            }
            else
            {
                string isql = "UPDATE Customers SET Firstname=?,Lastname=?,Email=? WHERE CustomerId=?";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("FirstName", _Firstname);
                Cmd.Parameters.AddWithValue("LastName", _Lastname);
                Cmd.Parameters.AddWithValue("Email", _Email);
                Cmd.Parameters.AddWithValue("CustomerId", _CustomerId);
                Cmd.ExecuteNonQuery();
            }
        }
    }
}
