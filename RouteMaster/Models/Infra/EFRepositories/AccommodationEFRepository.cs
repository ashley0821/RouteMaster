using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using RouteMaster.Models.Infra.Extensions;
using System.Security.Principal;
using System.Data;
using System.IO;
using RouteMaster.Models.ViewModels.Accommodations;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class AccommodationEFRepository : IAccommodationRepository
	{
		private readonly AppDbContext _db = new AppDbContext();

		public void Create(AccommodationCreateDto dto)
		{
			Accommodation entity = dto.ToIndexEntity();

            _db.Accommodations.Add(entity);
            _db.SaveChanges();
        }

		public void EditAccommodationProfile(AccommodationEditDto dto)
		{
			Accommodation entity = _db.Accommodations.FirstOrDefault(a => a.Id == dto.Id);

			//_db.Entry(entity).CurrentValues.SetValues(dto.ToEditEntity());
			string address = dto.GetFullAddress();

			entity.Name = dto.Name;
			entity.Description = dto.Description;
			entity.RegionId = dto.RegionId;
			entity.TownId = dto.TownId;
			entity.Address = address;
			entity.PhoneNumber = dto.PhoneNumber;
			entity.Website = dto.Website;
			entity.IndustryEmail = dto.IndustryEmail;
			entity.ParkingSpace = dto.ParkingSpace;

			_db.SaveChanges();
		}


		public AccommodationEditDto GetEditInfo(int? id)
		{
			var accommodationdb = _db.Accommodations.AsNoTracking().FirstOrDefault(x => x.Id == id);

			return accommodationdb == null ? null : accommodationdb.ToEditDto();
			;
		}

		public bool ExistName(string name)
        {
            return _db.Accommodations.Any(m => m.Name == name);

        }
		public bool IsOriginalName(AccommodationEditDto dto)
		{
			return _db.Accommodations.FirstOrDefault(a=>a.Id == dto.Id).Name == dto.Name;
		}
		

		public IEnumerable<AccommodationIndexDto> Search()
		{

			var accommodationDb = (IEnumerable<Accommodation>)_db.Accommodations.AsNoTracking()
				.Include(a => a.Partner)
				.Include(a => a.AccommodationImages);

			return accommodationDb.Select(a => a.ToIndexDto());
		}

		public void CreateRoomAndImages(RoomCreateDto dto, HttpPostedFileBase[] files, String path)
		{
			Room entity = dto.ToRoomCreateEntity();
			_db.Rooms.Add(entity);

			RoomImage img = new RoomImage();

			if (files.Length > 0 && files[0] != null)
			{
				foreach (HttpPostedFileBase file in files)
				{
					string fileName = SaveUploadedFile(path, file);
					img.Image = fileName;
					_db.RoomImages.Add(img);
					_db.SaveChanges();
				}
			}
			_db.SaveChanges();
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

		public void EditService(ServiceInfoVM vm)
		{
			var accommodationInDb = _db.Accommodations.Find(vm.AccommodationId);
			//var service = _db.Accommodations.FirstOrDefault(a => a.Id == vm.AccommodationId)?.ServiceInfos.Select(s => s.Id);

			accommodationInDb.ServiceInfos.Clear();
			foreach(var service in vm.ServiceInfoList)
			{
				var serviceInfo = _db.ServiceInfos.Find(service.Id);
				accommodationInDb.ServiceInfos.Add(serviceInfo);
			}

			_db.SaveChanges();

		}
	}
}