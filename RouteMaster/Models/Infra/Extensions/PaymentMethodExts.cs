using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class PaymentMethodExts
	{

		public static PaymentMethodIndexVM ToIndexVM(this PaymentMethodDto dto)
		{
			return new PaymentMethodIndexVM
			{
				id = dto.id,
				Name = dto.Name,
				Description = dto.Description,
			};
		}

		public static PaymentMethodDto ToIndexDto(this PaymentMethodIndexVM vm)
		{
			return new PaymentMethodDto
			{
				id = vm.id,
				Name = vm.Name,
				Description = vm.Description,
			};
		}

		public static PaymentMethodDto ToIndexDto(this PaymentMethod entity)
		{
			return new PaymentMethodDto
			{
				id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
			};
		}
		public static PaymentMethod ToEntity(this PaymentMethodDto dto)
		{
			return new PaymentMethod
			{
				Id = dto.id,
				Name = dto.Name,
				Description = dto.Description,
			};
		}

		public static PaymentMethodCreateDto ToCreateDto(this PaymentMethodCreateVM vm)
		{
			return new PaymentMethodCreateDto
			{
				id = vm.id,
				Name = vm.Name,
				Description = vm.Description,
			};
		}
		public static PaymentMethod ToEntity(this PaymentMethodCreateDto dto)
		{
			return new PaymentMethod
			{
				Id = dto.id,
				Name = dto.Name,
				Description = dto.Description,
			};
		}

		public static PaymentMethodEditDto ToEditDto(this PaymentMethod entity)
		{
			return new PaymentMethodEditDto
			{
				id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
			};
		}
		public static PaymentMethodEditVM ToEditVM(this PaymentMethodEditDto dto)
		{
			return new PaymentMethodEditVM
			{
				id = dto.id,
				Name = dto.Name,
				Description = dto.Description,
			};
		}
		public static PaymentMethodEditDto ToEditDto(this PaymentMethodEditVM vm)
		{
			return new PaymentMethodEditDto
			{
				id = vm.id,
				Name = vm.Name,
				Description = vm.Description,
			};
		}
		public static PaymentMethod ToEntity(this PaymentMethodEditDto dto)
		{
			return new PaymentMethod
			{
				Id = dto.id,
				Name = dto.Name,
				Description = dto.Description,
			};
		}
		
	}
}