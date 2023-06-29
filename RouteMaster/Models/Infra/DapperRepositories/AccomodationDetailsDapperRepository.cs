using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace RouteMaster.Models.Infra.DapperRepositories
{
	public class AccomodationDetailsDapperRepository:IAccomodationDetailsRepository
	{
		private readonly string _connStr;

		public AccomodationDetailsDapperRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings
				["AppDbContext"].ConnectionString;
		}
		public IEnumerable<AccommodationDetailsDto> Search()
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT
      [OrderId]
      ,[AccommodationId]
      ,[AccommodationName]
      ,[RoomType]
      ,[RoomName]
      ,[CheckIn]
      ,[CheckOut]
      ,[RoomPrice]
  FROM AccommodationDetails where orderid = @orderid by orderid";
				return conn.Query<AccommodationDetailsDto>(sql);
			}
		}

		public void Create(AccommodationDetailsDto dto)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Edit(AccommodationDetailsDto dto)
		{
			throw new NotImplementedException();
		}

		
	}
}