using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Criterias
{
    public class MemberCriteria
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string CellPhoneNumber { get; set; }

        public DateTime? CreateDateBegin { get; set; }

        public DateTime? CreateDateEnd { get; set; }

        public string IsSuspended { get; set; }

    }
}