using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using RealmEyeNET.Definition;
using ScrapySharp.Extensions;
using static RealmEyeNET.Constants.RealmEyeUrl;

namespace RealmEyeNET.Scraper
{
	public partial class PlayerScraper
	{
		/// <summary>
		/// Scrapes name history information.
		/// </summary>
		/// <returns>Name history information.</returns>
		public NameHistoryData ScrapeNameHistory()
		{
			var page = Browser.NavigateToPage(new Uri($"{NameHistoryUrl}/{PlayerName}"));

			var returnData = new NameHistoryData
			{
				IsPrivate = false,
				NameHistory = new List<NameHistoryEntry>()
			};

			var colMd = page.Html.CssSelect(".col-md-12").First();
			var nameHistExists = colMd.SelectNodes("//div[@class='col-md-12']/p/text()");
			if (nameHistExists.Count == 2 && nameHistExists.Last().InnerText.Contains("No name changes detected."))
				return returnData;

			var hiddenTxtHeader = colMd.SelectSingleNode("//div[@class='col-md-12']/h3/text()");
			if (hiddenTxtHeader != null && hiddenTxtHeader.InnerText.Contains("Name history is hidden"))
			{
				returnData.IsPrivate = true;
				return returnData;
			}

			var nameHistoryColl = page.Html
				.CssSelect(".table-responsive")
				.CssSelect(".table")
				.First()
				// <tbody><tr>
				.SelectNodes("tbody/tr");

			// td[1] => name
			// td[2] => from
			// td[3] => to
			foreach (var nameHistoryEntry in nameHistoryColl)
			{
				returnData.NameHistory.Add(new NameHistoryEntry
				{
					Name = nameHistoryEntry.SelectSingleNode("td[1]").InnerText,
					From = nameHistoryEntry.SelectSingleNode("td[2]").InnerText,
					To = nameHistoryEntry.SelectSingleNode("td[3]").InnerText
				});
			}

			return returnData;
		}

		/// <summary>
		/// Scrapes rank history.
		/// </summary>
		/// <returns>The rank history.</returns>
		public RankHistoryData ScrapeRankHistory()
		{
			var page = Browser.NavigateToPage(new Uri($"{RankHistoryUrl}/{PlayerName}"));

			var returnData = new RankHistoryData
			{
				IsPrivate = false,
				RankHistory = new List<RankHistoryEntry>()
			};

			var colMd = page.Html.CssSelect(".col-md-12").First();

			var hiddenTxtHeader = colMd.SelectSingleNode("//div[@class='col-md-12']/h3/text()");
			if (hiddenTxtHeader != null && hiddenTxtHeader.InnerText.Contains("Rank history is hidden"))
			{
				returnData.IsPrivate = true;
				return returnData;
			}

			var rankHistoryColl = page.Html
				.CssSelect(".table-responsive")
				.CssSelect(".table")
				.First()
				// <tbody><tr>
				.SelectNodes("tbody/tr");

			// td[1] => rank
			// td[2] => achieved
			foreach (var rankHistEntry in rankHistoryColl)
			{
				int rank = int.Parse(rankHistEntry.SelectSingleNode("td[1]").FirstChild.InnerText);
				string since = rankHistEntry.SelectSingleNode("td[2]").FirstChild.InnerText;
				string date = rankHistEntry.SelectSingleNode("td[2]").FirstChild.Attributes["title"].Value;
				returnData.RankHistory.Add(new RankHistoryEntry
				{
					Achieved = since,
					Date = date,
					Rank = rank
				});
			}

			return returnData;
		}

		/// <summary>
		/// Scrapes the guild history.
		/// </summary>
		/// <returns>The guild history.</returns>
		public GuildHistoryData ScrapGuildHistory()
		{
			var page = Browser.NavigateToPage(new Uri($"{NameHistoryUrl}/{PlayerName}"));

			var returnData = new GuildHistoryData
			{
				IsPrivate = false,
				GuildHistory = new List<GuildHistoryEntry>()
			};

			var colMd = page.Html.CssSelect(".col-md-12").First();
			var guildHistExists = colMd.SelectNodes("//div[@class='col-md-12']/p/text()");
			if (guildHistExists.Count == 2 && guildHistExists.Last().InnerText.Contains("No guild changes detected."))
				return returnData;

			var hiddenTxtHeader = colMd.SelectSingleNode("//div[@class='col-md-12']/h3/text()");
			if (hiddenTxtHeader != null && hiddenTxtHeader.InnerText.Contains("Guild history is hidden"))
			{
				returnData.IsPrivate = true;
				return returnData;
			}

			var guildHistoryColl = page.Html
				.CssSelect(".table-responsive")
				.CssSelect(".table")
				.First()
				// <tbody><tr>
				.SelectNodes("tbody/tr");

			// td[1] => guild name
			// td[2] => rank
			// td[3] => from
			// td[4] => to
			foreach (var guildHistoryRow in guildHistoryColl)
			{
				returnData.GuildHistory.Add(new GuildHistoryEntry
				{
					GuildName = guildHistoryRow.SelectSingleNode("td[1]").FirstChild.Name,
					GuildRank = guildHistoryRow.SelectSingleNode("td[2]").InnerText,
					From = guildHistoryRow.SelectSingleNode("td[3]").InnerText,
					To = guildHistoryRow.SelectSingleNode("td[4]").InnerText
				});
			}

			return returnData;
		}
	}
}