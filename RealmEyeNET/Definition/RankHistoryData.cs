using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public struct RankHistoryData
	{
		public bool IsPrivate;
		public IList<RankHistoryEntry> RankHistory; 
	}

	public struct RankHistoryEntry
	{
		public int Rank;
		public string Achieved;
	}
}