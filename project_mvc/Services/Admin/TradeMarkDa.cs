using Dapper;
using project_mvc.Dappers;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using System.Data;
using System.Data.SqlClient;

namespace project_mvc.Services.Admin
{
	public class TradeMarkDa(string connectionSql)
	{
		private readonly DapperDA DapperDA = new(connectionSql);

		[Obsolete]
		public async Task<List<TradeMarkItem>?> ListSearch(SearchModel search, int page = 1, int rowPage = 10)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				int start = (page - 1) * rowPage;
				var paras = new DynamicParameters();
				paras.AddDynamicParams(new
				{
					search.Keyword,
					start,
					@size = rowPage
				});
				var result = await connect.QueryAsync<TradeMarkItem>("dbo.TradeMarkListSearch", paras, commandType: CommandType.StoredProcedure);
				await connect.CloseAsync();
				return [.. result];
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		//public async Task<List<TradeMarkItem>?> ListSearch(SearchModel search, int page, int rowPage, bool isExport)
		//{
		//	try
		//	{
		//		using SqlConnection connect = DapperDA.GetOpenConnection();
		//		if (search != null && !string.IsNullOrEmpty(search.Keyword))
		//		{
		//			var result = connect.Query<TradeMarkItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[OrderDisplay],[UrlPicture],[ParentId],[IsShow],[ModuleTypeCode] FROM TradeMarks WHERE IsDeleted = 0 AND Name LIKE N'%' + @Keyword + '%' ESCAPE N'~' ORDER BY  Id DESC", new { @Keyword = Utility.CharacterSpecail(search.Keyword) });
		//			await connect.CloseAsync();
		//			return result.ToList();
		//		}
		//		else
		//		{
		//			var result = connect.Query<TradeMarkItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[OrderDisplay],[UrlPicture],[ParentId],[IsShow],[ModuleTypeCode] FROM TradeMarks WHERE IsDeleted = 0 ORDER BY Id DESC");
		//			await connect.CloseAsync();
		//			return result.ToList();
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		return null;
		//	}
		//}
		[Obsolete]
		public async Task<TradeMarks?> GetId(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<TradeMarks>("SELECT * FROM TradeMarks WHERE IsDeleted = 0 AND Id=@id", new { id });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<bool> CheckTradeMark(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<TradeMarks>("SELECT Id FROM TradeMarks WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result != null && result.Any();
			}
			catch
			{
				return false;
			}

		}
		[Obsolete]
		public async Task<TradeMarks?> GetByTradeMark(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<TradeMarks>("SELECT * FROM TradeMarks WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<List<TradeMarkItem>?> GetAllById(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				string Sql = string.Empty;
				if (id == 0)
				{
					Sql = "select * from TradeMarks WHERE IsDeleted = 0";
					var result = await connect.QueryAsync<TradeMarkItem>(Sql);
					await connect.CloseAsync();
					return result?.ToList();
				}
				else
				{
					Sql = "select * from TradeMarks WHERE IsDeleted = 0 And Id!=@id";
					var result = await connect.QueryAsync<TradeMarkItem>(Sql, new { id });
					await connect.CloseAsync();
					return result?.ToList();
				}



			}
			catch (Exception ex)
			{
				return null;

			}
		}

		[Obsolete]
		public async Task<List<TradeMarks>?> GetAll()
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<TradeMarks>("select * from TradeMarks WHERE IsDeleted = 0");
				await connect.CloseAsync();
				return result?.ToList();

			}
			catch (Exception ex)
			{
				return null;

			}
		}
	}
}
