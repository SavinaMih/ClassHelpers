
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassHelpers.Models.Domain
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }

        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public virtual Account Sender { get; set; }

        [ForeignKey("Receiver")] 
        public int ReceiverId { get; set; }
        public virtual Account Receiver { get; set; }

        public virtual List<Message> Messages { get; set; }
        
    }
}
