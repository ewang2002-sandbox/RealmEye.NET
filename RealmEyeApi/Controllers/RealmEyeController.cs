using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealmEyeNET.Definition;
using RealmEyeNET.Scraper;

namespace RealmEyeApi.Controllers
{
	[ApiController]
	[Route("api/realmeye")]
	public class RealmEyeController : Controller
	{
		private readonly ILogger<RealmEyeController> _logger;

		public RealmEyeController(ILogger<RealmEyeController> logger)
		{
			_logger = logger;
		}

		[HttpGet("basics/{name}")]
		public JsonResult GetBasicData(string name)
		{
			var profile = new PlayerScraper(name)
				.ScrapePlayerProfile();
			return new JsonResult(profile);
		}

		[HttpGet("char/{name}")]
		public JsonResult GetCharacterData(string name)
		{
			var characterData = new PlayerScraper(name)
				.ScrapCharacterInformation();
			return new JsonResult(characterData);
		}

		[HttpGet("pets/{name}")]
		public JsonResult GetPetYard(string name)
		{
			var pets = new PlayerScraper(name)
				.ScrapPetYard();
			return new JsonResult(pets);
		}

		[HttpGet("graveyard/{name}/{amount?}")]
		public JsonResult GetGraveyard(string name, int amount = -1)
		{
			var gy = new PlayerScraper(name)
				.ScrapGraveyard(amount);
			return new JsonResult(gy);
		}

		[HttpGet("graveyardsummary/{name}")]
		public JsonResult GetGraveyardSummary(string name)
		{
			var gys = new PlayerScraper(name)
				.ScrapeGraveyardSummary();
			return new JsonResult(gys);
		}

		public JsonResult GetNameHistory(string name)
		{
			var nh = new PlayerScraper(name)
				.ScrapeNameHistory();
			return new JsonResult(nh);
		}

		public JsonResult GetGuildHistory(string name)
		{
			var gh = new PlayerScraper(name)
				.ScrapGuildHistory();
			return new JsonResult(gh);
		}
		public JsonResult GetRankHistory(string name)
		{
			var rh = new PlayerScraper(name)
				.ScrapeRankHistory();
			return new JsonResult(rh);
		}
	}
}
