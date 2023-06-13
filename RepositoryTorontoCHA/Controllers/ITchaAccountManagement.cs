using TorontoCHA.Entity;

namespace TorontoCHA.Repository.Controllers

{
    public interface ITchaAccountManagement
    {
        public int CreateTchaAccount(TchaAccount tchaAccount);

        public TchaAccount LoginTchaAccount(string username, string password);

        public List<TchaAccount> CheckEmailUsernameTchaAccount(string username, string email);

        public int DeleteTchaAccount(int accountId);

        public TchaAccount GetAccountById(int accountId);

        public int UpdateAccount(TchaAccount tchaAccount);

        //public List<TchaAccount> SearchTchaAccount(string username, string password);
    }
}
