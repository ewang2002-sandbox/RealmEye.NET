// NOTE: everything is relative to OuterHtml, NOT InnerHtml 

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using RealmEyeNET.Definition;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using static RealmEyeNET.Constants.RealmEyeUrl;

namespace RealmEyeNET.Scraper
{
	public class PlayerScraper
	{
		public static ScrapingBrowser Browser = new ScrapingBrowser();
		public string PlayerName { get; }

		public PlayerScraper(string name)
		{
			PlayerName = name;
			Browser = new ScrapingBrowser
			{
				AllowAutoRedirect = true,
				AllowMetaRedirect = true
			};
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
			returnData.Description[0] = descriptionTable.FirstChild.InnerText;
			returnData.Description[1] = descriptionTable.FirstChild.NextSibling.InnerText;
			returnData.Description[2] = descriptionTable.FirstChild.NextSibling.NextSibling.InnerText;

			// set defaults

			return returnData;
		}

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
				string characterType = characterRow.SelectSingleNode("td[3]").InnerText;
				int level = int.Parse(characterRow.SelectSingleNode("td[4]").InnerText);
				int cqc = int.Parse(characterRow.SelectSingleNode("td[5]").InnerText.Split('/')[0]);
				int fame = int.Parse(characterRow.SelectSingleNode("td[6]").InnerText);
				long exp = long.Parse(characterRow.SelectSingleNode("td[7]").InnerText);
				int place = int.Parse(characterRow.SelectSingleNode("td[8]").InnerText);

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
				int maxedStats = int.Parse(stats.InnerText.Split('/')[0]);
				int[] dataStats = stats.FirstChild
					.Attributes["data-stats"]
					.Value[1..^1]
					.Split(',')
					.Select(int.Parse)
					.ToArray();
				int[] bonusStats = stats.FirstChild
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

			return data;
		}
	}
}