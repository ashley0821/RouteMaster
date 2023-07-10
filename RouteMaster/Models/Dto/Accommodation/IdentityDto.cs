using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto.Accommodation
{
	public class IdentityDto
	{
        public int Id { get; set; }
        public string Permission { get; set; }
        public IdentityDto(int id, string permission)
        {
            this.Id = id;
            this.Permission = permission;
        }
    }
}