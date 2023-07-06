using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class Comments_AttractionsEFRepository:IComments_AttractionsRepository
	{
		private AppDbContext _db = new AppDbContext();

		public IEnumerable<Comments_AttractionsIndexDto> Search(Comments_AttractionCriteria criteria)
		{
			var commAttr = _db.Comments_Attractions.Include(c => c.Attraction).Include(c => c.Member);

			if(string.IsNullOrEmpty(criteria.AttractionName) == false)
			{
				commAttr= commAttr.Where(c => c.Attraction.Name.Contains(criteria.AttractionName));
			}
			if (criteria.SortId != null)
			{
				switch (criteria.SortId)
				{
					case 0:
						commAttr = commAttr.OrderBy(c => c.Id);
						break;
					case 1:
						commAttr = commAttr.OrderByDescending(c => c.Score);
						break;
					case 2:
						commAttr = commAttr.OrderByDescending(c => c.StayHours);
						break;
					case 3:
						commAttr = commAttr.OrderByDescending(c => c.Price);
						break;
					case 4:
						commAttr = commAttr.OrderByDescending(c => c.CreateDate);
						break;
				}
			}


			return commAttr.AsNoTracking()
				.ToList()
				.Select(c => c.ToIndexDto());
		}
	}
}