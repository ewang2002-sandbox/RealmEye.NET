using System;
using System.Linq;
using System.Runtime.Caching.Hosting;
using RealmEyeNET.Scraper;

namespace RealmEyeTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var p = new PlayerScraper("").ScrapeGraveyardSummary();
		}
	}
}