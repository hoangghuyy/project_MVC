using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Admin.Models;
using System.Data.SqlClient;
using System.Data;

namespace project_mvc.Services.Admin
{
	public class ProductDa(string connectionSql)
	{
		private readonly DapperDA DapperDA = new(connectionSql);

		[Obsolete]
		public async Task<List<ProductAdminItem>?> ListSearch(SearchModel search, int page, int rowPage)
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
                var result = await connect.QueryAsync<ProductAdminItem>("dbo.AdminProductListSearch", paras, commandType: CommandType.StoredProcedure);
				await connect.CloseAsync();
				return [.. result];
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		[Obsolete]
		public async Task<Products?> GetId(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Products>("SELECT * FROM Products WHERE IsDeleted = 0 AND Id=@id", new { id });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<bool> CheckProduct(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Products>("SELECT Id FROM Products WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result != null && result.Any();
			}
			catch
			{
				return false;
			}

		}
		[Obsolete]
		public async Task<Products?> GetByProductName(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Products>("SELECT * FROM Products WHERE IsDeleted = 0 AND Name=@name", new { name });
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
