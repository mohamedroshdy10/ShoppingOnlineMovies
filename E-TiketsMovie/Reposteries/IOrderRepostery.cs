using E_TiketsMovie.Models.Tables;
using E_TiketsMovie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Reposteries
{
    public interface IOrderRepostery
    {
        Task<ResulteViewModel> AddOrder(List<ShoppingCardItems> cardItems, string UserId, string Email);
        Task<List<OrderVM>> GetAllOrder(string userId,string userRole);
    }
}
