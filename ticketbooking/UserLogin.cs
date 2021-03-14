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
            Console.WriteLine("enter password:");
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
