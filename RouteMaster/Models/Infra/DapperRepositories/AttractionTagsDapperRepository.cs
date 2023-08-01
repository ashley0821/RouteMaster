using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels.Attractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Infra.DapperRepositories
{
	public class AttractionTagsDapperRepository
	{
		private readonly string _connStr; //最好要加readonly 防止連線字串被改掉

		public AttractionTagsDapperRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
		}

		public IEnumerable<string> SearchTags()
		{
			string sql = @"SELECT  [AttractionTags].Name
FROM [dbo].[Attractions]
LEFT JOIN [dbo].[Tags_Attractions]
ON Tags_Attractions.AttractionId = Attractions.Id
LEFT JOIN [dbo].[AttractionTags]
ON AttractionTags.Id = Tags_Attractions.TagId
ORDER BY [Attractions].Id";

			IEnumerable<string> Tags = new SqlConnection(_connStr)
								.Query<string>(sql);

			return Tags;
		}

		public IEnumerable<AttractionTagVM> AllTags()
		{
			string sql = @"SELECT * FROM [dbo].[AttractionTags]";

			IEnumerable<AttractionTagVM> Tags = new SqlConnection(_connStr)
								.Query<AttractionTagVM>(sql);

			return Tags;
		}

		public void AddTag(string attractionName, List<int> tagId)
		{
			foreach (var tag in tagId)
			{
				string sql = @"INSERT INTO [dbo].[Tags_Attractions] ([AttractionId], [TagId])
VALUES ((
	SELECT Id
	FROM [dbo].[Attractions]
	WHERE Name = @Name
), @TagId);";

				new SqlConnection(_connStr).Execute(sql, new { Name = attractionName, TagId = tag });
			}
		}

		public string GetTag(int id)
		{
			string sql = @"SELECT[AttractionTags].Name
FROM[dbo].[Attractions]
LEFT JOIN[dbo].[Tags_Attractions]
			ON Tags_Attractions.AttractionId = Attractions.Id
LEFT JOIN[dbo].[AttractionTags]
			ON AttractionTags.Id = Tags_Attractions.TagId
WHERE[Attractions].Id = @Id";

			var tag = new SqlConnection(_connStr)
								.QueryFirstOrDefault<string>(sql, new { Id = id });

			return tag;
		}

		public int? GetTagId(int id)
		{
			string sql = @"SELECT Tags_Attractions.TagId
FROM[dbo].[Attractions]
LEFT JOIN[dbo].[Tags_Attractions]
			ON Tags_Attractions.AttractionId = Attractions.Id
WHERE[Attractions].Id = @Id";

			var tagId = new SqlConnection(_connStr)
								.QueryFirstOrDefault<int?>(sql, new { Id = id });

			return tagId;
		}

		public void EditTag(int attractionId, List<int> tagId)
		{
			string checkSql = @"DELETE FROM [dbo].[Tags_Attractions] WHERE AttractionId = @AttractionId";

			int existingCount = new SqlConnection(_connStr).QueryFirstOrDefault<int>(checkSql, new { AttractionId = attractionId });

			foreach (var tag in tagId)
			{

				// 景點沒有標籤，執行插入操作
				string insertSql = @"INSERT INTO [dbo].[Tags_Attractions] (AttractionId, TagId)
VALUES (@AttractionId, @TagId)";

				new SqlConnection(_connStr).Execute(insertSql, new { AttractionId = attractionId, TagId = tag });


			}

		}
	}
}