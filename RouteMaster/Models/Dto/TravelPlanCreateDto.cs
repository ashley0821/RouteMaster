using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
    public class TravelPlanCreateDto
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int TravelDays { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}