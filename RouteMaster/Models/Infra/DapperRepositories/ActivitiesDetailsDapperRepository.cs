using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
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

		public List<ActivitiesDetailsIndexVM>GetActivitiesDetails(int orderId)
		{
			
			string sql = @"SELECT 
       [OrderId]
      ,[ActivityId]
      ,[ActivityName]
      ,[StartTime]
      ,[EndTime]
      ,[Price]
      ,[Quantity]
  FROM ActivitiesDetails where orderId = @orderId order by orderId";
			IEnumerable<ActivitiesDetailsIndexVM>activitiesDetails=new SqlConnection(_connStr).Query<ActivitiesDetailsIndexVM>(sql, new { orderid = orderId });
            return activitiesDetails.ToList();
        }

		
	};
}
	