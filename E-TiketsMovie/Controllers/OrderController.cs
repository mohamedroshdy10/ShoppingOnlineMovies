using E_TiketsMovie.Data;
using E_TiketsMovie.Reposteries;
using E_TiketsMovie.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_TiketsMovie.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShoppingCard _shoppingCard;
        private readonly EcommercdbContext _context;
        private readonly OrderRepostery _repoRepsotery;

        public MoiveRepostery _repoMovie { get; }

        public OrderController(ShoppingCard shoppingCard, MoiveRepostery repoMovie,
            EcommercdbContext context,
            OrderRepostery repoRepsotery)
        {
            this._shoppingCard = shoppingCard;
            this._repoMovie = repoMovie;
            this._context = context;
            this._repoRepsotery = repoRepsotery;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var AllOrders = await _repoRepsotery.GetAllOrder(userId,userRole);
            return View(AllOrders);
        }
        public IActionResult ShoppingCart()
        {
            var Iteams = _shoppingCard.GetShoppingCardItems();
            _shoppingCard.ShoppingCardItems = Iteams;
            var resopns = new ShoppingCartVM()
            {
                 shoppingCard=_shoppingCard,
                  ShoppingCardTotal= _shoppingCard.GetShoppingCradTotal()
            };
            return View(resopns);
        }

        public async Task<RedirectToActionResult> AddItemToShoppingCart(int Id)
        {
            var item = await _repoMovie.GetbyID(Id);
            if(item!=null)
            {
                _shoppingCard.AddIteamTOCard(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<RedirectToActionResult> RemoveIteam(int Id)
        {
            var item = await _repoMovie.GetbyID(Id);
            if (item != null)
            {
                _shoppingCard.RemoveIteamFromCrad(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var Items = _shoppingCard.GetShoppingCardItems();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            await _repoRepsotery.AddOrder(Items, userId, UserEmail);
            await _shoppingCard.ClearShoppingCart();
            return View("OrderComplete");
           
        }
    }
}
