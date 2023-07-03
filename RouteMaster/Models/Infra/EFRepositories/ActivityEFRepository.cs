using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RouteMaster.Models.Infra.Criterias;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class ActivityEFRepository:IActivityRepository
	{
		private AppDbContext _db;

        public ActivityEFRepository()
        {
            _db = new AppDbContext();
        }


        public IEnumerable<ActivityIndexDto> Search(ActivityIndexCriteria criteria)
        {
			//todo search 篩選條件設計
			var query = _db.Activities
				.Include(a => a.ActivityCategory)
				.Include(a => a.Attraction)
				.Include(a => a.Region);

			#region where
			if (string.IsNullOrEmpty(criteria.Name) == false)
			{
				query=query.Where(a=>a.Name.Contains(criteria.Name));
			}
			if (criteria.ActivityCategoryId != null && criteria.ActivityCategoryId.Value > 0)
			{
				query = query.Where(a=>a.ActivityCategoryId == criteria.ActivityCategoryId.Value);
			}

			if(criteria.AttractionId!=null&&criteria.AttractionId.Value > 0)
			{
				query=query.Where(a=>a.AttractionId== criteria.AttractionId.Value);
			}
			if(criteria.RegionId!=null&&criteria.RegionId.Value > 0)
			{
				query = query.Where(a => a.RegionId == criteria.RegionId.Value);
			}
			if (criteria.MinPrice.HasValue)
			{
				query = query.Where(a => a.Price >= criteria.MinPrice.Value);
			}
			if (criteria.MaxPrice.HasValue)
			{
				query=query.Where(a=>a.Price<=criteria.MaxPrice.Value);
			}
			if(criteria.StartDate.HasValue)
			{
				query=query.Where(a=>a.StartTime >= criteria.StartDate.Value);
			}
			if(criteria.EndDate.HasValue)
			{
				query=query.Where(a=>a.EndTime<=criteria.EndDate.Value);
			}
			if(criteria.ShowAvailableOnly==true)
			{
				query=query.Where(a=>a.Status==true);
			}
			#endregion


			var activities = query
				.OrderBy(x => x.RegionId)
				.ThenBy(x => x.AttractionId)
				.ThenBy(x => x.ActivityCategoryId)
				.ToList()
				.Select(a => a.ToIndexDto());

			return activities;
        }




	



        public void Create(ActivityCreateDto dto)
		{
			Activity activity= dto.ToEntity();
			_db.Activities.Add(activity);
			_db.SaveChanges();
		}

		public void Delete(int id)
		{
			var activity = _db.Activities.FirstOrDefault(x=>x.Id==id);
			_db.Activities.Remove(activity);
			_db.SaveChanges();
		}

		public void Edit(ActivityEditDto dto)
		{
			var activityInDb=_db.Activities.FirstOrDefault(x => x.Id == dto.Id);

			activityInDb.ActivityCategoryId = dto.ActivityCategoryId;
			activityInDb.AttractionId = dto.AttractionId;
			activityInDb.Name=dto.Name;
			activityInDb.RegionId = dto.RegionId;			
			activityInDb.Price=dto.Price;
			activityInDb.StartTime=dto.StartTime;
			activityInDb.EndTime=dto.EndTime;
			activityInDb.Description = dto.Description;
			activityInDb.Status=dto.Status;

			_db.SaveChanges();
			
			
		}

		public bool ExistAcativity(string activityName, int attractionId, DateTime startTime, DateTime endTime)
		{
			return _db.Activities.
			Any(a => a.Name == activityName 
				&& a.AttractionId == attractionId 
				&& a.StartTime == startTime 
				&& a.EndTime == endTime);
		}

        public Activity GetActivityById(int id)
        {
			Activity activityInDb= _db.Activities.FirstOrDefault(x=>x.Id==id);
			return activityInDb;
        }
    }
}