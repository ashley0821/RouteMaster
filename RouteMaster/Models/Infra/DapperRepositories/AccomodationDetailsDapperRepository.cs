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
	public class AccomodationDetailsDapperRepository
	{
		private readonly string _connStr;

		public AccomodationDetailsDapperRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings
				["AppDbContext"].ConnectionString;
		}

        public List<AccomodationDetailsVM> GetAccomodationDetails(int orderId)
		{
			string sql= @"SELECT [OrderId], [AccommodationId], [AccommodationName], [RoomType], [RoomName], [CheckIn], [CheckOut], [RoomPrice]
                   FROM AccommodationDetails
                   WHERE orderid = @orderid";
            IEnumerable<AccomodationDetailsVM> accomodationDetails = new SqlConnection(_connStr).Query<AccomodationDetailsVM>(sql, new {orderid= orderId });


            return accomodationDetails.ToList();
        }
    }


      
}
