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
	public class ExtraServiceDapperRepository : IExtraServiceRepository
	{
		private string _connstr;

        public ExtraServiceDapperRepository()
        {
			_connstr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
        }



		public IEnumerable<ExtraServiceIndexDto> Search()
		{

			using(var conn=new SqlConnection(_connstr))
			{

				string sql = @"select[Id], [Name], [AttractionId], 
[Price], [Description] ,[Status] 
from ExtraServices order by Price";
				return conn.Query<ExtraServiceIndexDto>(sql);	
			}
		}





		public void Create(ExtraServiceCreateDto dto)
		{

			//還沒測試
			using(var conn =new SqlConnection(_connstr))
			{

				string sql = @"INSERT INTO ExtraServices(
Name, AttractionId, Price, Description, Status
)VALUES(@Name, @AttractionId, @Price, @Description, @Status
)";

				conn.Execute(sql, dto);

			}

		

			

		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Edit(ExtraServiceEditDto dto)
		{
			throw new NotImplementedException();
		}

		public bool ExistExtraService(string name, int attractionId)
		{
			throw new NotImplementedException();
		}


	}
}