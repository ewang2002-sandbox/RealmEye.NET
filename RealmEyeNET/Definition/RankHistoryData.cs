using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public class RankHistoryData
	{
		public string Status { get; set; }
		public IList<RankHistoryEntry> RankHistory { get; set; }
	}

	public class RankHistoryEntry
	{
		public int Rank { get; set; }
		public string Achieved { get; set; }
		public string Date { get; set; }
	}
}