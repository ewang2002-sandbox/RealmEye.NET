namespace RealmEyeNET.Definition
{
	public struct GraveyardSummaryData
	{
		public bool IsPrivate;
		public int MaxedStats;
		public int BaseFame;
		public GraveyardSummaryProperty[] Properties;
	}

	public struct GraveyardSummaryProperty
	{
		public string Achievement;
		public int Total;
		public int Max;
		public double Average;
		public int Min; 
	}
}