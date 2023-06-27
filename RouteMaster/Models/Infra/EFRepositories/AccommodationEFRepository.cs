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
			Accommodation accommodation = dto.ToEntity();

            _db.Accommodations.Add(accommodation);
            _db.SaveChanges();
        }

        public bool ExistAccount(string name)
        {
            return _db.Accommodations.Any(m => m.Name == name);

        }

        public IEnumerable<AccommodationIndexDto> Search()
		{
			return _db.Accommodations.AsNoTracking()
				.Include(a => a.Partner)
				.Include(a => a.AccommodationImages)
				.Select(a => new AccommodationIndexDto
				{
					Id = a.Id,
					PartnerId = a.PartnerId,
					Name = a.Name,
					Address = a.Address,
					AccommodationImage = a.AccommodationImages.FirstOrDefault().Image
				});

		}
	}
}