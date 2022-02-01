using E_TiketsMovie.Models.Tables.Peopel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Models.Tables
{
    [Table(nameof(Order),Schema ="order")]
    public class Order
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public ICollection<OrderIteam>  OrderIteams { get; set; }
        [ForeignKey(nameof(UserId))]
        public  AppliationUser User { set; get; }
        public string UserId { get; set; }

    }
    [Table(nameof(OrderIteam), Schema = "order")]
    public class OrderIteam
    {
        public int ID { get; set; }
        public int Amount  { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(Movie))]
        public int MoiveId { get; set; }
        public  Movie  Movie { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order  Order { get; set; }

    }
    public class ShoppingCardItems
    {
        public int ID { get; set; }
        public Movie Movie  { get; set; }
        [ForeignKey(nameof(Movie))]
        public int MovieID { get; set; }
        public string ShoppingCardId { get; set; }
        public int Amount { get; set; }
    }
}
