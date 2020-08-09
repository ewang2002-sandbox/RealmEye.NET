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
		public string ActivePetSkin;
		public string Name;
		public string Rarity;
		public string Family;
		public int Place;
		public PetAbilityData[] PetAbilities;
	}

	public struct PetAbilityData
	{
		public string AbilityName;
		public int Level;
		public bool IsMaxed; 
	}
}