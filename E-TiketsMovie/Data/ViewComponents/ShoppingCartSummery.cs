using Microsoft.AspNetCore.Mvc;
using System;

namespace E_TiketsMovie.Data.ViewComponents
{
    public class ShoppingCartSummery : ViewComponent
    {
        private readonly ShoppingCard _shoppingCard;

        public ShoppingCartSummery(ShoppingCard shoppingCard)
        {
            this._shoppingCard = shoppingCard;
        }

        public  IViewComponentResult Invoke()
        {
            var Cart = _shoppingCard.GetShoppingCardItems();
            return View(Cart.Count);
        }
    }

    }

