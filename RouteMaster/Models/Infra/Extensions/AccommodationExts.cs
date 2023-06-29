﻿using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class AccommodationExts
	{
		private static AppDbContext _db = new AppDbContext(); 
        public static AccommodationIndexVM ToVM(this AccommodationIndexDto dto)
		{
			return new AccommodationIndexVM
			{
				Id = dto.Id,
				PartnerId = dto.PartnerId,
				Name = dto.Name,
				Address = dto.Address,
				AccommodationImage = dto.AccommodationImage
			};
		}
		
		public static AccommodationCreateDto ToDto(this AccommodationCreateVM vm)
		{
			return new AccommodationCreateDto
            {
				//Id = vm.Id,
				PartnerId = vm.PartnerId,
				Name = vm.Name,
                RegionId = vm.RegionId,
                TownId = vm.TownId,
                Address = vm.Address,
                PhoneNumber = vm.PhoneNumber,
                IndustryEmail = vm.IndustryEmail
            };
		}
		
		public static Accommodation ToEntity(this AccommodationCreateDto dto)
		{
			string address = GetFullAddress(dto);
			return new Accommodation
			{
				//Id = dto.Id,
				PartnerId = 1,//dto.PartnerId,
				Name = dto.Name,
				RegionId = dto.RegionId,
				TownId = dto.TownId,
				Address = address,
				PhoneNumber = dto.PhoneNumber,
				IndustryEmail = dto.IndustryEmail,
                CreateDate = DateTime.Now
            };
		}

		private static string GetFullAddress(AccommodationCreateDto dto)
		{
			return $"{_db.Regions.Where(r=>r.Id == dto.RegionId).FirstOrDefault().Name}{_db.Towns.Where(t=>t.Id == dto.TownId).FirstOrDefault().Name}{dto.Address}";
		}
	}
}