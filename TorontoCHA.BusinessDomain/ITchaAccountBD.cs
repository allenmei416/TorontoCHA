using TorontoCHA.Entity;

namespace TorontoCHA.BusinessDomain
{
    public interface ITchaAccountBD
    {
        public int CreateTchaAccount(TchaAccount tchaAccount);

        public TchaAccount LoginTchaAccount(string username, string password);

        //public bool CheckEmailUsernameTchaAccount(string username, string email);

        public int DeleteTchaAccount(int accountId);

        public TchaAccount GetAccountById(int accountId);

        public int UpdateAccount(TchaAccount tchaAccount);
    }
}
