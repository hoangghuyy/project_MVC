using System.Data.SqlClient;
using System.Data;
using Dapper;
using project_mvc.Areas.Admin.Models;

namespace project_mvc.Dappers
{
	public class DapperDA(string conection)
	{
		private readonly string _connectionString = conection;

		[Obsolete]
		public SqlConnection GetOpenConnection()
		{
			SqlConnection connection = new(_connectionString);
			connection.Open();
			return connection;
		}

		[Obsolete]
		public async Task<SqlConnection> GetOpenConnectionAsync()
		{
			SqlConnection connection = new(_connectionString);
			await connection.OpenAsync();
			return connection;
		}

		[Obsolete]
		public async Task<int> Insert<T>(T obj, string nameTable)
		{
			try
			{
				Properties properties = ParseProperties(obj);
				string sql = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2}) SELECT CAST(scope_identity() AS int)",
					nameTable,
					string.Join(", ", properties.ValueNames.Select(x => $"{x}")),
					string.Join(", ", properties.ValueNames.Select(x => $"@{x}")));
				SqlConnection connection = GetOpenConnection();
				IEnumerable<int> id = await connection.QueryAsync<int>(sql, properties.ValuePairs, commandType: CommandType.Text);
				SetId(obj, id.First(), properties.IdPairs);
				await connection.CloseAsync();
				return id != null && id.Any() ? id.First() : 0;
			}
			catch
			{
				return 0;
			}
		}

		public async Task<int> Delete(dynamic id, string nameTable)
		{
			try
			{
				string sql = string.Format("UPDATE {0} SET IsDeleted = 1 WHERE Id = {1}", nameTable, id);
				int rs = await ExecuteAsync(CommandType.Text, sql, id);
				return rs;
			}
			catch
			{
				return 0;
			}
		}

		[Obsolete]
		public async Task<int> Update<T>(T obj, string nameTable)
		{
			try
			{
				Properties properties = ParseProperties(obj);
				string sqlIdPairs = GetSqlPairs(properties.IdNames);
				string sqlValuePairs = GetSqlPairs(properties.ValueNames);
				string sql = string.Format("UPDATE {0} SET {1} WHERE {2}", nameTable, sqlValuePairs, sqlIdPairs);
				await ExecuteAsync(CommandType.Text, sql, properties.AllPairs);
				return 1;
			}
			catch
			{
				return 0;
			}
		}

		[Obsolete]
		private async Task<int> ExecuteAsync(CommandType commandType, string sql, object? parameters = null)
		{
			SqlConnection connection = await GetOpenConnectionAsync();
			int result = await connection.ExecuteAsync(sql, parameters, commandType: commandType);
			await connection.CloseAsync();
			return result;
		}

		/// <summary>
		/// Create a commaseparated list of value pairs on 
		/// the form: "key1=@value1, key2=@value2, ..."
		/// </summary>
		private static string GetSqlPairs(IEnumerable<string> keys, string separator = ", ")
		{
			var pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();
			return string.Join(separator, pairs);
		}
		private static void SetId<T>(T obj, int id, IDictionary<string, object> propertyPairs)
		{
			if (propertyPairs.Count == 1)
			{
				var propertyName = propertyPairs.Keys.First();
				var propertyInfo = obj?.GetType().GetProperty(propertyName);
				if (propertyInfo?.PropertyType == typeof(int))
				{
					propertyInfo.SetValue(obj, id, null);
				}
			}
		}

		private static Properties ParseProperties<T>(T obj)
		{
			Properties propertyContainer = new();

			var typeName = typeof(T).Name.ToLower();

			var validKeyNames = new[]
				{
				"id",
				string.Format("{0}id", typeName), string.Format("{0}_id", typeName)
			};

			var properties = obj!.GetType().GetProperties();
			foreach (var property in properties)
			{
				// Skip reference types (but still include string!)
				if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
					continue;

				// Skip methods without a public setter
				if (property.GetSetMethod() == null)
					continue;

				var type = property.PropertyType;
				type = Nullable.GetUnderlyingType(type) ?? type;
				if (type != typeof(string) && type != typeof(int) && type != typeof(bool) && type != typeof(DateTime)
					&& type != typeof(double) && type != typeof(decimal) && type != typeof(Nullable) && type != typeof(Guid))
					continue;

				var name = property.Name;
				var value = property.GetValue(obj, null);

				if (validKeyNames.Contains(name.ToLower()))
				{
					propertyContainer.AddId(name, value!);
				}
				else if (type == typeof(DateTime))
				{
					if (Convert.ToDateTime(value) == DateTime.MinValue)
					{
						propertyContainer.AddValue(name, "");
					}
					else
					{
						propertyContainer.AddValue(name, value!);
					}
				}
				else
				{
					propertyContainer.AddValue(name, value!);
				}
			}

			return propertyContainer;
		}
	}
}
