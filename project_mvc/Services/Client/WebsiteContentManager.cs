using Dapper;
using project_mvc.Dappers;
using project_mvc.Services.Admin.Models;
using System.Data.SqlClient;

namespace project_mvc.Services.Client
{
	public class WebsiteContentManager
	{
		private readonly DapperDA _dapperDa;

		public WebsiteContentManager(string connectionSql)
		{
			_dapperDa = new DapperDA(connectionSql);
		}

		[Obsolete]
		public async Task<WebsiteContentItem> GetByNameAscii(string NameAscii)
		{
			using (SqlConnection connect = _dapperDa.GetOpenConnection())
			{
				var result = await connect.QueryAsync<WebsiteContentItem>(
					"SELECT c.*, (SELECT m.Comment FROM AspnetMembership m WHERE c.CreatorID = m.UserId AND m.IsLockedOut = 0 AND m.IsApproved = 1) CommentCreator " +
					"FROM WebsiteContent c WHERE c.IsDeleted = 0 AND c.IsShow = 1 AND c.IsApproved = 1 " +
					"AND c.NameAscii = @NameAscii AND c.CreatedDate <= GETDATE() ORDER BY c.ID DESC",
					new { NameAscii });
				await connect.CloseAsync();
				return result.FirstOrDefault();
			}
		}
	}
}
