using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
    public class MemberActiveVM
    {
        public int Id { get; set; }

        public string Account { get; set; }

        [Display(Name = "名")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        public string Email { get; set; }   

        public bool IsConfirmed { get; set; }

        public string ConfirmCode { get; set; }
    }
}