using Dapper;
using project_mvc.Dappers;
using project_mvc.Helpers;
using project_mvc.Services.Admin;
using project_mvc.Services.Admin.Models;
using System;
using System.Data.SqlClient;

namespace project_mvc.Services.Client
{
    public class WebsiteModuleManager
    {
        private readonly DapperDA DapperDA;
        public WebsiteModuleManager(string connectionSql)
        {
            DapperDA = new DapperDA(connectionSql);
        }
        [Obsolete]
		public async Task<WebsiteModuleContentItem?> GetByNameContentAscii(string nameAscii)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<WebsiteModuleContentItem>("SELECT * FROM WebsiteModuleContents WHERE IsDeleted = 0 AND IsShow = 1 AND NameAscii = @nameAscii", new { nameAscii });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}
		}
		[Obsolete]
		public async Task<WebsiteModuleProductItem?> GetByNameProductAscii(string nameAscii)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<WebsiteModuleProductItem>("SELECT * FROM WebsiteModuleProducts WHERE IsDeleted = 0 AND IsShow = 1 AND NameAscii = @nameAscii", new { nameAscii });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}
		}
		[Obsolete]

		public async Task<WebsiteModuleContentItem?> GetModuleById(int id)
		{
			try
			{
				using (SqlConnection connect = DapperDA.GetOpenConnection())
				{
					

					var result = await connect.QueryAsync<WebsiteModuleContentItem>(
						"SELECT * FROM WebsiteModuleContents WHERE IsDeleted = 0 AND IsShow = 1 AND Id = @id",
						new { id }
					);

					
					var module = result.FirstOrDefault();
					

					return module;
				}
			}
			catch (Exception ex)
			{
				
				return null;
			}
		}

		[Obsolete]
		public List<WebsiteModuleContentItem> GetListByArrId(string listArray)
		{
			using SqlConnection connect = DapperDA.GetOpenConnection();
			IEnumerable<WebsiteModuleContentItem> result = connect.Query<WebsiteModuleContentItem>("SELECT * FROM WebsiteModuleContents where IsDeleted = 0 AND IsShow =1 AND ',' + @listArray + ',' LIKE '%,'+CONVERT(varchar(10), ID)+',%' Order By Id desc", new { listArray });
			connect.Close();
			return result.ToList();
		}

		[Obsolete]
		public List<WebsiteModuleProductItem> GetByListModuleTypeCode(string code)
		{
			using (SqlConnection connect = DapperDA.GetOpenConnection())
			{
				IEnumerable<WebsiteModuleProductItem> result = connect.Query<WebsiteModuleProductItem>("SELECT * FROM WebsiteModuleProducts where IsDeleted = 0 AND IsShow =1 AND ',' + @code + ',' like '%,'+ModuleTypeCode+',%'", new { code });
				connect.Close();
				return result.ToList();
			}
		}

		[Obsolete]
		public List<int> GetBrandIds(string brandIds)
		{
			if (string.IsNullOrEmpty(brandIds))
				return new List<int>();

			using (SqlConnection connect = DapperDA.GetOpenConnection())
			{
				// Giả sử brandIds là chuỗi phân cách bởi dấu phẩy: "1,2,3"
				string sql = @"SELECT DISTINCT BrandId 
                      FROM Products 
                      WHERE IsDeleted = 0 
                      AND IsShow = 1
                      AND BrandId IN (SELECT value FROM STRING_SPLIT(@brandIds, ','))";

				var result = connect.Query<int>(sql, new { brandIds });
				return result.ToList();
			}
		}
	}
}
