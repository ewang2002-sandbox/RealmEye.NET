using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

		public IList<CharacterData> ScrapCharacterInformation()
		{
			var charList = new List<CharacterData>();

			var page = Browser.NavigateToPage(new Uri($"{PlayerUrl}/{PlayerName}"));
			var summaryTable = page.Html.CssSelect(".summary").First();

			bool isPrivate = true;
			foreach (var row in summaryTable.SelectNodes("tr"))
			{
				foreach (var col in row.SelectNodes("td[1]"))
				{
					if (col.InnerText == "Characters")
						isPrivate = false;
				}
			}

			if (isPrivate)
				return charList;


		}
	}
}