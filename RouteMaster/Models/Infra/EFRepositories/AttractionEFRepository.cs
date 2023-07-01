using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class AttractionEFRepository : IAttractionRepository
	{
		private AppDbContext _db;

		public AttractionEFRepository()
		{
			_db = new AppDbContext();

		}
		public IEnumerable<AttractionIndexDto> Search()
		{
			return _db.Attractions
				.AsNoTracking() // 告訴程式不用追蹤改動 以增加效率
				.Include(p => p.AttractionCategory) // 避免N+1 Query
				.Include(p=> p.Region)
				.Include(p => p.Town)
				.Select(p => new AttractionIndexDto
				{
					Id = p.Id,
					Category = p.AttractionCategory.Name,
					Region = p.Region.Name,
					Town = p.Town.Name,
					Name = p.Name,
					Description = p.Description,
					AverageScore = _db.Comments_Attractions
								.Where(c => c.AttractionId == p.Id)
								.Select(c => c.Score)
								.Average(),
					AverageStayHours = (double)_db.Comments_Attractions
								.Where(c => c.AttractionId == p.Id)
								.Select(c => c.StayHours)
								.Average(),
					AveragePrice = (int)_db.Comments_Attractions
								.Where(c => c.AttractionId == p.Id)
								.Select(c => c.Price)
								.Average(),
				});
		}
	}
}