using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
    public class AccommodationCreateDto
    {
        //public int Id { get; set; }

        public int PartnerId { get; set; }

        [Required]
        [StringLength(850)]
        public string Name { get; set; }

        public int RegionId { get; set; }

        public int TownId { get; set; }

        [Required]
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string IndustryEmail { get; set; }
    }
}