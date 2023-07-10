using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Criterias
{
    public class PartnerCriteria
    {
        
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string Email { get; set; }

        public DateTime? CreateDateBegin { get; set; }

        public DateTime? CreateDateEnd { get; set; }

        public bool IsConfirmed { get; set; }

        public bool? IsSuspended { get; set; }

    }
}