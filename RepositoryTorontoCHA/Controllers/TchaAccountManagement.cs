using Dapper;
using System.Data;
using System.Data.SqlClient;
using TorontoCHA.Entity;

namespace TorontoCHA.Repository.Controllers
{
    public class TchaAccountManagement : ITchaAccountManagement
    {
        private string _reloaccessConnetionString;

        public TchaAccountManagement(IConfiguration configuration)
        {
            _reloaccessConnetionString = configuration.GetConnectionString("DBTorontoCHA");
        }
        public int CreateTchaAccount(TchaAccount tchaAccount)
        {
            var param = new DynamicParameters();
            param.Add("@Username", tchaAccount.Username);
            param.Add("@Password", tchaAccount.Password);
            param.Add("@Email", tchaAccount.Email);
            param.Add("@FirstName", tchaAccount.FirstName);
            param.Add("@LastName", tchaAccount.LastName);
            param.Add("@PhoneNumber", tchaAccount.PhoneNumber);
            using (SqlConnection connection = new SqlConnection(_reloaccessConnetionString))
            {
                int result = connection.Execute("CreateTchaAccount", param, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }
        public TchaAccount LoginTchaAccount(string username, string password)
        {
            var param = new DynamicParameters();
            param.Add("@Username", username);
            param.Add("@Password", password);
            using (SqlConnection connection = new SqlConnection(_reloaccessConnetionString))
            {
                var result = connection.Query<TchaAccount>("LoginTchaAccount", param, commandType: CommandType.StoredProcedure).ToList();
                if (result.Count > 0)
                    return result.FirstOrDefault();
                else
                    return null;
            }
        }

        public TchaAccount GetAccountById(int accountId)
        {
            var param = new DynamicParameters();
            param.Add("@AccountId", accountId);

            using (SqlConnection connection = new SqlConnection(_reloaccessConnetionString))
            {
                var result = connection.Query<TchaAccount>("GetAccountById", param, commandType: CommandType.StoredProcedure).ToList();
                if (result.Count > 0)
                    return result.FirstOrDefault();
                else
                    return null;
            }
        }

        public List<TchaAccount> CheckEmailUsernameTchaAccount(string username, string email)
        {
            var param = new DynamicParameters();
            param.Add("@Username", username);
            param.Add("@Email", email);
            using (SqlConnection connection = new SqlConnection(_reloaccessConnetionString))
            {
                var result = connection.Query<TchaAccount>("CheckEmailUsernameTchaAccount", param, commandType: CommandType.StoredProcedure).ToList();
                return result;
                
            }
        }

        public int DeleteTchaAccount(int accountId)
        {
            var param = new DynamicParameters();
            param.Add("@AccountId", accountId);
            using (SqlConnection connection = new SqlConnection(_reloaccessConnetionString))
            {
                int result = connection.Execute("DeleteTchaAccount", param, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }

        public int UpdateAccount(TchaAccount tchaAccount)
        {
            var param = new DynamicParameters();
            param.Add("@AccountId", tchaAccount.AccountId);
            param.Add("@Username", tchaAccount.Username);
            param.Add("@Password", tchaAccount.Password);
            param.Add("@Email", tchaAccount.Email);
            param.Add("@FirstName", tchaAccount.FirstName);
            param.Add("@LastName", tchaAccount.LastName);
            param.Add("@PhoneNumber", tchaAccount.PhoneNumber);
            using (SqlConnection connection = new SqlConnection(_reloaccessConnetionString))
            {
                int result = connection.Execute("UpdateAccount", param, null, 0, CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
