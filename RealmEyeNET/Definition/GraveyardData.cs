using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public struct GraveyardData
	{
		public bool IsPrivate;
		public IList<GraveyardEntry> Graveyard; 
	}

	public struct GraveyardEntry
	{
		public string DiedOn;
		public string Character;
		public int Level;
		public int BaseFame;
		public int TotalFame;
		public int Experience;
		public string[] Equipment;
		public int MaxedStats;
		public string KilledBy;
		public bool HadBackpack;
	}
}