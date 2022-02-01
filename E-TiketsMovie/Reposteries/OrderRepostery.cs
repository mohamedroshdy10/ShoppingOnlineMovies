using E_TiketsMovie.Data;
using E_TiketsMovie.Data.Static;
using E_TiketsMovie.Models.Tables;
using E_TiketsMovie.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Reposteries
{
    public class OrderRepostery : IOrderRepostery
    {
        private readonly EcommercdbContext _context;

        public OrderRepostery(EcommercdbContext context)
        {
            this._context = context;
        }
        public async Task<ResulteViewModel> AddOrder(List<ShoppingCardItems> cardItems, string UserId, string Email)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel()
            {
                IsSuccess = false,
                Message = "Error"
            };
            var order = new Order();
            order.Email = Email;
            order.UserId = UserId;
            order.OrderIteams = cardItems.Select(x => new OrderIteam
            {
                Amount = x.Amount,
                MoiveId = x.MovieID,
                Price = x.Movie.Price,
                OrderId = order.ID
            }).ToList();



            //foreach (var item in cardItems)
            //{
            //    var orderItems = new OrderIteam()
            //    {
            //        Amount = item.Amount,
            //        MoiveId = item.MovieID,
            //        OrderId = order.ID,
            //        Price = item.Movie.Price
            //    };
            //}
            await _context.Orders.AddAsync(order);
            if (await _context.SaveChangesAsync() > 0)
            {
                resulteViewModel.IsSuccess = true;
                resulteViewModel.Message = "Saved Successfully";
            }
            else
            {
                resulteViewModel.IsSuccess = true;
                resulteViewModel.Message = "Saved Failad";
            }
            return resulteViewModel;
        }

        public async Task<List<OrderVM>> GetAllOrder(string userId, string userRole)
        {
            try
            {
                if (userRole == UserRoles.User)
                {
                    var Orders = await _context.Orders.Where(x => x.UserId == userId).
                                        Include(x => x.User).Include(i => i.OrderIteams).
                                        ThenInclude(c => c.Movie).
                                        Select(x => new OrderVM
                                        {
                                            ID = x.ID,
                                            UserName=x.User.FullName,
                                            orderIteamVms = x.OrderIteams.Select(xx => new OrderIteamVm
                                            {
                                                Amount = xx.Amount,
                                                MovieName = xx.Movie.Name,
                                                Price = xx.Movie.Price
                                            }).ToList()
                                        }).ToListAsync();
                    return Orders;
                }
                else
                {
                    return await _context.Orders.
                                        Include(x => x.User).Include(i => i.OrderIteams).
                                        ThenInclude(c => c.Movie).
                                        Select(x => new OrderVM
                                        {
                                            ID = x.ID,
                                            UserName = x.User.FullName,
                                            orderIteamVms = x.OrderIteams.Select(xx => new OrderIteamVm
                                            {
                                                Amount = xx.Amount,
                                                MovieName = xx.Movie.Name,
                                                Price = xx.Movie.Price
                                            }).ToList()
                                        }).ToListAsync();
                }

                
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
