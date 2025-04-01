using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Admin.Models;
using System.Data.SqlClient;
using System.Data;

namespace project_mvc.Services.Admin
{
	public class AdvertisingDa(string connectionSql)
	{
		private readonly DapperDA DapperDA = new(connectionSql);

		//[Obsolete]
		//public async Task<List<AdvertisingItem>?> ListSearch(SearchModel search, int page, int rowPage)
		//{
		//	try
		//	{
		//		using SqlConnection connect = DapperDA.GetOpenConnection();
		//		page = page > 1 ? page : 1;
		//		rowPage = rowPage > 1 ? rowPage : 10;
		//		int start = (page - 1) * rowPage;
		//		var paras = new DynamicParameters();
		//		paras.AddDynamicParams(new
		//		{
		//			search.Keyword,
		//			start,
		//			@size = rowPage
		//		});
		//		var result = await connect.QueryAsync<AdvertisingItem>("dbo.AdvertisingListSearch", paras, commandType: CommandType.StoredProcedure);
		//		await connect.CloseAsync();
		//		return [.. result];
		//	}
		//	catch (Exception ex)
		//	{
		//		return null;
		//	}
		//}

		[Obsolete]
		public async Task<List<AdvertisingItem>?> ListSearch(SearchModel search, int page, int rowPage)
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
                var result = await connect.QueryAsync<AdvertisingItem>("dbo.AdvertisingListSearch", paras, commandType: CommandType.StoredProcedure);
				await connect.CloseAsync();
				return [.. result];
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		[Obsolete]
		public async Task<Advertisings?> GetId(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Advertisings>("SELECT * FROM Advertisings WHERE IsDeleted = 0 AND Id=@id", new { id });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<bool> CheckAdvertising(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Advertisings>("SELECT Id FROM Advertisings  WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result != null && result.Any();
			}
			catch
			{
				return false;
			}

		}
		[Obsolete]
		public async Task<Advertisings?> GetByAdvertisingName(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Advertisings>("SELECT * FROM Advertisings WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}
	}
}
