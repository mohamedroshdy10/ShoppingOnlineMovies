using E_TiketsMovie.Data;
using E_TiketsMovie.Models.Enums;
using E_TiketsMovie.Models.Tables;
using E_TiketsMovie.Models.Tables.Peopel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_TiketsMovie.ViewModels
{
    public class ActorViewModel
    {
        public int ID { get; set; }
        public string Profile { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public IFormFile ActorImage { set; get; }
    }
    public class ActorViewModel_Movie
    {
        public int ID { get; set; }
        public string Profile { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public IFormFile ActorImage { set; get; }
        public ICollection<Actor_MoviewDTO> Actor_MoviewDTOs { get; set; }
    }
    public class Actor_MoviewDTO
    {
        public string MovieName { get; set; }
        public string MovieImageUrl { get; set; }
        public int MovieID { get; set; }
    }
    public class ProudusserViewModel
    {
        public int ID { get; set; }
        public string Profile { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public IFormFile ProudserImge { set; get; }

    }
    public class CienmaViewModel
    {
        public int ID { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile CinamaImage { set; get; }
    }
    public class MoviewViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "Movie description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Display(Name = "Movie poster URL")]
        [Required(ErrorMessage = "Movie poster URL is required")]
        public string ImageURL { get; set; }

        public IFormFile Poster { get; set; }

        [Display(Name = "Movie start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Movie category is required")]
        public MovieCatogery MovieCatogery { get; set; }

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int> ActorIds { get; set; }
        [Display(Name = "Select a cinema")]
        [Required(ErrorMessage = "Movie cinema is required")]
        public int CinemaId { get; set; }
        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int ProducerId { get; set; }
    }
    public class DisplayMoiveDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CinemaId { get; set; }
        public int ProducerId { get; set; }
        public string ImageURL { get; set; }

        public string StartDate { get; set; }


        public string EndDate { get; set; }
        public MovieCatogery MovieCatogery { get; set; }
        //public string MoiveCatogeryDisplay
        //{
        //    get
        //    {
        //       return MovieCatogery==0?""
        //    }
        //}

        //Relationships
        public List<ActorsDTO> ActorIds { get; set; }
        public string CinmaName { get; set; }
        public string ProudsserName { get; set; }
    }
    public class ActorsDTO
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public string ActorImage { get; set; }
    }
    public class GetSelect
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
    public class ShoppingCartVM
    {
        public ShoppingCard shoppingCard { get; set; }
        public double ShoppingCardTotal { get; set; }
    }

    public class OrderVM
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppliationUser User { set; get; }
        public ICollection<OrderIteamVm> orderIteamVms { get; set; }
        public string UserName { get; set; }

    }
    public class OrderIteamVm
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string MovieName { get; set; }

    }

}

