using project_mvc.Dappers;

namespace project_mvc.Services.Admin
{
	public class BaseDa(string connectionSql)
	{
		private readonly DapperDA DapperDA = new(connectionSql);
		public async Task<int> Insert(dynamic obj, string nameTable) => await DapperDA.Insert(obj, nameTable);
		public async Task<int> Update(dynamic obj, string nameTable) => await DapperDA.Update(obj, nameTable);
		public async Task<int> Delete(dynamic id, string nameTable) => await DapperDA.Delete(id, nameTable);
	}
}
