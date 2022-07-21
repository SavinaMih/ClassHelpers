using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassHelpers.Models.Domain;
using ClassHelpers.Models.InputModels;
using ClassHelpers.Repositories;

namespace ClassHelpers.Models.BusinessLogic
{
    public class ContactLogic
    {
        IContactRepository contactRepository = new ContactRepository();
        IAccountRepository accountRepository = new AccountRepository();

        public void CreateContact(string mobileNumber, string contactName, int accountId)
        {
            Contact contact = new Contact();
            contact.MobileNumber = mobileNumber;
            contact.ContactName = contactName;
            contact.ContactOwnerId = accountId;

            Account contactAccount = accountRepository.GetAccount(mobileNumber);
            if (contactAccount != null)
            {
                contact.OwnerAccountId = contactAccount.AccountId;
            }

            contactRepository.AddContact(contact);
        }
    }
}
