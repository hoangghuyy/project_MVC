using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Admin.Models;
using System.Data.SqlClient;
using System.Data;
using project_mvc.Helpers;

namespace project_mvc.Services.Admin
{
	public class WebsiteModuleContentDa(string connectionSql)
	{
		private readonly DapperDA DapperDA = new(connectionSql);

		//[Obsolete]
		//public async Task<List<WebsiteModuleContentItem>?> ListSearch(SearchModel search, int page, int rowPage)
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
		//		var result = await connect.QueryAsync<WebsiteModuleContentItem>("dbo.WebsiteModuleContentListSearch", paras, commandType: CommandType.StoredProcedure);
		//		await connect.CloseAsync();
		//		return [.. result];
		//	}
		//	catch (Exception ex)
		//	{
		//		return null;
		//	}
		//}
		[Obsolete]
		public async Task<List<WebsiteModuleContentItem>?> ListSearch(SearchModel search, int page, int rowPage, bool isExport)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				if (search != null && !string.IsNullOrEmpty(search.Keyword))
				{
					var result = connect.Query<WebsiteModuleContentItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[NameAscii],[Code],[LinkUrl],[Description],[Content],[Video],[UrlPicture],[AlbumPictureJson],[IsShow],[CreatedDate],[ModifiedDate],[OrderDisplay],[ModuleTypeCode],[ParentId] FROM WebsiteModuleContents WHERE IsDeleted = 0 AND Name LIKE N'%' + @Keyword + '%' ESCAPE N'~' ORDER BY  Id DESC", new { @Keyword = Utility.CharacterSpecail(search.Keyword) });
					await connect.CloseAsync();
					return result.ToList();
				}
				else
				{
					var result = connect.Query<WebsiteModuleContentItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[NameAscii],[Code],[LinkUrl],[Description],[Content],[Video],[UrlPicture],[AlbumPictureJson],[IsShow],[CreatedDate],[ModifiedDate],[OrderDisplay],[ModuleTypeCode],[ParentId] FROM WebsiteModuleContents WHERE IsDeleted = 0 ORDER BY Id DESC");
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
		public async Task<WebsiteModuleContents?> GetId(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<WebsiteModuleContents>("SELECT * FROM WebsiteModuleContents WHERE IsDeleted = 0 AND Id=@id", new { id });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<bool> CheckModuleContent(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<WebsiteModuleContents>("SELECT Id FROM WebsiteModuleContents WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result != null && result.Any();
			}
			catch
			{
				return false;
			}

		}
		[Obsolete]
		public async Task<WebsiteModuleContents?> GetByNameModuleContent(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<WebsiteModuleContents>("SELECT * FROM WebsiteModuleContents WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<List<WebsiteModuleContentItem>?> GetAllById(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				string Sql = string.Empty;
				if(id == 0)
				{
					Sql="select * from WebsiteModuleContents WHERE IsDeleted = 0";
					var result = await connect.QueryAsync<WebsiteModuleContentItem>(Sql);
					await connect.CloseAsync();
					return result?.ToList();
				}
				else
				{
					Sql = "select * from WebsiteModuleContents WHERE IsDeleted = 0 And Id!=@id";
					var result = await connect.QueryAsync<WebsiteModuleContentItem>(Sql, new { id });
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
        public async Task<List<WebsiteModuleContents>?> GetAll()
        {
            try
            {
                using SqlConnection connect = DapperDA.GetOpenConnection();
                var result = await connect.QueryAsync<WebsiteModuleContents>("select * from WebsiteModuleContents WHERE IsDeleted = 0");
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
