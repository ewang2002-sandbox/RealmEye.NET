using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RealmEyeNET.Definition;
using ScrapySharp.Extensions;
using static RealmEyeNET.Constants.RealmEyeUrl;

namespace RealmEyeNET.Scraper
{
	public partial class PlayerScraper
	{
		/// <summary>
		/// Scraps the graveyard.
		/// </summary>
		/// <param name="limit">The maximum number of dead characters to get. By default, all graveyard entries are extracted.</param>
		/// <returns>The graveyard.</returns>
		public GraveyardData ScrapGraveyard(int limit = -1)
		{
			var page = Browser.NavigateToPage(new Uri($"{GraveyardUrl}/{PlayerName}"));

			var colMd = page.Html.CssSelect(".col-md-12").First();
			if (colMd == null)
				return new GraveyardData
				{
					IsPrivate = true,
					NoDataAvailable = false,
					GraveyardCount = -1,
					Graveyard = new List<GraveyardEntry>()
				};

			// this probably isnt the best way
			// to do it.
			var gyInfoPara = colMd.SelectSingleNode("//div[@class='col-md-12']/p/text()");
			var gyInfoHead = colMd.SelectSingleNode("//div[@class='col-md-12']/h3/text()");

			if (gyInfoHead != null && gyInfoHead.InnerText == "No data available yet.")
				return new GraveyardData
				{
					Graveyard = new List<GraveyardEntry>(),
					IsPrivate = false,
					GraveyardCount = -1,
					NoDataAvailable = true
				};

			var numGraveyards = 0;
			if (gyInfoPara != null && !gyInfoPara.InnerText.Contains("We haven"))
				numGraveyards = int.Parse(gyInfoPara.InnerText.Split("graves")[0]
					.Split("found")[1]
					.Replace(",", "")
					.Trim());

			// no graveyard data
			if (numGraveyards == 0)
				return new GraveyardData
				{
					IsPrivate = false,
					GraveyardCount = -1,
					NoDataAvailable = true,
					Graveyard = new List<GraveyardEntry>()
				};

			var lowestPossibleAmt = Math.Floor((double)numGraveyards / 100) * 100;
			if (limit != -1 && limit <= numGraveyards)
				lowestPossibleAmt = Math.Floor((double)limit / 100) * 100;

			var returnData = new GraveyardData
			{
				NoDataAvailable = false,
				GraveyardCount = numGraveyards,
				IsPrivate = false,
				Graveyard = new List<GraveyardEntry>()
			};

			// iterate over each page in the damn website :( 
			for (int index = 1; index <= lowestPossibleAmt + 1; index += 100)
			{
				if (index != 1)
					page = Browser.NavigateToPage(new Uri($"{GraveyardUrl}/{PlayerName}/{index}"));

				var graveyardTable = page.Html
					.CssSelect(".table-responsive")
					.CssSelect(".table")
					.First()
					// <tbody><tr>
					.SelectNodes("tbody/tr");

				// td[1] => date
				// td[2] => garbage
				// td[3] => class name
				// td[4] => level
				// td[5] => base fame
				// td[6] => total fame 
				// td[7] => exp
				// td[8] => items
				// td[9] => stats
				// td[10] => died to
				foreach (var gyRow in graveyardTable)
				{
					var diedOn = gyRow.SelectSingleNode("td[1]").InnerText;
					var character = gyRow.SelectSingleNode("td[3]").InnerText;
					var level = int.Parse(gyRow.SelectSingleNode("td[4]").InnerText);
					var baseFame = int.Parse(gyRow.SelectSingleNode("td[5]").InnerText);
					var totalFame = int.Parse(gyRow.SelectSingleNode("td[6]").FirstChild.InnerText);
					var exp = long.Parse(gyRow.SelectSingleNode("td[7]").InnerText);

					IList<string> characterEquipment = new List<string>();
					var equips = gyRow
						.SelectSingleNode("td[8]")
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

					var hadBackpack = equips.Count == 5;
					var statsMaxed = int.Parse(gyRow.SelectSingleNode("td[9]").FirstChild.InnerText.Split('/')[0]);
					var diedTo = gyRow.SelectSingleNode("td[10]").InnerText;

					returnData.Graveyard.Add(new GraveyardEntry
					{
						DiedOn = diedOn,
						BaseFame = baseFame,
						Character = character,
						Equipment = characterEquipment.ToArray(),
						Experience = exp,
						HadBackpack = hadBackpack,
						KilledBy = diedTo,
						Level = level,
						MaxedStats = statsMaxed,
						TotalFame = totalFame
					});
				}
			}

			if (limit != -1)
				returnData.Graveyard = returnData.Graveyard.Take(limit).ToArray();

			return returnData;
		}

