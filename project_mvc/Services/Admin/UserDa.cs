using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Admin.Models;
using System.Data.SqlClient;
using System.Data;

namespace project_mvc.Services.Admin
{
	public class UserDa(string connectionSql)
	{
		private readonly DapperDA DapperDA = new(connectionSql);

		[Obsolete]
		public async Task<List<UserAdminItem>?> ListSearch(SearchModel search, int page, int rowPage)
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
                var result = await connect.QueryAsync<UserAdminItem>("dbo.AdminUserListSearch", paras, commandType: CommandType.StoredProcedure);
				await connect.CloseAsync();
				return [.. result];
			}
			catch (Exception ex)
			{
				return null;
			}
		}

        [Obsolete]
        public async Task<UserAdmins?> GetId(int id)
        {
            try
            {
                using SqlConnection connect = DapperDA.GetOpenConnection();
                var result = await connect.QueryAsync<UserAdmins>("SELECT * FROM UserAdmins WHERE IsDeleted = 0 AND Id=@id", new { id });
                await connect.CloseAsync();
                return result?.FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        [Obsolete]
        public async Task<bool> CheckUser(string user)
        {
            try
            {
                using SqlConnection connect = DapperDA.GetOpenConnection();
                var result = await connect.QueryAsync<UserAdmins>("SELECT Id FROM UserAdmins WHERE IsDeleted = 0 AND UserName=@user", new { user });
                await connect.CloseAsync();
                return result != null && result.Any();
            }
            catch
            {
                return false;
            }

        }
		[Obsolete]
		public async Task<UserAdmins?> GetByUserName(string user)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<UserAdmins>("SELECT * FROM UserAdmins WHERE IsDeleted = 0 AND UserName=@user", new { user });
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
