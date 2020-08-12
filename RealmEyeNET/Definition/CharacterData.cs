using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public class CharacterData
	{
		public string Status { get; set; }
		public IList<CharacterEntry> Characters { get; set; }
	}

	public class CharacterEntry
	{
		public string CharacterType { get; set; }
		public int Level { get; set; }
		public int ClassQuestsCompleted { get; set; }
		public int Fame { get; set; }
		public long Experience { get; set; }
		public int Place { get; set; }
		public string[] EquipmentData { get; set; }
		public bool HasBackpack { get; set; }
		public Stats Stats { get; set; }
		public int StatsMaxed { get; set; }
	}

	public class Stats
	{
		public int Health;
		public int Magic;
		public int Attack;
		public int Defense;
		public int Speed;
		public int Vitality;
		public int Wisdom;
		public int Dexterity;
	}
}