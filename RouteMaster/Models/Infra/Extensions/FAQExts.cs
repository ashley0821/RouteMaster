using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class FAQExts
	{
		public static FAQIndexDto ToIndexDto(this FAQ entity)
		{
			return new FAQIndexDto
			{
				Id = entity.Id,
				CategoryName = entity.FAQCategory.Name,
				Question = entity.Question,
				Answer = entity.Answer,
				Helpful = entity.Helpful,
				CreateDate = entity.CreateDate,
				ModifiedDate = entity.ModifiedDate
			};
		}
		public static FAQIndexVM ToIndexVM(this FAQIndexDto dto)
		{
			return new FAQIndexVM
			{
				Id = dto.Id,
				CategoryName = dto.CategoryName,
				Question = dto.Question,
				Answer = dto.Answer,
				Helpful = dto.Helpful,
				CreateDate = dto.CreateDate,
				ModifiedDate = dto.ModifiedDate
			};
		}

		public static FAQCreateDto ToCreateDto(this FAQCreateVM vm)
		{
			return new FAQCreateDto
			{
				CategoryId = vm.CategoryId,
				Question = vm.Question,
				Answer = vm.Answer,
				Helpful = vm.Helpful
			};
		}

		public static FAQ ToCreateEntity(this FAQCreateDto dto)
		{
			return new FAQ
			{
				CategoryId = dto.CategoryId,
				Question = dto.Question,
				Answer = dto.Answer,
				Helpful = dto.Helpful,
				CreateDate = DateTime.Now,
				ModifiedDate = DateTime.Now
			};
		}

		public static FAQEditDto ToEditDto(this FAQ entity)
		{
			return new FAQEditDto
			{
				Id = entity.Id,
				CategoryId = entity.CategoryId,
				Question = entity.Question,
				Answer = entity.Answer,
				Helpful = entity.Helpful
			};
		}

		public static FAQEditVM ToEditVM(this FAQEditDto dto)
		{
			return new FAQEditVM
			{
				Id = dto.Id,
				CategoryId = dto.CategoryId,
				Question = dto.Question,
				Answer = dto.Answer,
				Helpful = dto.Helpful
			};
		}

		public static FAQEditDto ToEditDto(this FAQEditVM vm)
		{
			return new FAQEditDto
			{
				Id = vm.Id,
				CategoryId = vm.CategoryId,
				Question = vm.Question,
				Answer = vm.Answer,
				Helpful = vm.Helpful
			};
		}

		public static FAQEditImgIndexVM ToEditImgIndexVM(this FAQImage entity)
		{
			return new FAQEditImgIndexVM
			{
				Id = entity.Id,
				FAQId = entity.Id,
				Image = entity.Image
			};
		}

		public static FAQChangeImgVM ToChangeImgVM(this FAQImage entity)
		{
			return new FAQChangeImgVM
			{
				ImgId = entity.Id,
				FAQId=entity.FAQId,
				Image = entity.Image

			};
		}

		public static FAQDetailVM ToDetailVM(this FAQDetailDto dto)
		{
			return new FAQDetailVM
			{
				Id = dto.Id,
				CategoryName = dto.CategoryName,
				Question = dto.Question,
				Answer = dto.Answer,
				Helpful = dto.Helpful,
				Images = dto.Images
			};
		}



	}
}