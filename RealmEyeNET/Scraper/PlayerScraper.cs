// NOTE: everything is relative to OuterHtml, NOT InnerHtml 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using RealmEyeNET.Definition;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using static RealmEyeNET.Constants.RealmEyeUrl;

namespace RealmEyeNET.Scraper
{
	public partial class PlayerScraper
	{
		public static ScrapingBrowser Browser = new ScrapingBrowser
		{
			AllowAutoRedirect = true,
			AllowMetaRedirect = true
		};

		public string PlayerName { get; }

		public PlayerScraper(string name)
		{
			PlayerName = name;
		}

		/// <summary>
		/// Scrapes a player's profile. You'll get the basics like character count, fame, experience, description, and more.
		/// </summary>
		/// <returns>The player's basic profile.</returns>
		public PlayerData ScrapePlayerProfile()
		{
			var page = Browser.NavigateToPage(new Uri($"{PlayerUrl}/{PlayerName}"));
			if (page == null)
			{
				return new PlayerData
				{
					IsHiddenOrErrored = true
				};
			}

			var mainElement = page.Html.CssSelect(".col-md-12");

			var htmlNodes = mainElement as HtmlNode[] ?? mainElement.ToArray();
			if (htmlNodes.CssSelect(".player-not-found").Count() != 0)
			{
				return new PlayerData
				{
					IsHiddenOrErrored = true
				};
			}

			// profile public
			// scrap time
			var returnData = new PlayerData
			{
				Name = page.Html.CssSelect(".entity-name").First().InnerText
			};

			var summaryTable = page.Html.CssSelect(".summary").First();
			// <tr> 
			foreach (var row in summaryTable.SelectNodes("tr"))
			{
				// td[1] is the first column (property name) -- e.g. "Skins"
				// td[2] is the second column (property value) -- e.g. "58 (6349th)
				foreach (var col in row.SelectNodes("td[1]"))
				{
					if (col.InnerText == "Characters")
						returnData.CharacterCount = int.Parse(col.NextSibling.InnerText);
					else if (col.InnerText == "Skins")
						returnData.Skins = int.Parse(col.NextSibling.InnerText.Split('(')[0]);
					else if (col.InnerText == "Fame")
						returnData.Fame = int.Parse(col.NextSibling.InnerText.Split('(')[0]);
					else if (col.InnerText == "Exp")
						returnData.Exp = int.Parse(col.NextSibling.InnerText.Split('(')[0]);
					else if (col.InnerText == "Rank")
						returnData.Rank = int.Parse(col.NextSibling.InnerText);
					else if (col.InnerText == "Account fame")
						returnData.AccountFame = int.Parse(col.NextSibling.InnerText.Split('(')[0]);
					else if (col.InnerText == "Guild")
						returnData.Guild = col.NextSibling.InnerText;
					else if (col.InnerText == "Guild Rank")
						returnData.GuildRank = col.NextSibling.InnerText;
					else if (col.InnerText == "First seen")
						returnData.FirstSeen = col.NextSibling.InnerText;
					else if (col.InnerText == "Last seen")
						returnData.LastSeen = col.NextSibling.InnerText;
					else if (col.InnerText == "Created")
						returnData.Created = col.NextSibling.InnerText;
				}
			}

			returnData.Description = new string[3];
			// #d is id = "d" (in html)
			var descriptionTable = htmlNodes.CssSelect("#d").First();
			// TODO account for no description 
			returnData.Description[0] = descriptionTable.FirstChild.InnerText;
			returnData.Description[1] = descriptionTable.FirstChild.NextSibling.InnerText;
			returnData.Description[2] = descriptionTable.FirstChild.NextSibling.NextSibling.InnerText;

			return returnData;
		}

