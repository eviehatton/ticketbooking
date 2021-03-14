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
            Console.Clear();
            Console.WriteLine("-----Admin Login-----");
            Console.Write("Enter Username:");
            string enteredUser = Console.ReadLine();


            using SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf'; Integrated Security = True");
            connection.Open();
            string command = "SELECT Username FROM AdminLogins";

            SqlDataAdapter da = new SqlDataAdapter(command, connection);
            DataTable _Username = new DataTable();
            da.Fill(_Username);
            List<string> userList = new List<string>();
            foreach (DataRow dr in _Username.Rows)
            {
                userList.Add(dr[0].ToString());
            }
            connection.Close();
            bool E = userList.Contains(enteredUser);
            if (!E)
            {
                Console.WriteLine("invalid username");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                UserLoginA();

            }
            else
            {
                PassCheck(enteredUser);
            }
        }
        public static void PassCheck(string username)
        {
            Console.Write("Enter Password:");
            string enteredPass = Console.ReadLine();
            string encyrptedPass = EncryptPlainTextToCipherText(enteredPass);

            using SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\backdoor\source\repos\ticketbooking\ticketbooking\Database1.mdf'; Integrated Security = True");
            connection.Open();
            string command = "SELECT Password FROM AdminLogins";

            SqlDataAdapter da = new SqlDataAdapter(command, connection);
            DataTable _Password = new DataTable();
            da.Fill(_Password);
            List<string> passList = new List<string>();
            foreach (DataRow dr in _Password.Rows)
            {
                passList.Add(dr[0].ToString());
            }
            connection.Close();
            bool E = passList.Contains(encyrptedPass);
            if (!E)
            {
                Console.WriteLine("incorrect password");
                PassCheck(username);
                
                

            }
            else
            {
                Console.WriteLine("logging in...");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                AdminView.AdminMenu();
            }

        }
        private const string SecurityKey = "Sty1les_C0mpl@x_K£y";
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
        static void ViewDataA()
        {

        }
    }
}
