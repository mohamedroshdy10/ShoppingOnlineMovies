using E_TiketsMovie.Models.Tables;
using E_TiketsMovie.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Data
{
    public class ShoppingCard
    {
        private readonly EcommercdbContext _context;

        public string ShoppingCardId { get; set; }
        public List<ShoppingCardItems> ShoppingCardItems { get; set; }
        public ShoppingCard(EcommercdbContext context)
        {
            this._context = context;
        }
        // GetShoppingCardIteam
        public static ShoppingCard GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<EcommercdbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCard(context) { ShoppingCardId = cartId };
        }
        public List<ShoppingCardItems> GetShoppingCardItems()
        {
            return ShoppingCardItems ?? (ShoppingCardItems = _context.ShoppingCardItems.
                Where(x => x.ShoppingCardId == ShoppingCardId).Include(a => a.Movie).ToList());
        }
        //GetShopping Card Total
        public double GetShoppingCradTotal() => (double)_context.ShoppingCardItems.Where(x => x.ShoppingCardId == ShoppingCardId).
            Include(a => a.Movie).Select(x => x.Movie.Price * x.Amount).Sum();

        #region Operations
        public void AddIteamTOCard(MoviewViewModel movie)
        {
            var shoppingCards = _context.ShoppingCardItems.Where(x => x.Movie.ID == movie.Id && 
            ShoppingCardId == ShoppingCardId).
                Include(d=>d.Movie).FirstOrDefault();
            try
            {
                if (shoppingCards == null)
                {
                    var shcCard = new ShoppingCardItems()
                    {
                        ShoppingCardId = ShoppingCardId,
                        Amount = 1,
                        MovieID=movie.Id
                    };
                    _context.ShoppingCardItems.Add(shcCard);
                }
                else
                {
                    shoppingCards.Amount++;
                }
                _context.SaveChanges();
            }
            catch (NullReferenceException ex1)
            {

                throw ex1;
            }
            catch (Exception )
            {
                throw;
            }
           
        }

        //Remove An Iteam
        public void RemoveIteamFromCrad(MoviewViewModel movie)
        {
            var shoppingCard = _context.ShoppingCardItems.
                Where(x => x.ShoppingCardId == ShoppingCardId 
                && 
                x.Movie.ID == movie.Id).FirstOrDefault();
            if(shoppingCard!=null)
            {
                if(shoppingCard.Amount>1)
                {
                    shoppingCard.Amount--;
                }
                else
                {
                    _context.ShoppingCardItems.Remove(shoppingCard);
                }
                _context.SaveChanges();
            }
        }
        public async Task ClearShoppingCart()
        {
            var orders = await _context.ShoppingCardItems.Where(x => x.ShoppingCardId == ShoppingCardId).ToListAsync();
            _context.RemoveRange(orders);
            await _context.SaveChangesAsync();
        }
        #endregion


    }
}
