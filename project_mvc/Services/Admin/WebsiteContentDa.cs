using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Admin.Models;
using System.Data.SqlClient;
using System.Data;

namespace project_mvc.Services.Admin
{
	public class WebsiteContentDa(string connectionSql)
	{
		private readonly DapperDA DapperDA = new(connectionSql);

		[Obsolete]
		public async Task<List<WebsiteContentItem>?> ListSearch(SearchModel search, int page = 1, int rowPage = 10)
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
				var result = await connect.QueryAsync<WebsiteContentItem>("dbo.WebsiteContentListSearch", paras, commandType: CommandType.StoredProcedure);
				await connect.CloseAsync();
				return [.. result];
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		[Obsolete]
		public async Task<WebsiteContents?> GetId(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<WebsiteContents>("SELECT * FROM WebsiteContents WHERE IsDeleted = 0 AND Id=@id", new { id });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<bool> CheckContent(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<WebsiteContents>("SELECT Id FROM WebsiteContents WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result != null && result.Any();
			}
			catch
			{
				return false;
			}

		}
		[Obsolete]
		public async Task<WebsiteContents?> GetByNameModuleContent(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<WebsiteContents>("SELECT * FROM WebsiteContents WHERE IsDeleted = 0 AND Name=@name", new { name });
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
