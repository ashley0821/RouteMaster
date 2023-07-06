using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.DapperRepositories
{
	public class AttractionDapperRepository:IAttractionRepository
	{
		private readonly string _connStr; //最好要加readonly 防止連線字串被改掉

		public AttractionDapperRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
		}

		public void Create(AttractionCreateDto dto, HttpPostedFileBase[] files, string path)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void DeleteImage(int imageId)
		{
			throw new NotImplementedException();
		}

		public void Edit(AttractionEditDto dto)
		{
			using (var connection = new SqlConnection(_connStr))
			{
				string sql = @"UPDATE Attractions
                       SET AttractionCategoryId = @AttractionCategoryId,
                           RegionId = @RegionId,
                           TownId = @TownId,
                           Name = @Name,
                           Address = @Address,
                           PositionX = @PositionX,
                           PositionY = @PositionY,
                           Description = @Description,
                           Website = @Website
                       WHERE Id = @Id";

				connection.Execute(sql, dto);
			}
		}

		public void EditImage(AttractionImageIndexDto dto, HttpPostedFileBase file, string path)
		{
			throw new NotImplementedException();
		}

		public bool ExistAttraction(string Name)
		{
			throw new NotImplementedException();
		}

		public AttractionDetailDto Get(int id)
		{
			throw new NotImplementedException();
		}

		public AttractionEditDto GetEditDto(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<AttractionImageIndexDto> GetImages(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<AttractionIndexDto> Search()
		{
			throw new NotImplementedException();
		}

		public void UploadImage(AttractionImageIndexDto dto, HttpPostedFileBase[] files, string path)
		{
			throw new NotImplementedException();
		}
	}
}