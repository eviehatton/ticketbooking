using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace ticketbooking
{
    public class AdminLogin
    {
        public static void UserLoginA()
        {
            //gathering login details 
            Console.Clear();
            Console.WriteLine("-----Admin Login-----");
            Console.Write("Enter Username:");
            string enteredUser = Console.ReadLine();

            //establishing connection to database
            using SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf'; Integrated Security = True");
            connection.Open();
            string command = "SELECT Username FROM AdminLogins";

            //getting the username from the database and comparing it to user input
            SqlDataAdapter da = new SqlDataAdapter(command, connection);
            //creating a datatable to insert relevant data
            DataTable _Username = new DataTable();
            da.Fill(_Username);
            List<string> userList = new List<string>();
            foreach (DataRow dr in _Username.Rows)
            {
                userList.Add(dr[0].ToString());
            }
            //adding usernames to list so specifics can be retrieved
            connection.Close();
            //checking the username is valid 
            bool E = userList.Contains(enteredUser);
            if (!E)
            {
                Console.WriteLine("invalid username");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                UserLoginA();
                //back to login page if false to try again
            }
            else
            {
                //passing the username to the password checker
                PassCheck(enteredUser);
            }
        }
        public static void PassCheck(string username)
        {
            Console.Write("Enter Password:");
            string enteredPass = Console.ReadLine();
            string encyrptedPass = EncryptPlainTextToCipherText(enteredPass);
            //getting user input and passing it through encrytption

            using SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf'; Integrated Security = True");
            connection.Open();
            string command = "SELECT Password FROM AdminLogins";
            //opening connection to database and getting the encrypted passwords
            SqlDataAdapter da = new SqlDataAdapter(command, connection);
            DataTable _Password = new DataTable();
            da.Fill(_Password);
            List<string> passList = new List<string>();
            foreach (DataRow dr in _Password.Rows)
            {
                passList.Add(dr[0].ToString());
            }
            connection.Close();
            //checking the encypted input against database
            bool E = passList.Contains(encyrptedPass);
            if (!E)
            {
                Console.WriteLine("incorrect password");
                PassCheck(username);
                //if false redirected to password check again
                
                

            }
            else
            {
                Console.WriteLine("logging in...");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                AdminView.AdminMenu();
                //redirec to admin method if all infomation matched
            }

        }
        private const string SecurityKey = "Sty1les_C0mpl@x_K£y";
        //encrytpting the passwords to protect them
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
