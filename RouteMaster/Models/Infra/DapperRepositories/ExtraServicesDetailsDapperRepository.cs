using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.DapperRepositories
{
	public class ExtraServicesDetailsDapperRepository : IExtraServiceDetailsRepository
	{
		private readonly string _connStr;

		public ExtraServicesDetailsDapperRepository()
		{
			_connStr =
			System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
		}


		public List<ExtraServicesDetailsVM> GetExtraServicesDetails(int orderId)
		{

			string sql = @"SELECT [Id]
		    ,[OrderId]
		    ,[ExtraServiceId]
		    ,[ExtraServiceName]
		    ,[Price]
		    ,[Quantity]
		       FROM ExtraServicesDetails WHERE orderid = @orderid";
			IEnumerable<ExtraServicesDetailsVM> extraServicesDetails = new SqlConnection(_connStr).Query<ExtraServicesDetailsVM>(sql, new { orderid = orderId });
			return extraServicesDetails.ToList();
		}

//		public void Edit(ExtraServicesDetailsDto dto)
//		{
//			using (var conn = new SqlConnection(_connStr))
//			{

//				string sql = @"Update ExtraServicesDetails SET 
//[OrderId]=@OrderId, 
//[ExtraServiceId]=@ExtraServiceId, 
//[ExtraServiceName]=@ExtraServiceName, 
//[Price]=@Price, 
//[Quantity]=@Quantity
//WHERE Id=@Id";

//				conn.Execute(sql, dto);
//			}
//		}

		public IEnumerable<ExtraServicesDetailsDto> Search()
		{
			using (var conn = new SqlConnection(_connStr))
			{

				string sql = @"SELECT [Id]
      ,[OrderId]
      ,[ExtraServiceId]
      ,[ExtraServiceName]
      ,[Price]
      ,[Quantity]
         FROM ExtraServicesDetails";
				return (IEnumerable<ExtraServicesDetailsDto>)conn.Query<ExtraServicesDetail>(sql);
			}
		}

		public void Create(ExtraServicesDetailsDto dto)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		void IExtraServiceDetailsRepository.ExtraServicesDetailsEdit(ExtraServicesDetailsEditDto dto)
		{
			using (var conn = new SqlConnection(_connStr))
			{

				string sql = @"Update ExtraServicesDetails SET 
[OrderId]=@OrderId, 
[ExtraServiceId]=@ExtraServiceId, 
[ExtraServiceName]=@ExtraServiceName, 
[Price]=@Price, 
[Quantity]=@Quantity
WHERE Id=@Id";

				conn.Execute(sql, dto);
			}
		}

	

		//ExtraServicesDetailsDto GetExtraServicesDetails(int id)
		//{
		//	string sql = @"SELECT [Id]
		//    ,[OrderId]
		//    ,[ExtraServiceId]
		//    ,[ExtraServiceName]
		//    ,[Price]
		//    ,[Quantity]
		//       FROM ExtraServicesDetails WHERE orderid = @orderid";
		//	IEnumerable<ExtraServicesDetailsDto> extraServicesDetails = new SqlConnection(_connStr).Query<ExtraServicesDetailsDto>(sql, new { orderid = id });
		//	return extraServicesDetails.ToList().FirstOrDefault();

		//}
	}

}
