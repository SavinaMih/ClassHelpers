
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassHelpers.Models.Domain
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        public int? OwnerAccountId { get; set; }
        public int ContactOwnerId { get; set; }

        [Required(ErrorMessage = "Please enter a contactname"), Display(Name = "Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Please enter a mobile number"), Display(Name = "Mobile number")]
        public string MobileNumber { get; set; }


        [ForeignKey("ContactOwnerId")]
        public virtual Account ContactOwner { get; set; }

        [ForeignKey("OwnerAccountId")]
        public virtual Account Account { get; set; }
    }
}