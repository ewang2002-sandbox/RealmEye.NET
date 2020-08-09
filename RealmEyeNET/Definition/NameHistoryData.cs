using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public struct NameHistoryData
	{
		public bool IsPrivate;
		public IList<NameHistoryEntry> NameHistory; 
	}

	public struct NameHistoryEntry
	{
		public string Name;
		public string From;
		public string To;
	}
}