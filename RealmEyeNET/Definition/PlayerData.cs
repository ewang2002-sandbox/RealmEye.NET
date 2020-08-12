namespace RealmEyeNET.Definition
{
	public class PlayerData
	{
		public string Name { get; set; }
		public int CharacterCount { get; set; }
		public int Skins { get; set; }
		public int Fame { get; set; }
		public int Exp { get; set; }
		public int Rank { get; set; }
		public int AccountFame { get; set; }
		public string Guild { get; set; }
		public string GuildRank { get; set; }
		public string FirstSeen { get; set; }
		public string Created { get; set; }
		public string LastSeen { get; set; }
		public string[] Description { get; set; }
		public string Status { get; set; }
		public PlayerData() => CharacterCount = -1;
	}
}