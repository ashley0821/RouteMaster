using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;

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
				.Include(p => p.Region)
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

		public void Create(AttractionCreateDto dto)
		{
			// 將 RegisterDto 轉成 Member
			Attraction att = new Attraction
			{
				AttractionCategoryId = dto.AttractionCategoryId,
				RegionId = dto.RegionId,
				TownId = dto.TownId,
				Name = dto.Name,
				Address = dto.Address,
				PositionX = dto.PositionX,
				PositionY = dto.PositionY,
				Description = dto.Description,
				Website = dto.Website,
			};

			// 將它存到db
			_db.Attractions.Add(att);
			_db.SaveChanges();
		}

		public bool ExistAttraction(string Name)
		{
			return _db.Attractions.Any(m => m.Name == Name);
		}

		public AttractionDetailDto Get(int id)
		{
			return _db.Attractions
				.AsNoTracking() // 告訴程式不用追蹤改動 以增加效率
				.Include(p => p.AttractionCategory) // 避免N+1 Query
				.Include(p => p.Region)
				.Include(p => p.Town)
				.Where(p=>p.Id == id)
				.Select(p => new AttractionDetailDto
				{
					Id = p.Id,
					Category = p.AttractionCategory.Name,
					Region = p.Region.Name,
					Town = p.Town.Name,
					Name = p.Name,
					Address = p.Address,
					PositionX = p.PositionX,
					PositionY = p.PositionY,
					Description = p.Description,
					Website= p.Website,
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
				}).FirstOrDefault();
		}
	}
}