using scheduler_v2.Models;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace scheduler_v2.Managers
{
    public class UserManager
    {
        public users CreateAccount(string username, string password, string email) // creates user account entity ready to be sent
        {
            string newSalt = CreateSalt(10);
            string hashedPassword = HashPassword(newSalt, password);
            var dateFormat = "yyyy-MM-dd HH:mm:ss";
            var stringDate = DateTime.Now.ToString(dateFormat);
            var convertedBack = DateTime.ParseExact(stringDate, dateFormat, CultureInfo.InvariantCulture);
            users user = new users()
            {
                username = username,
                password = hashedPassword,
                email = email,
                salt = newSalt,
                last_login = convertedBack,
                registration_date = convertedBack,
                enabled = 1,
                roles = "user"
            };
            return user;
        }

        private static string CreateSalt(int size) // Creates salt
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public string HashPassword(string salt, string password) // Concatenates salt and password
        {
            string mergedPass = string.Concat(salt, password);
            return Sha256(mergedPass);
        }

        private string Sha256(string randomString) // Hashes salted password
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckIfAccountNameExists(string accountNameInput) // Check if account name exists in DB
        {
            bool accountNameIsNotOk = true;
            using (var db = new scheduler_v2Entities())
            {
                var accName = (from s in db.users
                               where s.username == accountNameInput
                               select s.username).DefaultIfEmpty(String.Empty).First();

                if (accName.Equals(String.Empty))
                {
                    accountNameIsNotOk = false;
                };

                return accountNameIsNotOk;
            }
        }

        public bool CheckIfPasswordIsOk(string password) // Check if password is ok
        {
            bool passwordIsNotOk = true;
            if (password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit))
            {
                passwordIsNotOk = false;
            }
            return passwordIsNotOk;
        }

        public bool CheckIfEmailExists(string emailInput) // check if email exists in db
        {
            bool emailExists = false;
            using (var db = new scheduler_v2Entities())
            {
                var email = (from s in db.users
                             where s.email == emailInput
                             select s.email).DefaultIfEmpty(String.Empty).First().ToString();

                if (email != (String.Empty))
                {
                    emailExists = true; // returns if linq query returns an email from DB
                };

                return emailExists;
            }
        }

        public string GetPassword(string accountName) // Gets password from DB
        {
            using (var db = new scheduler_v2Entities())
            {
                var password = db.users
                              .Where(s => s.username == accountName)
                              .Select(s => s.password)
                              .FirstOrDefault()
                              .ToString();
                return password;
            }
        }

        public string GetUserSalt(string accountNameInput) // Gets salt from DB
        {
            string salt = null;
            using (var db = new scheduler_v2Entities())
            {
                try
                {
                    salt = db.users
                              .Where(s => s.username == accountNameInput)
                              .Select(s => s.salt)
                              .FirstOrDefault()
                              .ToString();
                }
                catch
                {
                    return salt;
                }

                return salt;
            }
        }

        public bool Login(string accountNameInput, string accountPasswordInput) // Login
        {
            string salt;
            string passwordInDb;
            string hashedPassword;
            bool loginOk = false;
            try
            {
                salt = GetUserSalt(accountNameInput); // Get salt from DB with UserAccountName
                if (salt is null)
                {
                    return loginOk = false;
                }
                passwordInDb = GetPassword(accountNameInput); // Gets hashed password from DB
                hashedPassword = HashPassword(salt, accountPasswordInput); // Concatenates password input and salt and runs algorithm
            }
            catch
            {
                return loginOk; // returns false bool if operations above fails
            }

            if (passwordInDb.Equals(hashedPassword)) // if hashed input password equals hashed password from database, LoginOk becomes true
            {
                loginOk = true;
            }

            return loginOk;
        }

        public string CreateToken(string accountName)
        {
            UserManager manager = new UserManager();

            string salt = manager.GetUserSalt(accountName);
            if (salt is null)
            {
                return salt;
            }
            string hashedToken = HashPassword(salt, accountName);
            return hashedToken;
        }

        public int GetAccountIdFromName(string accountName)
        {
            using (var db = new scheduler_v2Entities())
            {

                var userAccId = (from s in db.users
                                 where s.username == accountName
                                 select s.id).FirstOrDefault();

                return userAccId;
            }
        }
    }
}