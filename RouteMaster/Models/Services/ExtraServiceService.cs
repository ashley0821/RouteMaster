using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class ExtraServiceService
	{
		private IExtraServiceRepository _repo;

        public ExtraServiceService(IExtraServiceRepository repo)
        {
            _repo = repo;            
        }

        public IEnumerable<ExtraServiceIndexDto> Search()
        {
            return _repo.Search();
        }


        public Result Create(ExtraServiceCreateDto dto)
        {

            if (_repo.ExistExtraService(dto.Name, dto.AttractionId))
            {
                return Result.Fail("該景點已存在此額外服務項目");
            }
            else
            {
                _repo.Create(dto);
                return Result.Success();
            }
            
        }

        public Result Edit(ExtraServiceEditDto dto)
        {

            if (_repo.ExistExtraService(dto.Name, dto.AttractionId))
            {
                return Result.Fail("該景點已存在此額外服務項目");
            }
            else
            {
                _repo.Edit(dto);
                return Result.Success();
            }
           
        }



        public Result Delete(int id)
        {
            _repo.Delete(id);
            return Result.Success();
        }



       public ExtraServiceEditDto GetExtraServiceById(int id)
        {
            var extraServiceInDb= _repo.GetExtraServiceById(id);
            return extraServiceInDb;
        }



    }
}