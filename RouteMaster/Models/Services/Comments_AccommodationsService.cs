using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class Comments_AccommodationsService
	{
		private IComments_AccommodationsRepository _repo;

		public Comments_AccommodationsService(IComments_AccommodationsRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<Comments_AccommodationsIndexDto> Search(Comments_AccommodationCriteria criteria)
		{
			return _repo.Search(criteria);
		}

		public Result Create (Comments_AccommodationsCreateDto dto, HttpPostedFileBase[] file1 , string path)
		{
			_repo.Create(dto, file1, path);
			return Result.Success();
		}

		public Result Update(Comments_AccommodationsEditDto dto)
		{
			_repo.Update(dto);
			return Result.Success();
		}

		public void DeleteComment_Accommodation(int id)
		{
			if(_repo.ExistImgWithinComment(id))
			{
				_repo.ClearImg(id);
			}

			_repo.DeleteComment(id);
		}
	}
}