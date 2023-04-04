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
    public class ShoppingCartController : Controller
    {

        private readonly ILogger<ShoppingCartController> _logger;
        private CardService cardService = new CardService();
        private static ShoppingCartService shoppingCartService = new ShoppingCartService(); 
        private readonly UserManager<IdentityUser> _userManager;

    public ShoppingCartController(ILogger<ShoppingCartController> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

        [Authorize]
        [HttpGet("[controller]")]
        public IActionResult Index()
        {
            String userId = _userManager.GetUserId(this.User);
            return View(new ShoppingCartViewModel{
                Cards = shoppingCartService.getShoppingCartFromId(userId).Select(
                    c => new CardViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        OriginalImageUrl = c.OriginalImageUrl,
                        OriginalText = c.OriginalText,
                        RarityCode = c.RarityCode,
                        Price = Math.Round(1.24 * float.Parse(c.ConvertedManaCost)+2.83,2)
                    }).ToList()
            });
        }

        [Authorize]
        [HttpGet("[controller]/add/{index}")]
        public IActionResult AddToCart(int index)
        {
            String userId = _userManager.GetUserId(this.User);
            Card selectedCard = cardService.GetCardById(index);

            shoppingCartService.addToCart(userId, selectedCard);

            return Content("Added to shopping cart");
        }

        [Authorize]
        [HttpGet("[controller]/remove/{index}")]
        public IActionResult removeFromCart(int index)
        {
            String userId = _userManager.GetUserId(this.User);
            Card selectedCard = cardService.GetCardById(index);

            shoppingCartService.removeFromCart(userId, selectedCard);

            return View("Index", new ShoppingCartViewModel{
                Cards = shoppingCartService.getShoppingCartFromId(userId).Select(
                    c => new CardViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        OriginalImageUrl = c.OriginalImageUrl,
                        OriginalText = c.OriginalText,
                        RarityCode = c.RarityCode,
                        Price = Math.Round(1.24 * float.Parse(c.ConvertedManaCost)+2.83,2)
                    }).ToList()
            });  
        }

        [Authorize]
        [HttpGet("[controller]/buy")]
        public IActionResult buyCart(){
            String userId = _userManager.GetUserId(this.User);

            shoppingCartService.resetShoppingCart(userId);

            return View("ThankYou");
        }
    }
}