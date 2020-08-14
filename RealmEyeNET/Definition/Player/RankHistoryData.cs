using System.Collections.Generic;
using RealmEyeNET.ApiReturnCode;

namespace RealmEyeNET.Definition.Player
{
	public class RankHistoryData
	{
		public ApiStatusCode Status { get; set; }
		public IList<RankHistoryEntry> RankHistory { get; set; }
	}

	public class RankHistoryEntry
	{
		public int Rank { get; set; }
		public string Achieved { get; set; }
		public string Date { get; set; }
	}
}