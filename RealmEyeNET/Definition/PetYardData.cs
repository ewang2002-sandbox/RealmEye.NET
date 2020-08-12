using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public struct PetYardData
	{
		public string Status { get; set; }
		public IList<PetEntry> Pets { get; set; }
	}

	public class PetEntry
	{
		// TODO make this into readable str
		public int ActivePetSkinId { get; set; }
		public string Name { get; set; }
		public string Rarity { get; set; }
		public string Family { get; set; }
		public int Place { get; set; }
		public IList<PetAbilityData> PetAbilities { get; set; }
		public int MaxLevel { get; set; }
	}

	public class PetAbilityData
	{
		public bool IsUnlocked { get; set; }
		public string AbilityName { get; set; }
		public int Level { get; set; }
		public bool IsMaxed { get; set; }
	}
}