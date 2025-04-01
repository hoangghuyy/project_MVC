using Dapper;
using project_mvc.Dappers;
using project_mvc.Helpers;
using project_mvc.Services.Admin;
using project_mvc.Services.Admin.Models;
using project_mvc.Services.Client.Models;
using System;
using System.Data.SqlClient;

namespace project_mvc.Services.Client
{
    public class BannerManager(string connectionSql)
	{
        private readonly DapperDA DapperDA = new DapperDA(connectionSql);

		[Obsolete]
		public async Task<List<Banner>?> GetBannerToList(string positionCode)
		{
			try
			{
				using SqlConnection connect = DapperDA.GetOpenConnection();
				var result = await connect.QueryAsync<Banner>("SELECT * FROM Advertisings WHERE IsDeleted = 0 AND IsShow = 1 AND PositionCode = @positionCode", new { positionCode });
				await connect.CloseAsync();
				return result?.ToList();
			}
			catch(Exception ex)
			{
				return null;
			}
		}
		
	}
}
