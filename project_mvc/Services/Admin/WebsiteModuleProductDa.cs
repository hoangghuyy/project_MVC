using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Admin.Models;
using System.Data.SqlClient;
using System.Data;
using project_mvc.Helpers;

namespace project_mvc.Services.Admin
{
    public class WebsiteModuleProductDa(string connectionSql)
    {
        private readonly DapperDA DapperDA = new(connectionSql);

		//[Obsolete]
		//public async Task<List<WebsiteModuleProductItem>?> ListSearch(SearchModel search, int page, int rowPage)
		//{
		//    try
		//    {
		//        using SqlConnection connect = DapperDA.GetOpenConnection();
		//        page = page > 1 ? page : 1;
		//        rowPage = rowPage > 1 ? rowPage : 10;
		//        int start = (page - 1) * rowPage;
		//        var paras = new DynamicParameters();
		//        paras.AddDynamicParams(new
		//        {
		//            search.Keyword,
		//            start,
		//            @size = rowPage
		//        });
		//        var result = await connect.QueryAsync<WebsiteModuleProductItem>("dbo.WebsiteModuleContentListSearch", paras, commandType: CommandType.StoredProcedure);
		//        await connect.CloseAsync();
		//        return [.. result];
		//    }
		//    catch (Exception ex)
		//    {
		//        return null;
		//    }
		//}
		[Obsolete]
		public async Task<List<WebsiteModuleProductItem>?> ListSearch(SearchModel search, int page, int rowPage, bool isExport)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				if (search != null && !string.IsNullOrEmpty(search.Keyword))
				{
					var result = connect.Query<WebsiteModuleProductItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[Title],[LinkUrl],[OrderDisplay],[UrlPicture],[UrlVideo],[ParentId],[Description],[Content],[IsShow],[NameAscii],[ModuleTypeCode] FROM WebsiteModuleProducts WHERE IsDeleted = 0 AND Name LIKE N'%' + @Keyword + '%' ESCAPE N'~' ORDER BY  Id DESC", new { @Keyword = Utility.CharacterSpecail(search.Keyword) });
					await connect.CloseAsync();
					return result.ToList();
				}
				else
				{
					var result = connect.Query<WebsiteModuleProductItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[Title],[LinkUrl],[OrderDisplay],[UrlPicture],[UrlVideo],[ParentId],[Description],[Content],[IsShow],[NameAscii],[ModuleTypeCode] FROM WebsiteModuleProducts WHERE IsDeleted = 0 ORDER BY Id DESC");
					await connect.CloseAsync();
					return result.ToList();
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		[Obsolete]
        public async Task<WebsiteModuleProducts?> GetId(int id)
        {
            try
            {
                using SqlConnection connect = DapperDA.GetOpenConnection();
                var result = await connect.QueryAsync<WebsiteModuleProducts>("SELECT * FROM WebsiteModuleProducts WHERE IsDeleted = 0 AND Id=@id", new { id });
                await connect.CloseAsync();
                return result?.FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        [Obsolete]
        public async Task<bool> CheckModuleProduct(string name)
        {
            try
            {
                using SqlConnection connect = DapperDA.GetOpenConnection();
                var result = await connect.QueryAsync<WebsiteModuleProducts>("SELECT Id FROM WebsiteModuleProducts WHERE IsDeleted = 0 AND Name=@name", new { name });
                await connect.CloseAsync();
                return result != null && result.Any();
            }
            catch
            {
                return false;
            }

        }
        [Obsolete]
        public async Task<WebsiteModuleProducts?> GetByNameModuleProduct(string name)
        {
            try
            {
                using SqlConnection connect = DapperDA.GetOpenConnection();
                var result = await connect.QueryAsync<WebsiteModuleProducts>("SELECT * FROM WebsiteModuleProducts WHERE IsDeleted = 0 AND Name=@name", new { name });
                await connect.CloseAsync();
                return result?.FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        [Obsolete]
        public async Task<List<WebsiteModuleProductItem>?> GetAllById(int id)
        {
            try
            {
                using SqlConnection connect = DapperDA.GetOpenConnection();
                string Sql = string.Empty;
                if (id == 0)
                {
                    Sql = "select * from WebsiteModuleProducts WHERE IsDeleted = 0";
                    var result = await connect.QueryAsync<WebsiteModuleProductItem>(Sql);
                    await connect.CloseAsync();
                    return result?.ToList();
                }
                else
                {
                    Sql = "select * from WebsiteModuleProducts WHERE IsDeleted = 0 And Id!=@id";
                    var result = await connect.QueryAsync<WebsiteModuleProductItem>(Sql, new { id });
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
        public async Task<List<WebsiteModuleProducts>?> GetAll()
        {
            try
            {
                using SqlConnection connect = DapperDA.GetOpenConnection();
                var result = await connect.QueryAsync<WebsiteModuleProducts>("select * from WebsiteModuleProducts WHERE IsDeleted = 0");
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
