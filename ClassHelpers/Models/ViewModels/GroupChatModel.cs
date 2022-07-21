using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClassHelpers.Models.Domain;
using ClassHelpers.Models.InputModels;

namespace ClassHelpers.Models.ViewModels
{
    public class GroupChatModel
    {
        public int GroupId { get; set; } 

        [Display(Name = "Group name")]
        public string GroupName { get; set; }
        public IEnumerable<GroupMessageModel> GroupMessages { get; set; }
        public string GroupOwner { get; set; }
        public List<string> Participants { get; set; }

        
        [Required(ErrorMessage = "Please enter a message before sending it")]
        [DataType(DataType.Text)]
        public string MessageInput { get; set; }


    }
}