		/// <summary>
		/// Scrapes a player's graveyard summary.
		/// </summary>
		/// <returns>The player's graveyard summary.</returns>
		public GraveyardSummaryData ScrapeGraveyardSummary()
		{
			var page = Browser.NavigateToPage(new Uri($"{GraveyardSummaryUrl}/{PlayerName}"));

			var colMd = page.Html.CssSelect(".col-md-12").First();
			if (colMd == null)
				return new GraveyardSummaryData
				{
					IsPrivate = true,
					Properties = new GraveyardSummaryProperty[0],
					StatsCharacters = new MaxedStatsByCharacters[0],
					TechnicalProperties = new GraveyardTechnicalProperty[0]
				};

			// this probably isnt the best way
			// to do it.
			var gyInfoHead = colMd.SelectSingleNode("//div[@class='col-md-12']/h3/text()");

			if (gyInfoHead != null && gyInfoHead.InnerText == "No data available yet.")
				return new GraveyardSummaryData
				{
					IsPrivate = false,
					Properties = new GraveyardSummaryProperty[0],
					StatsCharacters = new MaxedStatsByCharacters[0],
					TechnicalProperties = new GraveyardTechnicalProperty[0]
				};

			var returnData = new GraveyardSummaryData
			{
				IsPrivate = false
			};

			var firstSummaryTable = page.Html
				.CssSelect("#e")
				.First()
				// <tbody><tr>
				.SelectNodes("tr");

			// td[1] => random
			// td[2] => name
			// td[3] => total
			// td[4] => max
			// td[5] => avg
			// td[6] => min
			var props = firstSummaryTable.Select(row => new GraveyardSummaryProperty
			{
				Achievement = row.SelectSingleNode("td[2]").InnerText,
				Total = long.Parse(row.SelectSingleNode("td[3]").InnerText),
				Max = long.Parse(row.SelectSingleNode("td[4]").InnerText),
				Average = double.Parse(row.SelectSingleNode("td[5]").InnerText),
				Min = long.Parse(row.SelectSingleNode("td[6]").InnerText)
			}).ToArray();

			var secondSummaryTable = page.Html
				.CssSelect("#f")
				.First()
				.SelectNodes("tr");

			// td[1] => name
			// td[2] => total
			// td[3] => max
			// td[4] => avg
			// td[5] => min
			var techProps = secondSummaryTable.Select(row => new GraveyardTechnicalProperty
			{
				Achievement = row.SelectSingleNode("td[1]").InnerText,
				Total = row.SelectSingleNode("td[2]").InnerText,
				Max = row.SelectSingleNode("td[3]").InnerText,
				Average = row.SelectSingleNode("td[4]").InnerText,
				Min = row.SelectSingleNode("td[5]").InnerText
			}).ToArray();

			returnData.Properties = props.ToArray();
			returnData.TechnicalProperties = techProps.ToArray();

			// character info
			var thirdSummaryTable = page.Html
				.CssSelect("#g")
				.First()
				.SelectNodes("tbody/tr");

			// td[1] => class
			// td[2] => 0/8
			// td[3] => 1/8
			// ...
			// td[n] = (n - 2)/8
			// n <= 10
			// td[last] = td[11] = total
			var charInfo = thirdSummaryTable.Select(row => new MaxedStatsByCharacters
			{
				CharacterType = row.SelectSingleNode("td[1]").InnerText,
				Stats = new[]
				{
					row.SelectSingleNode("td[2]").InnerText == string.Empty
						? 0
						: int.Parse(row.SelectSingleNode("td[2]").InnerText),
					row.SelectSingleNode("td[3]").InnerText == string.Empty
						? 0
						: int.Parse(row.SelectSingleNode("td[3]").InnerText),
					row.SelectSingleNode("td[4]").InnerText == string.Empty
						? 0
						: int.Parse(row.SelectSingleNode("td[4]").InnerText),
					row.SelectSingleNode("td[5]").InnerText == string.Empty
						? 0
						: int.Parse(row.SelectSingleNode("td[5]").InnerText),
					row.SelectSingleNode("td[6]").InnerText == string.Empty
						? 0
						: int.Parse(row.SelectSingleNode("td[6]").InnerText),
					row.SelectSingleNode("td[7]").InnerText == string.Empty
						? 0
						: int.Parse(row.SelectSingleNode("td[7]").InnerText),
					row.SelectSingleNode("td[8]").InnerText == string.Empty
						? 0
						: int.Parse(row.SelectSingleNode("td[8]").InnerText),
					row.SelectSingleNode("td[9]").InnerText == string.Empty
						? 0
						: int.Parse(row.SelectSingleNode("td[9]").InnerText),
					row.SelectSingleNode("td[10]").InnerText == string.Empty
						? 0
						: int.Parse(row.SelectSingleNode("td[10]").InnerText),

				},
				Total = int.Parse(row.SelectSingleNode("td[11]").InnerText)
			}).ToArray();

			returnData.StatsCharacters = charInfo;

			return returnData;
		}
	}
}
