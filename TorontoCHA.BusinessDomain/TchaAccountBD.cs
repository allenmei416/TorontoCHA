using TorontoCHA.Entity;
using TorontoCHA.Repository;
using TorontoCHA.Repository.Controllers;
using System;
using System.Text.RegularExpressions;


namespace TorontoCHA.BusinessDomain
{
    public class TchaAccountBD : ITchaAccountBD
    {
        private readonly ITchaAccountManagement _tchaAccountManagement;
        
        public TchaAccountBD(ITchaAccountManagement tchaAccountManagement)
        {
            _tchaAccountManagement = tchaAccountManagement;
        }
        public int CreateTchaAccount(TchaAccount tchaAccount)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            if (tchaAccount.FirstName == null || tchaAccount.LastName == null || tchaAccount.Email == null || tchaAccount.Username == null || tchaAccount.PhoneNumber == null || tchaAccount.Password == null)
            {
                throw new Exception("Please fill out all out the fields.");
            }
            if (tchaAccount.FirstName.Length == 0 || tchaAccount.LastName.Length == 0 || tchaAccount.Email.Length == 0 || tchaAccount.Username.Length == 0 || tchaAccount.PhoneNumber.Length == 0)
            {
                throw new Exception("Please fill out all out the fields.");
            }

            if (tchaAccount.Username.Contains(" "))
            {
                throw new Exception("Usernames cannot contain a space");
            }

            if (tchaAccount.Password.Contains(" "))
            {
                throw new Exception("Passwords cannot contain a space");
            }

            if (tchaAccount.Password.Length < 8)
            {
                throw new Exception("Password must be 8 characters long.");
            }

            List<TchaAccount> commonEmailUser = _tchaAccountManagement.CheckEmailUsernameTchaAccount(tchaAccount.Username, tchaAccount.Email);
            if (commonEmailUser.Count > 0)
            {
                throw new Exception("An account already exists with the same username or email.");
            }
            if (tchaAccount.Email.Contains(" "))
            {
                throw new Exception("Emails cannot contain a space");
            }
            if (!Regex.IsMatch(tchaAccount.Email, pattern))
            {
                throw new Exception("Email is not valid.");
            }

            foreach (char c in tchaAccount.PhoneNumber)
            {
                if (!Char.IsDigit(c))
                {
                    throw new Exception("The phone number provided is not valid.");
                }
            }
            if (tchaAccount.PhoneNumber.Length != 10)
            {
                throw new Exception("The phone number provided is not valid.");
            }

            return _tchaAccountManagement.CreateTchaAccount(tchaAccount);

        }

        public TchaAccount LoginTchaAccount(string username, string password)
        {
            TchaAccount Result = _tchaAccountManagement.LoginTchaAccount(username, password);
            return Result;
        }

        public TchaAccount GetAccountById(int accountId)
        {
            TchaAccount Result = _tchaAccountManagement.GetAccountById(accountId);
            return Result;
        }


        //public bool CheckEmailUsernameTchaAccount(string username, string email)
        //{
        //    List<TchaAccount> Result = _tchaAccountManagement.CheckEmailUsernameTchaAccount(username, email);
        //    if (Result.Count > 0)
        //        return true;
        //    return false;
        //}

        public int DeleteTchaAccount(int accountId)
        {
            int Result = _tchaAccountManagement.DeleteTchaAccount(accountId);
            return Result;
        }

        public int UpdateAccount(TchaAccount tchaAccount)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            if (tchaAccount.FirstName == null || tchaAccount.LastName == null || tchaAccount.Email == null || tchaAccount.Username == null || tchaAccount.PhoneNumber == null || tchaAccount.Password == null)
            {
                throw new Exception("Please fill out all out the fields.");
            }
            if (tchaAccount.FirstName.Length == 0 || tchaAccount.LastName.Length == 0 || tchaAccount.Email.Length == 0 || tchaAccount.Username.Length == 0 || tchaAccount.PhoneNumber.Length == 0)
            {
                throw new Exception("Please fill out all out the fields.");
            }

            if (tchaAccount.Username.Contains(" "))
            {
                throw new Exception("Usernames cannot contain a space");
            }

            if (tchaAccount.Password.Contains(" "))
            {
                throw new Exception("Passwords cannot contain a space");
            }

            if (tchaAccount.Password.Length < 8)
            {
                throw new Exception("Password must be 8 characters long.");
            }

            List<TchaAccount> commonEmailUser = _tchaAccountManagement.CheckEmailUsernameTchaAccount(tchaAccount.Username, tchaAccount.Email);
            if (commonEmailUser.Count > 0)
            {
                throw new Exception("An account already exists with the same username or email.");
            }
            if (tchaAccount.Email.Contains(" "))
            {
                throw new Exception("Emails cannot contain a space");
            }
            if (!Regex.IsMatch(tchaAccount.Email, pattern))
            {
                throw new Exception("Email is not valid.");
            }

            foreach (char c in tchaAccount.PhoneNumber)
            {
                if (!Char.IsDigit(c))
                {
                    throw new Exception("The phone number provided is not valid.");
                }
            }
            if (tchaAccount.PhoneNumber.Length != 10)
            {
                throw new Exception("The phone number provided is not valid.");
            }

            return _tchaAccountManagement.UpdateAccount(tchaAccount);

        }
    }
}
