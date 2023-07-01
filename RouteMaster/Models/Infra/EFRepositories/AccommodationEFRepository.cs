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

			//var length = db.Regions.Select(r => r.Id == accommodationdb.RegionId);


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

	}
}