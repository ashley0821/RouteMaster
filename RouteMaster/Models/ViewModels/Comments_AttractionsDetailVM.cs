using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class Comments_AttractionsDetailVM
	{
        public int Id { get; set; }
        public string AttractioName { get; set; }   
        public string Content { get; set; }
        public int Score { get; set; }
        public IEnumerable<string> Images { get; set; }    
    }

}