using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClassHelpers.Models.Domain;

namespace ClassHelpers.Models.ViewModels
{
    public class ChatMessage
    {
        public Message Message { get; set; }

        [Display(Name = "Name")]
        public string OtherAccountContactName { get; set; } 

        public string LastSender { get; set; } 

        public bool RemoveChatAvailable { get; set; } 
    }
}
