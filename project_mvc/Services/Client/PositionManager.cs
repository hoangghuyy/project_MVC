using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Client.Models;
using System.Data.SqlClient;

namespace project_mvc.Services.Client
{
	public class PositionManager(string connectionSql)
	{
		private readonly DapperDA DapperDA = new DapperDA(connectionSql);

		[Obsolete]
		public async Task<List<ModulePosition>?> GetPositionToList(string code)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<ModulePosition>("SELECT * FROM ModulePositions WHERE IsDeleted = 0 AND IsShow = 1 AND Code = @code", new { code });
				await connect.CloseAsync();
				return result?.ToList();
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		[Obsolete]
		public async Task<List<ModulePosition>?> GetModuleProductIds(string moduleIds)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<ModulePosition>("SELECT * FROM ModulePositions WHERE IsDeleted = 0 AND IsShow = 1 AND (',' + ModuleProductIds + ',' LIKE '%,' + @moduleIds + ',%' OR ModuleProductIds = @moduleIds)", new { moduleIds });
				await connect.CloseAsync();
				return result?.ToList();
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		[Obsolete]		
		
		public List<ModulePosition> GetListViewIndex(string view)
		{
			using (SqlConnection connect = DapperDA.GetOpenConnection())
			{
				IEnumerable<ModulePosition> result = connect.Query<ModulePosition>("with name_tree as (" +
					" select *,OrderTree =0 from ModulePositions where Code = @view union all" +
					" select c.*,OrderTree=OrderTree+1 from ModulePositions c join name_tree p on p.Id = c.ParentId  AND c.IsDeleted=0 And c.IsShow = 1)" +
					" select * from name_tree ORDER BY OrderTree ASC", new { view });
				connect.Close();
				return result.ToList();
			}
		}
		
		[Obsolete]
		public List<WebsiteModuleProducts> GetListModuleInPositionIds(string moduleIds)
		{
			using SqlConnection connect = DapperDA.GetOpenConnection();
			var idList = moduleIds.Split(',').Select(int.Parse).ToList();
			var result = connect.Query<WebsiteModuleProducts>("SELECT * FROM WebsiteModuleProducts WHERE IsShow = 1 AND IsDeleted = 0 AND Id in @idList ORDER BY OrderDisplay ASC", new { idList });
			connect.Close();
			return result.ToList();
		}

		[Obsolete]
		public List<WebsiteModuleProducts> GetListModuleContentInPositionIds(string moduleIds)
		{
			using SqlConnection connect = DapperDA.GetOpenConnection();
			var idList = moduleIds.Split(',').Select(int.Parse).ToList();
			var result = connect.Query<WebsiteModuleProducts>("SELECT * FROM WebsiteModuleContents WHERE IsShow = 1 AND IsDeleted = 0 AND Id in @idList ORDER BY OrderDisplay ASC", new { idList });
			connect.Close();
			return result.ToList();
		}

		[Obsolete]
		public List<ProductAdminItem> GetListProductByModule(string moduleIds)
		{
			using SqlConnection connect = DapperDA.GetOpenConnection();
			var result = connect.Query<ProductAdminItem>("select * from Products WHERE IsDeleted = 0 and IsShow = 1 AND  ',' + @moduleIds + ',' LIKE '%,' + ModuleIds + ',%' ORDER BY OrderDisplay ASC", new { moduleIds });
			connect.Close();
			return result.ToList();
		}
	}
}
