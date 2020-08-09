using System;
using RealmEyeNET.Scraper;

namespace RealmEyeTest
{
	class Program
	{
		static void Main(string[] args)
		{
			new PlayerScraper("consolemc")
				.ScrapCharacterInformation();
		}
	}
}
