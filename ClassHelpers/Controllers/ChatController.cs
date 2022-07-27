using ClassHelpers.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClassHelpers.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IAccountRepository accountRepository;

        public ChatController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chatroom()
        {
            return View();
        }

        public IActionResult PrivateMessaging()
        {
            List<IdentityUser> accounts = accountRepository.GetAllAccounts().ToList();
            return View(accounts);
        }

        public IActionResult Message(string user)
        {
            List<IdentityUser> accounts = accountRepository.GetAllAccounts().ToList();
            KeyValuePair<List<IdentityUser>, string> model = new KeyValuePair<List<IdentityUser>, string>(accounts, user);

            return View("Message", model);
        }
    }
}
