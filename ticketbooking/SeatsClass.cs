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
    class SeatsClass
    {
        string _seatValue;
        int _status;
        int _seatId;

        public string seatValue { get => _seatValue; set => _seatValue = value; }
        public int status { get => _status; set => _status = value; }
        public int seatId { get => _seatId; set => _seatId = value; }


        public SeatsClass(DataRow row)
        {
            //_seatId = int.Parse(row["seatId"].ToString());
            _seatValue = (row["seatValue"].ToString());
            _status = int.Parse(row["status"].ToString());
        }

        public SeatsClass()
        {

            _seatId = -1;

        }
        public void Save()
        {
            SqlConnection Connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            Connect.Open();
            if (_seatId == -1) // first time
            {

                string isql = "INSERT INTO Seats (seatValue, status) VALUES (?,?)";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("seatValue", _seatValue);
                Cmd.Parameters.AddWithValue("status", _status);

                Cmd.ExecuteNonQuery();
                //get the generated seatid
                Cmd.CommandText = "Select @@Identity";
                _seatId = (int)Cmd.ExecuteScalar();
            }
            else
            {
                string isql = "UPDATE Seats SET seatValue=?,status=? WHERE seatId=?";
                SqlCommand Cmd = new SqlCommand(isql, Connect);
                Cmd.Parameters.AddWithValue("seatValue", _seatValue);
                Cmd.Parameters.AddWithValue("status", _status);

                Cmd.ExecuteNonQuery();
            }
        }
    }
}