		/// <summary>
		/// Scrapes character information.
		/// </summary>
		/// <returns>Character data.</returns>
		public CharacterData ScrapCharacterInformation()
		{
			var data = new CharacterData
			{
				IsPrivate = true,
				Characters = new List<CharacterEntry>()
			};

			var page = Browser.NavigateToPage(new Uri($"{PlayerUrl}/{PlayerName}"));
			var summaryTable = page.Html.CssSelect(".summary").First();

			foreach (var row in summaryTable.SelectNodes("tr"))
			{
				foreach (var col in row.SelectNodes("td[1]"))
				{
					if (col.InnerText != "Characters")
						continue;
					data.IsPrivate = false;
					if (int.TryParse(col.NextSibling.InnerText, out var result))
						if (result == 0)
							return data;
				}
			}

			if (data.IsPrivate)
				return data;

			var charTable = page.Html
				.CssSelect("#e")
				.First()
				// <tbody><tr>
				.SelectNodes("tbody/tr");

			// td[3] => character type
			// td[4] => level
			// td[5] => cqc
			// td[6] => fame
			// td[7] => exp
			// td[8] => place
			// td[9] => equipment
			// td[10] => stats
			foreach (var characterRow in charTable)
			{
				var characterType = characterRow.SelectSingleNode("td[3]").InnerText;
				var level = int.Parse(characterRow.SelectSingleNode("td[4]").InnerText);
				var cqc = int.Parse(characterRow.SelectSingleNode("td[5]").InnerText.Split('/')[0]);
				var fame = int.Parse(characterRow.SelectSingleNode("td[6]").InnerText);
				var exp = long.Parse(characterRow.SelectSingleNode("td[7]").InnerText);
				var place = int.Parse(characterRow.SelectSingleNode("td[8]").InnerText);

				IList<string> characterEquipment = new List<string>();
				var equips = characterRow
					.SelectSingleNode("td[9]")
					// <span class="item-wrapper">...
					.ChildNodes;
				for (int i = 0; i < 4; i++)
				{
					// equips[i] -> everything inside <span class="item-wrapper">
					var itemContainer = equips[i].ChildNodes[0];
					characterEquipment.Add(itemContainer.ChildNodes.Count == 0
						? "Empty Slot"
						: WebUtility.HtmlDecode(itemContainer.ChildNodes[0].Attributes["title"].Value));
				}

				// <span class = "player-stats" ...
				var stats = characterRow
					.SelectSingleNode("td[10]");
				var maxedStats = int.Parse(stats.InnerText.Split('/')[0]);
				var dataStats = stats.FirstChild
					.Attributes["data-stats"]
					.Value[1..^1]
					.Split(',')
					.Select(int.Parse)
					.ToArray();
				var bonusStats = stats.FirstChild
					.Attributes["data-bonuses"]
					.Value[1..^1]
					.Split(',')
					.Select(int.Parse)
					.ToArray();

				data.Characters.Add(new CharacterEntry
				{
					CharacterType = characterType,
					ClassQuestsCompleted = cqc,
					EquipmentData = characterEquipment.ToArray(),
					Experience = exp,
					Fame = fame,
					HasBackpack = equips.Count == 5,
					Level = level,
					Place = place,
					StatsMaxed = maxedStats,
					Stats = new Stats
					{
						Health = dataStats[0] - bonusStats[0],
						Magic = dataStats[1] - bonusStats[1],
						Attack = dataStats[2] - bonusStats[2],
						Defense = dataStats[3] - bonusStats[3],
						Speed = dataStats[4] - bonusStats[4],
						Vitality = dataStats[5] - bonusStats[5],
						Wisdom = dataStats[6] - bonusStats[6],
						Dexterity = dataStats[7] - bonusStats[7]
					}
				});
			}

			// TODO get last seen location for characters
			return data;
		}

