using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Client.Models;
using System.Data.SqlClient;

namespace project_mvc.Services.Client
{
	public class ModuleProductManager(string connectionSql)
	{
		private readonly DapperDA DapperDA = new DapperDA(connectionSql);

		[Obsolete]
		public async Task<List<ModuleProduct>?> GetContentToList(int moduleIds)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<ModuleProduct>("SELECT * FROM WebsiteModuleProducts WHERE IsDeleted = 0 AND IsShow = 1 AND ModuleIds = @moduleIds", new { moduleIds });
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
