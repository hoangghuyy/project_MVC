using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Admin.Models;
using System.Data.SqlClient;
using System.Data;
using project_mvc.Helpers;
using System;

namespace project_mvc.Services.Admin
{

	public class DepartmentDa(string connectionSql)
	{
		private readonly DapperDA DapperDA = new(connectionSql);

		//[Obsolete]
		//public async Task<List<DepartmentItem>?> ListSearch(SearchModel search, int page, int rowPage)
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
		//		var result = await connect.QueryAsync<DepartmentItem>("dbo.AdminUserListSearch", paras, commandType: CommandType.StoredProcedure);
		//		await connect.CloseAsync();
		//		return [.. result];
		//	}
		//	catch (Exception ex)
		//	{
		//		return null;
		//	}
		//}
		[Obsolete]
		public async Task<List<DepartmentItem>?> ListSearch(SearchModel search, int page, int rowPage, bool isExport)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				if (search != null && !string.IsNullOrEmpty(search.Keyword))
				{
					var result = connect.Query<DepartmentItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[OrderDisplay] FROM Departments WHERE IsDeleted = 0 AND Name LIKE N'%' + @Keyword + '%' ESCAPE N'~' ORDER BY  Id DESC", new { @Keyword = Utility.CharacterSpecail(search.Keyword) });
					await connect.CloseAsync();
					return result.ToList();
				}
				else
				{
					var result = connect.Query<DepartmentItem>("SELECT COUNT(ID) OVER () as TotalRecords, [Id],[Name],[OrderDisplay] FROM Departments WHERE IsDeleted = 0 ORDER BY Id DESC");
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
		public async Task<Departments?> GetId(int id)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Departments>("SELECT * FROM Departments WHERE IsDeleted = 0 AND Id=@id", new { id });
				await connect.CloseAsync();
				return result?.FirstOrDefault();
			}
			catch
			{
				return null;
			}

		}

		[Obsolete]
		public async Task<bool> CheckDepartment(string name)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Departments>("SELECT Id FROM Departments WHERE IsDeleted = 0 AND Name=@name", new { name });
				await connect.CloseAsync();
				return result != null && result.Any();
			}
			catch
			{
				return false;
			}

		}
		[Obsolete]
		public async Task<List<Departments>?> GetAll()
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Departments>("select * from Departments WHERE IsDeleted = 0");
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
