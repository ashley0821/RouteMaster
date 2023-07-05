using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class Comments_AccommodationsExts
	{
		public static Comments_AccommodationsIndexVM ToIndexVM(this Comments_AccommodationsIndexDto dto)
		{
			return new Comments_AccommodationsIndexVM	
			{
				Id = dto.Id,
				Account = dto.Account,
				Name = dto.Name,
				Score = dto.Score,
				Title = dto.Title,
				Pros = dto.Pros,
				Cons = dto.Cons,
				CreateDate = dto.CreateDate

			};
		}
		public static Comments_AccommodationsIndexDto ToIndexDto(this Comments_Accommodations entity)
		{
			return new Comments_AccommodationsIndexDto
			{
				Id = entity.Id,
				Account = entity.Member.Account,
				Name = entity.Accommodation.Name,
				Score = (float)entity.Score,
				Title = entity.Title,
				Pros = entity.Pros,
				Cons = entity.Cons,
				CreateDate = entity.CreateDate

			};
		}

		public static Comments_AccommodationsCreateDto ToCreateDto(this Comments_AccommodationsCreateVM vm)
		{
			return new Comments_AccommodationsCreateDto
			{
				MemberAccount = vm.MemberAccount,
				AccomodationId = vm.AccomodationId,
				Title = vm.Title,
				Pros = vm.Pros,
				Cons = vm.Cons,
				Score = vm.Score
			};
		}

		public static Comments_AccommodationsEditDto ToEditDto(this Comments_Accommodations entity)
		{
			return new Comments_AccommodationsEditDto
			{
				Id = entity.Id,
				Title = entity.Title,
				Pros = entity.Pros,
				Cons = entity.Cons,
				Score = (float)entity.Score
			};
		}
		public static Comments_AccommodationsEditVM ToEditVM(this Comments_AccommodationsEditDto dto)
		{
			return new Comments_AccommodationsEditVM
			{
				Id = dto.Id,
				Title = dto.Title,
				Pros = dto.Pros,
				Cons = dto.Cons,
				Score = dto.Score
			};
		}
		public static Comments_AccommodationsEditDto ToEditDto(this Comments_AccommodationsEditVM vm)
		{
			return new Comments_AccommodationsEditDto
			{
				Id=vm.Id,
				Title = vm.Title,	
				Pros = vm.Pros,
				Cons = vm.Cons,
				Score = vm.Score
			};
		}

		public static Comments_AccommodationsImgIndexVM ToImgIndexVM(this Comments_AccommodationImages entity)
		{
			return new Comments_AccommodationsImgIndexVM
			{
				Id = entity.Id,
				CommentId = entity.Comments_AccommodationId,
				Image = entity.Image
			};
		}

		public static Comments_AccommodationsChangeImgVM ToChangeImgVM(this  Comments_AccommodationImages entity)
		{
			return new Comments_AccommodationsChangeImgVM
			{
				ImgId = entity.Id,
				CommentId = entity.Comments_AccommodationId,
				Image = entity.Image
			};
		}
	}
}