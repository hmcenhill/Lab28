using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab28.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Lab28.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;
        private readonly IHttpClientFactory _httpClientFactory;


        public HomeController(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _httpClientFactory = httpClientFactory;
        }


        public IActionResult Index(Helper helper)
        {
            return View(helper);
        }

        public async Task<IActionResult> NewDeck()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com");

            var response = await client.GetAsync("api/deck/new/shuffle/");
            var content = await response.Content.ReadAsAsync<Deck>();

            if (content.Success)
            {
                var helper = new Helper();
                helper.IsDeck = true;
                helper.Remaining = content.Remaining;
                _session.SetString("Deck", content.Deck_id);
                _session.SetInt32("Remaining", content.Remaining);

                return View("Index", helper);
            }
            return NotFound();

        }

        public async Task<IActionResult> Draw(int drawCount = 5)
        {
            if (_session.GetString("Deck") != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://deckofcardsapi.com");

                var responseText = "https://deckofcardsapi.com/api/deck/" + _session.GetString("Deck") + "/draw/?count=" + drawCount.ToString();

                var response = await client.GetAsync(responseText);
                var content = await response.Content.ReadAsAsync<Draw>();

                if (content.Success)
                {
                    var helper = new Helper();
                    helper.IsDeck = true;
                    helper.Remaining = content.Remaining;
                    _session.SetString("Deck", content.Deck_id);
                    _session.SetInt32("Remaining", content.Remaining);

                    helper.Cards = content.Cards.ToList<Card>();

                    if(helper.Cards.Count > 0)
                    {
                        helper.AreDraws = true;
                    }

                    return View("Index", helper);
                }
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
