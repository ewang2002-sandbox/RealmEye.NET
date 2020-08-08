namespace RealmEyeNET.Definition
{
	public struct CharacterData
	{
		public string CharacterType;
		public int Level;
		public int ClassQuestsCompleted;
		public int Fame;
		public int Experience;
		public int Place;
		public EquipmentData[] EquipmentData;
		public bool HasBackpack;
		public Stats Stats; 
	}

	public struct Stats
	{
		public int Health;
		public int Magic;
		public int Attack;
		public int Defense;
		public int Speed;
		public int Vitality;
		public int Wisdom;
		public int Dexterity;
		public int StatsMaxed;
	}

	public struct EquipmentData
	{
		public string ItemName;
		public string ItemType;
		public string Tier;
	}
}