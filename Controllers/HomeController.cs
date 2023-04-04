using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_lib.Library.Services;
namespace mtg_app.Controllers;

[Route("")]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    CardService cardService = new CardService();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("")]
    [Route("[action]")]
    [Route("/{index}")]
    public IActionResult Index(int index)
    {
        return View(new HomeViewModel
        {
            Cards = cardService.GetCardsFromIndex(index).Select(
                c => new CardViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    OriginalImageUrl = c.OriginalImageUrl,
                    OriginalText = c.OriginalText,
                    RarityCode = c.RarityCode,
                    Price = Math.Round(1.24 * float.Parse(c.ConvertedManaCost)+2.83,2)
                })
                .ToList(),
            Index = index,
            Pages = cardService.GetPagesAmount()
            
        });
    }
    
    [Route("[action]")]
    [HttpPost]
    public ActionResult FormPagination(int index)
    {
        return View("Index", new HomeViewModel
        {
            Cards = cardService.GetCardsFromIndex(index).Select(
                    c => new CardViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        OriginalImageUrl = c.OriginalImageUrl,
                        OriginalText = c.OriginalText,
                        RarityCode = c.RarityCode,
                        Price = Math.Round(1.24 * float.Parse(c.ConvertedManaCost)+2.83,2)
                    })
                .ToList(),
            Index = index,
            Pages = cardService.GetPagesAmount()
        });
    }

    [Route("[action]")]
    [HttpPost]
    public ActionResult FormSearch(String name)
    {
        return View("Index", new HomeViewModel
        {
            Cards = cardService.GetCardsFromName(name).Select(
                c => new CardViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    OriginalImageUrl = c.OriginalImageUrl,
                    OriginalText = c.OriginalText,
                    RarityCode = c.RarityCode,
                    Price = Math.Round(1.24 * float.Parse(c.ConvertedManaCost)+2.83,2)
                })
                .ToList(),
            Index = 0,
            Pages = 0
        });
    }
    
    [Route("[action]")]
    [HttpPost]
    public IActionResult FilterManaCostDirection(String direction)
    {
        return View("Index",new HomeViewModel
        {
            Cards = cardService.GetCardsFromManaCostDirection(direction).Select(
                    c => new CardViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        OriginalImageUrl = c.OriginalImageUrl,
                        OriginalText = c.OriginalText,
                        RarityCode = c.RarityCode,
                        Price = Math.Round(1.24 * float.Parse(c.ConvertedManaCost)+2.83,2)
                    })
                .ToList(),
            Index = 0,
            Pages = cardService.GetPagesAmount()
            
        });
    }
    [Route("[action]")]
    [HttpPost]
    public IActionResult FilterManaCost(String manacost)
    {
        return View("Index",new HomeViewModel
        {
            Cards = cardService.GetCardsFromManaCost(manacost).Select(
                    c => new CardViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        OriginalImageUrl = c.OriginalImageUrl,
                        OriginalText = c.OriginalText,
                        RarityCode = c.RarityCode,
                        Price = Math.Round(1.24 * float.Parse(c.ConvertedManaCost)+2.83,2)
                    })
                .ToList(),
            Index = 0,
            Pages = 0
            
        });
    }

    [Route("[action]")]
    public IActionResult Privacy()
    {
        return View();
    }
    
    
    [Route("[action]")]
    [Route("/Home/Details/{id}")]
    public IActionResult Details(int id)
    {
        return View(cardService.GetCardById(id));
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    
    [Route("[action]")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
