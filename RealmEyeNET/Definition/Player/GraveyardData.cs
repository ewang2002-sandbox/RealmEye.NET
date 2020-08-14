using System.Collections.Generic;
using RealmEyeNET.ApiReturnCode;

namespace RealmEyeNET.Definition.Player
{
	public class GraveyardData
	{
		public ApiStatusCode Status { get; set; }
		public int GraveyardCount { get; set; }
		public IList<GraveyardEntry> Graveyard { get; set; }
	}

	public class GraveyardEntry
	{
		public string DiedOn { get; set; }
		public string Character { get; set; }
		public int Level { get; set; }
		public int BaseFame { get; set; }
		public int TotalFame { get; set; }
		public long Experience { get; set; }
		public string[] Equipment { get; set; }
		public int MaxedStats { get; set; }
		public string KilledBy { get; set; }
		public bool HadBackpack { get; set; }
	}
}