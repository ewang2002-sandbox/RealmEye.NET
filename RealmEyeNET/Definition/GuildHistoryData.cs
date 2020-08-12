using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public class GuildHistoryData
	{
		public string Status { get; set; }
		public IList<GuildHistoryEntry> GuildHistory { get; set; }
	}

	public class GuildHistoryEntry
	{
		public string GuildName { get; set; }
		public string GuildRank { get; set; }
		public string From { get; set; }
		public string To { get; set; }
	}
}