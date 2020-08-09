using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public struct CharacterData
	{
		public bool IsPrivate;
		public IList<CharacterEntry> Characters;
	}

	public struct CharacterEntry
	{
		public string CharacterType;
		public int Level;
		public int ClassQuestsCompleted;
		public int Fame;
		public long Experience;
		public int Place;
		public string[] EquipmentData;
		public bool HasBackpack;
		public Stats Stats;
		public int StatsMaxed;
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
	}
}