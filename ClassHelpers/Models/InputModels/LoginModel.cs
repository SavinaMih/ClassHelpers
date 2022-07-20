    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    namespace ClassHelpers.Models.InputModels
    {
    public class LoginModel
        {
            [Required(ErrorMessage = "Mobile number required")]
            [DataType(DataType.PhoneNumber)]
            public string MobileNumber { get; set; }

            [Required(ErrorMessage = "Password required")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }

