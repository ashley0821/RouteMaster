using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using System.IO;
using System.Web.DynamicData.ModelProviders;

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
				.Include(p => p.AttractionImages)
				.Include(p => p.Region)
				.Include(p => p.Town)
				.Select(p => new AttractionIndexDto
				{
					Id = p.Id,
					Category = p.AttractionCategory.Name,
					Region = p.Region.Name,
					Town = p.Town.Name,
					Name = p.Name,
					Image = _db.AttractionImages
								.Where(i => i.AttractionId == p.Id)
								.Select(i => i.Image)
								.FirstOrDefault(),
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

		public void Create(AttractionCreateDto dto, HttpPostedFileBase[] files, String path)
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
			
			AttractionImage img = new AttractionImage();

			if (files.Length > 0 && files[0] != null)
			{
				foreach (HttpPostedFileBase file in files)
				{
					string fileName = SaveUploadedFile(path, file);
					img.Image = fileName;
					_db.AttractionImages.Add(img);
					_db.SaveChanges();
				}
			}

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
				.Include(p => p.AttractionImages)
				.Where(p => p.Id == id)
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
					Website = p.Website,
					Images = _db.AttractionImages
								.Where(i => i.AttractionId == p.Id)
								.Select(i => i.Image)
								.ToList(),
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

		public AttractionEditDto GetEditDto (int id)
		{
			return _db.Attractions
				.Where(p => p.Id == id)
				.Select(p => new AttractionEditDto
				{
					Id = p.Id,
					AttractionCategoryId = p.AttractionCategoryId,
					RegionId = p.RegionId,
					TownId = p.TownId,
					Name = p.Name,
					Address = p.Address,
					PositionX = p.PositionX,
					PositionY = p.PositionY,
					Description = p.Description,
					Website = p.Website,
				}).FirstOrDefault();
		}


		public void Edit(AttractionEditDto dto)
		{
			Attraction att = _db.Attractions.Find(dto.Id);
			att.AttractionCategoryId = dto.AttractionCategoryId;
			att.RegionId = dto.RegionId;
			att.TownId = dto.TownId;
			att.Name = dto.Name;
			att.Address = dto.Address;
			att.PositionX = dto.PositionX;
			att.PositionY = dto.PositionY;
			att.Description = dto.Description;
			att.Website = dto.Website;


			// 將它存到db
			_db.SaveChanges();
		}

		public void Delete(int id)
		{
			Attraction att = _db.Attractions.Find(id);

			try
			{
				_db.Attractions.Remove(att);
				// 將它存到db
				_db.SaveChanges();
			}
			catch(Exception ex)
			{
				throw new Exception("無法刪除", ex);
			}

		}

		private string SaveUploadedFile(string path, HttpPostedFileBase file1)
		{
			// 如果沒有上傳檔案或檔案是空的, 就不處理, 傳回 string.empty
			if (file1 == null || file1.ContentLength == 0) return string.Empty;

			// 取得上傳檔案的副檔名
			string ext = Path.GetExtension(file1.FileName); // ".jpg" 而不是"jpg"

			// 如果副檔名不在允許的範圍裡, 表示上傳不合理的檔案類型, 就不處理, 傳回 string.empty
			string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
			if (allowedExts.Contains(ext.ToLower()) == false) return string.Empty;

			// 生成一個不會重複的檔名
			string newFileName = Guid.NewGuid().ToString("N") + ext; // "N"格式不會產生 dash字串縮短
			string fullName = Path.Combine(path, newFileName);

			//將上傳檔案存放到指定位置
			file1.SaveAs(fullName);

			//傳回存放的檔名
			return newFileName;
		}
	}
}