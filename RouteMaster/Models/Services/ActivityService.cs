using RouteMaster.Models.Dto;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class ActivityService
	{
		private IActivityRepository _repo;

        public ActivityService(IActivityRepository repo)
        {
            _repo = repo;            
        }

        public IEnumerable<ActivityIndexDto> Search()
        {
            return _repo.Search();
        }
    }
}