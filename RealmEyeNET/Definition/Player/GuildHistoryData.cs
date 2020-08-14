using System.Collections.Generic;
using RealmEyeNET.ApiReturnCode;

namespace RealmEyeNET.Definition.Player
{
	public class GuildHistoryData
	{
		public ApiStatusCode Status { get; set; }
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