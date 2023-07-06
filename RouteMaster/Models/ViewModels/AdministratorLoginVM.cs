using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
    public class AdministratorLoginVM
    {
        //用firstname當帳號
        [Display(Name ="帳號")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name ="密碼")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}