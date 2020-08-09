using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public struct GuildHistoryData
	{
		public bool IsPrivate;
		public IList<GuildHistoryEntry> GuildHistory; 
	}

	public struct GuildHistoryEntry
	{
		public string GuildName;
		public string GuildRank;
		public string From;
		public string To;
	}
}