		/// <summary>
		/// Scrapes pet yard data.
		/// </summary>
		/// <returns>The player's pets.</returns>
		public PetYardData ScrapPetYard()
		{
			var returnData = new PetYardData
			{
				IsPrivate = false,
				Pets = new List<PetEntry>()
			};

			var page = Browser.NavigateToPage(new Uri($"{PetYardUrl}/{PlayerName}"));
			// figure this out
			var mainElem = page.Html.CssSelect(".col-md-12").First();
			if (mainElem == null)
			{
				returnData.IsPrivate = true;
				return returnData;
			}

			var petsPrivateTag = mainElem.SelectSingleNode("//div[@class='col-md-12']/h3");
			if (petsPrivateTag != null)
			{
				if (petsPrivateTag.InnerText == "Pets are hidden.")
				{
					returnData.IsPrivate = true;
					return returnData;
				}

				if (petsPrivateTag.InnerText.Contains("has no pets."))
					return returnData;
			}


			var petTable = page.Html
				.CssSelect("#e")
				.First()
				// <tbody><tr>
				.SelectNodes("tbody/tr");

			// td[1] => span class, data-item
			// td[2] => name of pet
			// td[3] => rarity 
			// td[4] => family
			// depends on locked/unlocked
			// td[5] => first ability 
			// td[6] => first ability stats
			// td[7] => second ability
			// td[7] => second ability stats
			// td[8] => third ability 
			foreach (var petRow in petTable)
			{
				var maxLevel = int.Parse(petRow.SelectNodes("td")
					.Last()
					.InnerText);
				var petId = int.Parse(petRow.SelectSingleNode("td[1]")
					.FirstChild
					.Attributes["data-item"]
					.Value);
				var petName = petRow.SelectSingleNode("td[2]")
					.InnerText;
				var rarity = petRow.SelectSingleNode("td[3]")
					.InnerText;
				var family = petRow.SelectSingleNode("td[4]")
					.InnerText;
				var rank = petRow.SelectSingleNode("td[5]").InnerText == string.Empty
					? -1
					: int.Parse(petRow.SelectSingleNode("td[5]")
						.InnerText[..^2]);
				// td[6] start of ability 
				// will always be unlocked
				IList<PetAbilityData> petAbility = new List<PetAbilityData>();
				var firstAbilityName = petRow.SelectSingleNode("td[6]")
					.FirstChild
					.InnerText;
				var firstAbilityLevel = int.Parse(petRow.SelectSingleNode("td[7]")
					.FirstChild
					.InnerText);
				petAbility.Add(new PetAbilityData
				{
					AbilityName = firstAbilityName,
					IsMaxed = firstAbilityLevel == maxLevel,
					Level = firstAbilityLevel,
					IsUnlocked = true
				});

				// get 2nd ability + 
				for (int i = 8; i < petRow.SelectNodes("td").Count; i++)
				{
					var ability = petRow.SelectSingleNode($"td[{i}]");
					if (ability.InnerText == string.Empty)
						continue;

					if (!ability.CssSelect(".pet-ability-disabled").Any())
					{
						// ability exists and is unlocked!
						var nameOfAbility = ability.FirstChild.InnerText;
						++i;
						var abilityLvl = int.Parse(petRow.SelectSingleNode($"td[{i}]")
							.FirstChild
							.InnerText);
						petAbility.Add(new PetAbilityData
						{
							AbilityName = nameOfAbility,
							IsMaxed = abilityLvl == maxLevel,
							Level = abilityLvl,
							IsUnlocked = true
						});
					}
					else
					{
						// locked ability
						var nameOfAbility = ability.FirstChild.InnerText;
						petAbility.Add(new PetAbilityData
						{
							AbilityName = nameOfAbility,
							IsMaxed = false,
							Level = -1,
							IsUnlocked = false
						});
					}
				}

				returnData.Pets.Add(new PetEntry
				{
					ActivePetSkinId = petId,
					Family = family,
					MaxLevel = maxLevel,
					Name = petName,
					PetAbilities = petAbility,
					Place = rank,
					Rarity = rarity
				});
			}

			return returnData;
		}
	}
}