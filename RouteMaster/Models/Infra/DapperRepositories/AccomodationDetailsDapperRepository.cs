using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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

		public List<AccomodationDetailsVM> GetAccomodationDetails(int orderId)

		{
			string sql = @"SELECT [id], [OrderId], [AccommodationId], [AccommodationName], [RoomType], [RoomName], [CheckIn], [CheckOut], [RoomPrice]
                   FROM AccommodationDetails
                   WHERE orderid = @orderid";
			IEnumerable<AccomodationDetailsVM> accomodationDetails = new SqlConnection(_connStr).Query<AccomodationDetailsVM>(sql, new { orderid = orderId });


			return accomodationDetails.ToList();
		}

		public AccomodationDetailsEditDto GetAccomodationDetailsEditDetails(int id)
		{
			using (var conn = new SqlConnection(_connStr))
			{

				string sql = @"SELECT [Id]
      ,[OrderId]
      ,[AccommodationId]
      ,[AccommodationName]
      ,[RoomType]
      ,[RoomName]
      ,[CheckIn]
      ,[CheckOut]
      ,[RoomPrice]
  FROM [AccommodationDetails]Where id=@id ";
				return conn.QuerySingleOrDefault<AccomodationDetailsEditDto>(sql, new { id });
			}
		}


		void IAccomodationDetailsRepository.AccomodationDetailsEdit(AccomodationDetailsEditDto dto)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"Update AccommodationDetails SET 
[OrderId]=@OrderId, 
[AccommodationId]=@AccommodationId, 
[AccommodationName]=@AccommodationName, 
[RoomType]=@RoomType, 
[RoomName]=@RoomName,
[CheckIn]=@CheckIn,
[CheckOut]=@CheckOut,
[RoomPrice]=@RoomPrice
WHERE Id=@Id";
				conn.Execute(sql, dto);

				string roomPriceQuery = @"SELECT SUM(RoomPrice) FROM AccommodationDetails WHERE orderid = @orderid";
				int roomPriceTotal = conn.ExecuteScalar<int>(roomPriceQuery, new { orderid = dto.OrderId });

				string sqlOrder = @"UPDATE Orders SET Total = @Total WHERE Id = @OrderId";
				conn.Execute(sqlOrder, new { Total = roomPriceTotal, orderid = dto.OrderId });

			}
		}

		

		void IAccomodationDetailsRepository.AccomodationDetailsDelete(int id)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"DELETE FROM AccommodationDetails WHERE Id=@Id";
				conn.Execute(sql, new { id });
			}
		}

		
		public IEnumerable<AccommodationDetailsDto> Search(int orderId)
		{
			throw new NotImplementedException();
		}

		AccomodationDetailsVM IAccomodationDetailsRepository.GetAccomodationDetailsById(int id)
		{
			using (var conn = new SqlConnection(_connStr))
			{

				string sql = @"SELECT [Id]
      ,[OrderId]
      ,[AccommodationId]
      ,[AccommodationName]
      ,[RoomType]
      ,[RoomName]
      ,[CheckIn]
      ,[CheckOut]
      ,[RoomPrice]
  FROM [AccommodationDetails]Where id=@id";
				return conn.QuerySingleOrDefault<AccomodationDetailsVM>(sql, new { id });
			}
		}
	}


      
}
