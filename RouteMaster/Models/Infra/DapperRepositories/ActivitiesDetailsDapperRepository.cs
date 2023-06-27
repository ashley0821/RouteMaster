using Dapper;
using RouteMaster.Models.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.DapperRepositories
{
	public class ActivitiesDetailsDapperRepository
	{
		private readonly string _connStr;

		public ActivitiesDetailsDapperRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings
				["AppDbContext"].ConnectionString;
		}

		public IEnumerable<ActivitiesDetailsDto> GetActivitiesDetails()
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT 
       [OrderId]
      ,[ActivityId]
      ,[ActivityName]
      ,[StartTime]
      ,[EndTime]
      ,[Price]
      ,[Quantity]
  FROM ActivitiesDetails order by orderId";
				return conn.Query<ActivitiesDetailsDto>(sql);
			}

		}
	};
}
	