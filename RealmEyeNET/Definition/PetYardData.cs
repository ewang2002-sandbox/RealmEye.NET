using System.Collections.Generic;

namespace RealmEyeNET.Definition
{
	public struct PetYardData
	{
		public bool IsPrivate;
		public IList<PetEntry> Pets;
	}

	public struct PetEntry
	{
		// TODO make this into readable str
		public int ActivePetSkinId;
		public string Name;
		public string Rarity;
		public string Family;
		public int Place;
		public PetAbilityData[] PetAbilities;
		public int MaxLevel; 
	}

	public struct PetAbilityData
	{
		public bool IsUnlocked; 
		public string AbilityName;
		public int Level;
		public bool IsMaxed;
	}
}