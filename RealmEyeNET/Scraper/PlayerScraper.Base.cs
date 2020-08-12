// NOTE: everything is relative to OuterHtml, NOT InnerHtml 
// CONNECTION_ERROR
// PRIVATE_PROFILE
// SUCCESS
// CHARACTERS_PRIVATE
// PETS_PRIVATE
// NO_PETS
// NO_DATA_AVAILABLE
// NAME_HISTORY_PRIVATE
// GUILD_HISTORY_PRIVATE
// RANK_HISTORY_PRIVATE

using System;
using System.Linq;
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

		/// <summary>
		/// The player's name. 
		/// </summary>
		public string PlayerName { get; }

		/// <summary>
		/// Creates a new PlayerScrapper object. This object will give you the ability to scrape various parts of a RealmEye profile.
		/// </summary>
		/// <param name="name">The in-game name to look for.</param>
		public PlayerScraper(string name)
		{
			PlayerName = name;
		}

		/// <summary>
		/// Determines if the profile is private. 
		/// </summary>
		/// <returns>True if the profile is private; false otherwise.</returns>
		public bool ProfileIsPrivate()
		{
			var page = Browser.NavigateToPage(new Uri($"{PlayerUrl}/{PlayerName}"));
			var mainElement = page.Html.CssSelect(".col-md-12");

			return mainElement.CssSelect(".player-not-found").Count() != 0;
		}
	}
}