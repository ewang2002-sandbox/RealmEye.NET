using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public class NameHistoryData
	{
		public bool IsPrivate { get; set; }
		public IList<NameHistoryEntry> NameHistory { get; set; }
	}

	public class NameHistoryEntry
	{
		public string Name { get; set; }
		public string From { get; set; }
		public string To { get; set; }
	}
}