using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace ticketbooking
{
    class UserLogin
    {
        public static void CreateAccount(string email, int customerid)
        {
            Console.Write("create password:");
            string enteredPass = Console.ReadLine();
            bool valid = ValidatePassword(enteredPass);
            if (valid)
            {
                Console.WriteLine("valid password \ncreating account...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                insertDetails(email, customerid, enteredPass);
            }
            else
            {
                Console.WriteLine("Password does not meet requirements \nEnter new Password:");
                string pass2 = Console.ReadLine();
                ValidatePassword(pass2);
            }

        }
        public static bool ValidatePassword(string password)
        {
            const int minLen = 7;
            bool PLen = false;
            bool PUpper = false;
            bool PLower = false;
            bool PDigit = false;
            if (password.Length >= minLen)
            {
                PLen = true;
            }
            if (PLen)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c))
                    {
                        PUpper = true;
                    }
                    if (char.IsLower(c))
                    {
                        PLower = true;
                    }
                    if (char.IsDigit(c))
                    {
                        PDigit = true;
                    }
                }
            }
            bool isValid = PLen && PUpper && PLower && PDigit;
            return isValid;
        }
        public static void insertDetails(string email, int customerid, string password)
        {
            string Epass = EncryptPlainTextToCipherText(password);
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            conn.Open();
            string command = "INSERT INTO UserLogins (CustomerId,email,password) VALUES(@CustomerId,@email,@password)";
            SqlCommand myCommand = new SqlCommand(command, conn);
            
            myCommand.Parameters.AddWithValue("@CustomerId", customerid);
            myCommand.Parameters.AddWithValue("@email", email);
            myCommand.Parameters.AddWithValue("@password", Epass);
            myCommand.ExecuteNonQuery();
            conn.Close();

            ViewAccountInfo(email);
        }
            public static void LoginTo()
            {
                Console.Clear();
                Console.WriteLine("-----Customer Login-----");
                Console.Write("Enter Email:");
                string enteredUser = Console.ReadLine();


                using SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf'; Integrated Security = True");
                connection.Open();
                string command = "SELECT email FROM UserLogins";

                SqlDataAdapter da = new SqlDataAdapter(command, connection);
                DataTable _email = new DataTable();
                da.Fill(_email);
                List<string> emailList = new List<string>();
                foreach (DataRow dr in _email.Rows)
                {
                    emailList.Add(dr[0].ToString());
                }
                connection.Close();
                bool E = emailList.Contains(enteredUser);
                if (!E)
                {
                    Console.WriteLine("invalid email");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    LoginTo();

                }
                else
                {
                    PassCheck(enteredUser);
                }
            }
            public static void PassCheck(string email)
            {
                Console.Write("Enter Password:");
                string enteredPass = Console.ReadLine();
                string encyrptedPass = EncryptPlainTextToCipherText(enteredPass);

                using SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf'; Integrated Security = True");
                connection.Open();
                string command = "SELECT password FROM UserLogins";

                SqlDataAdapter da = new SqlDataAdapter(command, connection);
                DataTable _password = new DataTable();
                da.Fill(_password);
                List<string> passList = new List<string>();
                foreach (DataRow dr in _password.Rows)
                {
                    passList.Add(dr[0].ToString());
                }
                connection.Close();
                bool E = passList.Contains(encyrptedPass);
                if (!E)
                {
                    Console.WriteLine("incorrect password");
                    PassCheck(email);



                }
                else
                {
                    Console.WriteLine("logging in...");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    ViewAccountInfo(email);
                    
                }

            }
        public static void ViewAccountInfo(string email)
        {
            Console.WriteLine("-My Account-");

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
            conn.Open();

            string command = "SELECT FirstName FROM Customers where Email = @email";
            SqlDataAdapter da = new SqlDataAdapter(command, conn);
            da.SelectCommand.Parameters.AddWithValue("email", email);
            DataTable _FirstName = new DataTable();
            da.Fill(_FirstName);
            List<string> FList = new List<string>();
            foreach (DataRow dr in _FirstName.Rows)
            {
                FList.Add(dr[0].ToString());
            }
            string firstname = (FList[0]);


            string command2 = "SELECT SecondName FROM Customers where Email = @email";
            SqlDataAdapter da2 = new SqlDataAdapter(command2, conn);
            da2.SelectCommand.Parameters.AddWithValue("email", email);
            DataTable _SecondName = new DataTable();
            da2.Fill(_SecondName);
            List<string> SList = new List<string>();
            foreach (DataRow dr in _SecondName.Rows)
            {
                SList.Add(dr[0].ToString());
            }
            string secondname = (SList[0]);

            

            string command3 = "SELECT CustomerId FROM Customers where Email = @email";
            SqlDataAdapter da3 = new SqlDataAdapter(command3, conn);
            da3.SelectCommand.Parameters.AddWithValue("email", email);
            DataTable _CustomerId = new DataTable();
            da3.Fill(_CustomerId);
            List<string> CList = new List<string>();
            foreach (DataRow dr in _CustomerId.Rows)
            {
                CList.Add(dr[0].ToString());
            }
            string customerid = (CList[0]);

            

            string command4 = "SELECT BookingId FROM Bookings where CustomerId = @cid";
            SqlDataAdapter da4 = new SqlDataAdapter(command4, conn);
            da4.SelectCommand.Parameters.AddWithValue("cid", customerid);
            DataTable _BookingId = new DataTable();
            da4.Fill(_BookingId);
            List<string> BList = new List<string>();
            foreach (DataRow dr in _BookingId.Rows)
            {
                BList.Add(dr[0].ToString());
            }
            string bookingid = (BList[0]);

            

            string command5 = "SELECT seatValue FROM Bookings where CustomerId = @cid";
            SqlDataAdapter da5 = new SqlDataAdapter(command5, conn);
            da5.SelectCommand.Parameters.AddWithValue("cid", customerid);
            DataTable _seatValue = new DataTable();
            da5.Fill(_seatValue);
            List<string> VList = new List<string>();
            foreach (DataRow dr in _seatValue.Rows)
            {
                VList.Add(dr[0].ToString());
            }
            string seatvalue = (VList[0]);

            string command6 = "SELECT Price FROM Bookings where CustomerId = @cid";
            SqlDataAdapter da6 = new SqlDataAdapter(command6, conn);
            da6.SelectCommand.Parameters.AddWithValue("cid", customerid);
            DataTable _Price = new DataTable();
            da6.Fill(_Price);
            List<string> PList = new List<string>();
            foreach (DataRow dr in _Price.Rows)
            {
                PList.Add(dr[0].ToString());
            }
            string price = (PList[0]);


            Console.WriteLine("Registered Email: {0}", email);
            Console.WriteLine("Account Holder: {0} {1}", firstname, secondname);
            Console.WriteLine("Customer Identification: {0}", customerid);
            Console.WriteLine("Booking ref {0} Costing £{1}", bookingid, price);

            Console.WriteLine("Would you like to delete a booking ? (Y) (N)");
            string deleteinput = Console.ReadLine().ToUpper();
            if (deleteinput == "Y")
            {
                Console.Write("enter booking ref of booking to cancel");
                int DB = int.Parse(Console.ReadLine());

                string sqlStatement = "DELETE FROM Bookings WHERE CustomerID = @BookingID";
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename= 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf' ;Integrated Security=True");
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@BookingID",DB);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine("deletion successful");
                Console.WriteLine("redirecting to homepage....");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                Program.Menu();
            }
            else
            {
                
                Console.WriteLine("press enter to go back to homepage");
                string input = Console.ReadLine();
                if (input == (""))
                {
                    Console.Clear();
                    Program.Menu();
                }
                
            }



        }

        private const string SecurityKey = "T0mlins0n_C0mpl@x_K£y";
        public static string EncryptPlainTextToCipherText(string PlainText)
        {
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
            //Gettting the bytes from the Security Key and Passing it
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            //Assigning the Security key 
            objTripleDESCryptoService.Key = securityKeyArray;
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            //adding extra byte if needed
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();
            //creating result and passing to array
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            objTripleDESCryptoService.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}
