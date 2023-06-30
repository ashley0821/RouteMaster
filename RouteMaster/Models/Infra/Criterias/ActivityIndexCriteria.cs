using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Criterias
{
    public class ActivityIndexCriteria
    {
        public string Name { get; set; }    
        public int? ActivityCategoryId { get; set; }
        public int? AttractionId { get; set; }
        public int? RegionId { get; set; }
        public bool ShowAvailableOnly { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

    }
}