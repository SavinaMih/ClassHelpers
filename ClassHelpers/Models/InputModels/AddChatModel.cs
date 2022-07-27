
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassHelpers.Models.Domain;

namespace ClassHelpers.Models.InputModels
{
    public class AddChatModel
    {
        public int? SelectedContact { get; set; } 
        public IEnumerable<Contact> Contacts { get; set; }

        [Required(ErrorMessage = "Please enter a message before sending it")]
        [DataType(DataType.Text)]
        public string InitialMessage { get; set; }

    }
}
