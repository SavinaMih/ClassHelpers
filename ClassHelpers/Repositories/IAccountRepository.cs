using ClassHelpers.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace ClassHelpers.Repositories
{
    public interface IAccountRepository
    {
        IdentityUser GetAccountByMail(string email);
        IdentityUser GetAccountByID(string accountId);
        IdentityUser GetAccountByNumber(string mobileNumber);
        bool CheckIfAccountExists(string email);
        IEnumerable<IdentityUser> GetAllAccounts();
    }
}
