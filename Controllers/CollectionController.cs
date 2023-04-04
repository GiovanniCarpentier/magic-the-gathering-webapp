using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using mtg_app.Models;
using mtg_lib.Library.Models;
using mtg_lib.Library.Services;


namespace mtg_app.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ILogger<CollectionController> _logger;
        private CardService cardService = new CardService();

        private static CollectionService collectionService = new CollectionService();
        private readonly UserManager<IdentityUser> _userManager;

        public CollectionController(ILogger<CollectionController> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }


        [Authorize]
        [HttpGet("[controller]")]
        [HttpGet("[controller]/{index}")]
        public IActionResult Index(int index)
        {
            String userId = _userManager.GetUserId(this.User);
            return View(new CollectionViewModel
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
            Pages = cardService.GetPagesAmount(),
            Collection = collectionService.getCollectionFromId(userId)
        });
        }

    //api
    [Authorize]
    [HttpGet("[controller]/card/{index}")]
    public IActionResult SelectCard(int index){
        String userId = _userManager.GetUserId(this.User);

        Card selectedCard = cardService.GetCardById(index); 

        if(collectionService.collectionContains(userId, selectedCard)){
            collectionService.removeFromCollection(userId, selectedCard);
            return Content($"removed card");
        }else{
            collectionService.addToCollection(userId, selectedCard);
            return Content($"added card");
        }
    }

    [Authorize]
    [HttpPost("[controller]/FormSearch")]
    public ActionResult FormSearch(String name)
    {
        String userId = _userManager.GetUserId(this.User);
        return View("Index", new CollectionViewModel
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
            Pages = 0,
            Collection = collectionService.getCollectionFromId(userId)
        });
    }

    [Authorize]
    [HttpPost("[controller]/FilterManaCostDirection")]
    public IActionResult FilterManaCostDirection(String direction)
    {
        String userId = _userManager.GetUserId(this.User);
        return View("Index",new CollectionViewModel
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
            Pages = cardService.GetPagesAmount(),
            Collection = collectionService.getCollectionFromId(userId)
            
        });
    }
    
    [Authorize]
    [HttpPost("[controller]/FilterManaCost")]
    public IActionResult FilterManaCost(String manacost)
    {
        String userId = _userManager.GetUserId(this.User);
        return View("Index",new CollectionViewModel
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
            Pages = 0,
            Collection = collectionService.getCollectionFromId(userId)
            
        });
    }

    [Authorize]
    [HttpPost]
    public IActionResult FormPagination(int index)
    {
        String userId = _userManager.GetUserId(this.User);
        return View("Index", new CollectionViewModel
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
            Pages = cardService.GetPagesAmount(),
            Collection = collectionService.getCollectionFromId(userId)
        });
    }
    }
}