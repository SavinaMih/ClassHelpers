using ClassHelpers.Data;
using ClassHelpers.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace ClassHelpers.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ClassHelpersContext db;

        public AccountRepository(ClassHelpersContext db)
        {
            this.db = db;
        }

        public IdentityUser GetAccountByID(string accountId)
        {
           IdentityUser account = db.Users.SingleOrDefault(x => x.Id == accountId);
           return account;
        }

        public IdentityUser GetAccountByMail(string email)
        {
            IdentityUser account = db.Users.SingleOrDefault(x => x.Email == email);
            return account;
        }

        public bool CheckIfAccountExists(string email)
        {
            bool check = db.Users.Any(x => x.Email == email);
            return check;
        }

        public IEnumerable<IdentityUser> GetAllAccounts()
        {
            IEnumerable<IdentityUser> accounts = db.Users;
            return accounts;
        }

        public IdentityUser GetAccountByNumber(string mobileNumber)
        {
            IdentityUser account = db.Users.SingleOrDefault(x => x.PhoneNumber == mobileNumber);
            return account;
        }
    }
}
