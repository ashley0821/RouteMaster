using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class Comments_AttractionsExts
	{
		public static Comments_AttractionsIndexVM ToIndexVM(this Comments_AttractionsIndexDto dto)
		{
			return new Comments_AttractionsIndexVM
			{
				Id = dto.Id,
				Account = dto.Account,
				Name = dto.Name,
				Score = dto.Score,
				StayHours = dto.StayHours,
				Price = dto.Price,
				CreateDate = dto.CreateDate
			};
		}
		public static Comments_AttractionsIndexDto ToIndexDto(this Comments_Attractions entity)
		{
			return new Comments_AttractionsIndexDto
			{
				Id = entity.Id,
				Account = entity.Member.Account,
				Name = entity.Attraction.Name,
				Score = entity.Score,
				StayHours = entity.StayHours,
				Price = entity.Price,
				CreateDate = entity.CreateDate
			};
		}
		public static Comments_AttractionsCreateDto ToCreateDto(this Comments_AttractionsCreateVM vm)
		{
			return new Comments_AttractionsCreateDto
			{
				MemberId = vm.MemberId,
				AttractionId = vm.AttractionId,
				Score = vm.Score,
				Content = vm.Content,
				StayHours = vm.StayHours,
				Price = vm.Price,
				Image = vm.Image
			};
		}

		public static Comments_Attractions ToCreateEnity(this Comments_AttractionsCreateDto dto)
		{
			return new Comments_Attractions
			{	
				MemberId = dto.MemberId,
				AttractionId = dto.AttractionId,
				Score = dto.Score,
				Content = dto.Content,
				StayHours = dto.StayHours,
				Price = dto.Price,
				CreateDate= DateTime.Now
			};
		}

		public static Comments_AttractionsDetailVM ToDetailVM (this Comments_AttractionsDetailDto dto)
		{
			return new Comments_AttractionsDetailVM
			{
				Id = dto.Id,
				MemberAccount = dto.MemberAccount,
				AttractioName = dto.AttractioName,
				Content = dto.Content,
				Score = dto.Score,
				Images = dto.Images

			};
		}

	}

}