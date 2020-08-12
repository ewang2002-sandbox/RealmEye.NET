using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}