using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClassHelpers.Models.Domain;
using ClassHelpers.Models.InputModels;

namespace ClassHelpers.Models.ViewModels
{
    public class ChatModel
    {
           
        [Display(Name = "Contact")]
        public string ContactName { get; set; }
        public IEnumerable<ChatMessage> MessageList { get; set; }


       
        [Required(ErrorMessage = "Please enter a message before sending it")]
        [DataType(DataType.Text)]
        public string MessageInput { get; set; }

        public int ChatId { get; set; } 

        public Message message { get; set; }
    }
}
