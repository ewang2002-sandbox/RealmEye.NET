using System.Collections.Generic;
using RealmEyeNET.ApiReturnCode;

namespace RealmEyeNET.Definition.Player
{
	public class CharacterData
	{
		public ApiStatusCode Status { get; set; }
		public IList<CharacterEntry> Characters { get; set; }
	}

	public class CharacterEntry
	{
		// TODO make this into readable str
		public int ActivePetId { get; set; }
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
		public int Health { get; set; }
		public int Magic { get; set; }
		public int Attack { get; set; }
		public int Defense { get; set; }
		public int Speed { get; set; }
		public int Vitality { get; set; }
		public int Wisdom { get; set; }
		public int Dexterity { get; set; }
	}
}