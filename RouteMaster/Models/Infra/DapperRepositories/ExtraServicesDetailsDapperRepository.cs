using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.DapperRepositories
{

}
//{
//	public class ExtraServicesDetailsDapperRepository
//	{
//		private readonly string _connStr;

//		public ExtraServicesDetailsDapperRepository()
//		{
//			_connStr =
//			System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
//		}
//		public IEnumerable<ExtraServicesDetailsDto> search(ExtraServicesDetailsVM vm)
//		{
//			using(var conn = new SqlConnection(_connStr))
//			{
//				string sql = @"SELECT [Id]
//      ,[OrderId]
//      ,[ExtraServiceId]
//      ,[ExtraServiceName]
//      ,[Price]
//      ,[Quantity]
//  FROM ExtraServicesDetails order by OrderId";
//				return conn.Query<ExtraServicesDetailsDto>(sql,vm);
//			}
//		}
//	}
//}