namespace RealmEyeNET.Definition
{
	public class PlayerData
	{
		public string Name; 
		public int CharacterCount;
		public int Skins;
		public int Fame;
		public int Exp;
		public int Rank;
		public int AccountFame;
		public string Guild;
		public string GuildRank;
		public string FirstSeen;
		public string Created;
		public string LastSeen;
		public string[] Description;
		public bool IsHiddenOrErrored;

		public PlayerData() => CharacterCount = -1;
	}
}