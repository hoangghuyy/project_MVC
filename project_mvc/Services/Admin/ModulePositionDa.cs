using Dapper;
using project_mvc.Dappers;
using project_mvc.Helpers;
using project_mvc.Services.Admin.Models;
using System.Data.SqlClient;

namespace project_mvc.Services.Admin
{
	public class ModulePositionDa(string connectionSql)
	{
		private readonly DapperDA DapperDA = new(connectionSql);

		[Obsolete]
		public async Task<List<ModulePositionItem>?> ListSearch(SearchModel search, int page, int rowPage, bool isExport)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				if (search != null && !string.IsNullOrEmpty(search.Keyword))
				{
					var result = connect.Query<ModulePositionItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[Description],[ParentId],[IsShow],[OrderDisplay],[Code],[TypeView],[ModuleTypeCode],[NumberCount],[NumberContent], [ModuleContentIds], [ModuleProductIds],[UrlPicture],[UrlPictureMobile],[Video],[LinkBanner] FROM ModulePositions WHERE IsDeleted = 0 AND Name LIKE N'%' + @Keyword + '%' ESCAPE N'~' ORDER BY  Id DESC", new { @Keyword = Utility.CharacterSpecail(search.Keyword) });
					await connect.CloseAsync();
					return result.ToList();
				}
				else
				{
					var result = connect.Query<ModulePositionItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[Description],[ParentId],[IsShow],[OrderDisplay],[Code],[TypeView],[ModuleTypeCode],[NumberCount],[NumberContent], [ModuleContentIds], [ModuleProductIds],[UrlPicture],[UrlPictureMobile],[Video],[LinkBanner] FROM ModulePositions WHERE IsDeleted = 0 ORDER BY Id DESC");
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
		public async Task<ModulePositions?> GetId(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<ModulePositions>("SELECT * FROM ModulePositions WHERE IsDeleted = 0 AND Id=@id", new { id });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<bool> CheckModulePosition(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<ModulePositions>("SELECT Id FROM ModulePositions WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result != null && result.Any();
			}
			catch
			{
				return false;
			}

		}
		[Obsolete]
		public async Task<ModulePositions?> GetByNameModuleContent(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<ModulePositions>("SELECT * FROM ModulePositions WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<List<ModulePositionItem>?> GetAllById(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				string Sql = string.Empty;
				if (id == 0)
				{
					Sql = "select * from ModulePositions WHERE IsDeleted = 0";
					var result = await connect.QueryAsync<ModulePositionItem>(Sql);
					await connect.CloseAsync();
					return result?.ToList();
				}
				else
				{
					Sql = "select * from ModulePositions WHERE IsDeleted = 0 And Id!=@id";
					var result = await connect.QueryAsync<ModulePositionItem>(Sql, new { id });
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
		public async Task<List<ModulePositions>?> GetAll()
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<ModulePositions>("select * from ModulePositions WHERE IsDeleted = 0");
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
