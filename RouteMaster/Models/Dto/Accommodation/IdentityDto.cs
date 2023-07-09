using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto.Accommodation
{
	public class IdentityDto
	{
        public string Email { get; set; }
        public string Permission { get; set; }
        public IdentityDto(string email, string permission)
        {
            this.Email = email;
            this.Permission = permission;
        }
    }
}