using E_TiketsMovie.Models.Enums;
using E_TiketsMovie.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Models.Tables
{
    [Table(nameof(Actor),Schema ="guide")]
    public class Actor
    {
        [Key]
        public int ID { get; set; }
        public string profile { get; set; }
        public string fullName { get; set; }
        public string Bio { get; set; }

        #region Relations
        public ICollection<Actor_Movies> Actor_Movies { get; set; }
        #endregion
    }

    [Table(nameof(Produsser), Schema = "guide")]
    public class Produsser
    {
        [Key]
        public int ID { get; set; }
        public string profile { get; set; }
        public string fullName { get; set; }
        public string Bio { get; set; }
        public ICollection<Movie>Movies { get; set; }
    }

    [Table(nameof(Cinema), Schema = "guide")]
    public class Cinema
    {
        [Key]
        public int ID { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection <Movie> Movies { get; set; }
    }

    [Table(nameof(Movie), Schema = "guide")]
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime endDate { get; set; }
        public MovieCatogery  MovieCatogery { get; set; }

        // Can be One Cimema
        public virtual Cinema Cinema { get; set; }
        [ForeignKey(nameof(Cinema))]
        public int CinemaId { get; set; }
        //Prossdure
        [ForeignKey(nameof(ProdusserId))]
        public virtual Produsser Produsser  { get; set; }
        public int ProdusserId { get; set; }
        #region Relations
        public ICollection<Actor_Movies>  Actor_Movies { get; set; }
        public ICollection<OrderIteam> OrderIteams { set; get; }


        #endregion
    }

    [Table(nameof(Actor_Movies), Schema = "guide")]
    public class Actor_Movies
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(ActorID))]
        public virtual Actor   Actor { get; set; }
        public int ActorID { get; set; }


        [ForeignKey(nameof(MovieID))]
        public virtual Movie  Movie { get; set; }
        public int MovieID { get; set; }

        
    }
}
