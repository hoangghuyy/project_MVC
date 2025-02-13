namespace project_mvc.Areas.Admin.Models
{
	public class Properties
	{
		private readonly Dictionary<string, object> _ids;
		private readonly Dictionary<string, object> _values;

		#region Properties

		internal IEnumerable<string> IdNames
		{
			get { return _ids.Keys; }
		}

		internal IEnumerable<string> ValueNames
		{
			get { return _values.Keys; }
		}

		internal IEnumerable<string> AllNames
		{
			get { return _ids.Keys.Union(_values.Keys); }
		}

		internal IDictionary<string, object> IdPairs
		{
			get { return _ids; }
		}

		internal IDictionary<string, object> ValuePairs
		{
			get { return _values; }
		}

		internal IEnumerable<KeyValuePair<string, object>> AllPairs
		{
			get { return _ids.Concat(_values); }
		}

		#endregion Properties

		#region Constructor

		internal Properties()
		{
			_ids = [];
			_values = [];
		}

		#endregion Constructor

		#region Methods

		internal void AddId(string name, object value)
		{
			_ids.Add(name, value);
		}

		internal void AddValue(string name, object value)
		{
			_values.Add(name, value);
		}

		#endregion Methods
	}
}
