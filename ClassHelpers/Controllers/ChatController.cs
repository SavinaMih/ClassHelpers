using ClassHelpers.Repositories;
using Microsoft.AspNetCore.Authorization;
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
            List<string> accounts = accountRepository.GetAllAccounts().Select(a => a.UserName).ToList();
            return View(accounts);
        }

        public IActionResult Message(string user)
        {
            return View("Message", user);
        }
    }
}
