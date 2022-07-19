using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace ClassHelpers.Models.Domain
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required, Display(Name = "Mobile number")]
        public string MobileNumber { get; set; }

        [Required, Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [InverseProperty("ContactOwner")]
        public virtual ICollection<Contact> Contacts { get; set; } 

        [InverseProperty("Sender")]
        public virtual ICollection<Chat> SentChats { get; set; } 
        [InverseProperty("Receiver")]
        public virtual ICollection<Chat> ReceivedChats { get; set; } 

        [InverseProperty("GroupOwner")]
        public virtual ICollection<Group> OwnedGroups { get; set; } 
        [InverseProperty("Accounts")]
        public virtual ICollection<Group> Groups { get; set; } 

        [InverseProperty("GroupMessageSender")]
        public virtual ICollection<GroupMessage> GroupMessagesSent { get; set; }
    }
}
