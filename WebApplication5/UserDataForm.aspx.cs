using System;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace WebApplication5
{
    public partial class UserDataForm : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            NameValueCollection formData = Request.Form;

            // Server-side validation
            if (UserInputValidator.ValidateAllData(formData))
                return;

            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BRUCE LEE\source\repos\WebApplication5\WebApplication5\App_Data\UserDatabase.mdf;Integrated Security=True");

            var insertCommand = "INSERT INTO person(first_name, last_name, phone_number, email_address)" +
                    "VALUES(@firstName, @lastName, @phoneNumber, @emailAddress)";

            var readCommand = "SELECT first_name AS Firstname, last_name AS Lastname, phone_number AS Phone, email_address AS Email FROM person";
          
            using (sqlConnection)
            {
                var sqlCommand = new SqlCommand(insertCommand, sqlConnection);
                
                // read data from HTML Form
                sqlCommand.Parameters.AddWithValue("@firstName", formData["fname"]);
                sqlCommand.Parameters.AddWithValue("@lastName", formData["lname"]);
                sqlCommand.Parameters.AddWithValue("@phoneNumber", formData["phone"]);
                sqlCommand.Parameters.AddWithValue("@emailAddress", formData["email"]);

                sqlConnection.Open();
                var numOfRowsInserted = sqlCommand.ExecuteNonQuery();
   
                if (numOfRowsInserted != 1)
                    return;

                // Display updated list if a new contact was successfully saved
                sqlCommand = new SqlCommand(readCommand, sqlConnection);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    sqlDataReader.Read();

                    GridView1.DataSource = sqlDataReader;
                    GridView1.DataBind();
                }
            }
        }
    }

    public enum RegexPattern
    {
        None,
        Phone,
        Email
    }


    public class UserInputValidator
    {
        private static bool ValidateText(string text, string regexPattern)
        {
            var regex = new Regex(regexPattern);

            if (regex.IsMatch(text))
                return true;
            else
                return false;
        }

        private static bool ValidateUserInput(string text, RegexPattern regexPattern)
        {
            const string PhonePattern = @"(0047|\\+47|47)?\\d{8}";
            const string EmailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            if (string.IsNullOrWhiteSpace(text))
                return false;

            switch (regexPattern)
            {
                case RegexPattern.Phone:
                    if (!ValidateText(text, PhonePattern))
                        return false;
                    break;
                case RegexPattern.Email:
                    if (!ValidateText(text, EmailPattern))
                        return false;
                    break;
                case RegexPattern.None:
                default:
                    break;
            }

            return true;
        }

        public static bool ValidateAllData(NameValueCollection nvc)
        {
            var regexPattern = RegexPattern.None;
            foreach(var key in nvc.AllKeys)
            {
                if (key == "phone")
                    regexPattern = RegexPattern.Phone;

                if (key == "email")
                    regexPattern = RegexPattern.Email;

                if (!ValidateUserInput(nvc[key], regexPattern))
                    return false;
            }

            /* ADD MORE SERVER-SIDE VALIDATION*/

            return true;
        }
    }
}