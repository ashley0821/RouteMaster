using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
    public class AttractionListIndexVM
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Region { get; set; }

        public string Town { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double? PositionX { get; set; }

        public double? PositionY { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public double? AverageScore { get; set; }

        public double? AverageStayHours { get; set; }

        public double? AveragePrice { get; set; }
    }
}