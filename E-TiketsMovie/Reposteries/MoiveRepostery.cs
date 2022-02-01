using E_TiketsMovie.Data;
using E_TiketsMovie.Models.Tables;
using E_TiketsMovie.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Reposteries
{
    public class MoiveRepostery : IMovieRepsotery
    {
        private readonly EcommercdbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MoiveRepostery(EcommercdbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
        }
        public string UploadImages(MoviewViewModel mdl)
        {
            string uniqueFileName = null;
            if (mdl.Poster != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + mdl.Poster.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                try
                {
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    mdl.Poster.CopyTo(fileStream);
                }

                catch (Exception)
                {
                    throw;
                }
            }


            return uniqueFileName;
        }
        public async Task<ResulteViewModel> Add(MoviewViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel();
            var checkName = _context.Movies.Where(x => x.Name == mdl.Name).Any();

            if (string.IsNullOrEmpty(mdl.Name))
            {

            }
            else if (checkName)
            {
                resulteViewModel.IsSuccess = false;
                resulteViewModel.Message = "This Name Alerdy Existes";
            }
            else
            {
                Movie movie = new Movie()
                {
                    CinemaId = mdl.CinemaId,
                    Name = mdl.Name,
                    Price = mdl.Price,
                    ImageUrl = UploadImages(mdl),
                    StarDate = mdl.StartDate,
                    endDate = mdl.EndDate,
                    MovieCatogery = mdl.MovieCatogery,
                    ProdusserId = mdl.ProducerId,
                    Description = mdl.Description,
                    Actor_Movies = mdl.ActorIds.Select(x => new Actor_Movies
                    {
                        ActorID = x,
                    }).ToList()
                };
                await _context.Movies.AddAsync(movie);

                #region with For
                //foreach (var item in mdl.ActorIds)
                //{
                //    Actor_Movies actor_Movies = new Actor_Movies()
                //    {
                //        ActorID = item,
                //        MovieID = movie.ID

                //    };
                //    await _context.Actor_Movies.AddAsync(actor_Movies);
                //}
                #endregion
                if (await _context.SaveChangesAsync() > 0)
                {
                    resulteViewModel.IsSuccess = true;
                    resulteViewModel.Message = "Saved SuucessFully";
                }
                else
                {
                    resulteViewModel.IsSuccess = false;
                    resulteViewModel.Message = "Faild To Save";

                }

            }
            return resulteViewModel;
        }

        public IList<GetSelect> SelectActor()
        {
            return _context.Actors.Select(x => new GetSelect
            {
                Value = x.ID,
                Text = x.fullName
            }).ToList();
        }
        public IList<GetSelect> selectProdusser()
        {
            return _context.Produssers.Select(x => new GetSelect
            {
                Value = x.ID,
                Text = x.fullName
            }).ToList();
        }
        public IList<GetSelect> SelectCinema()
        {
            return _context.Cinemas.Select(x => new GetSelect
            {
                Value = x.ID,
                Text = x.Name
            }).ToList();
        }

        public async Task<ResulteViewModel> Delete(int Id)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            var data = await _context.Movies.FindAsync(Id);
            // var moviesActor = _context.Actors.Where(x => x.Actor_Movies.Any(x => x.MovieID == Id)).ToListAsync();
            if (data != null)
            {
                _context.Movies.Remove(data);
               
                if (await _context.SaveChangesAsync()>0)
                {
                    resulteViewModel.Message = AppConst.Message.DeleteSuccess;
                    resulteViewModel.IsSuccess = true;
                }
                else
                {
                    resulteViewModel.Message = AppConst.Message.DeleteFaliad;
                    resulteViewModel.IsSuccess = false;
                }
            }
            return resulteViewModel;

        }
        public async Task<IEnumerable<DisplayMoiveDTO>> GetAll()
        {
            var Data = await _context.Movies.Select(x => new DisplayMoiveDTO
            {
                Name = x.Name,
                CinmaName = x.Cinema.Name,
                Price = x.Price,
                Description = x.Description,
                MovieCatogery = x.MovieCatogery,
                EndDate = x.endDate.ToString("F"),
                StartDate = x.StarDate.ToString("F"),
                ProudsserName = x.Produsser.fullName,
                ImageURL = x.ImageUrl,
                Id = x.ID,
            }).ToListAsync();
            return Data;
        }
        public async Task<IEnumerable<DisplayMoiveDTO>> Filter(string searchName)
        {
            var Data = await _context.Movies.Where(x => x.Name.Contains(searchName) || x.Description.Contains(searchName)).Select(x => new DisplayMoiveDTO
            {
                Name = x.Name,
                CinmaName = x.Cinema.Name,
                Price = x.Price,
                Description = x.Description,
                MovieCatogery = x.MovieCatogery,
                EndDate = x.endDate.ToString("F"),
                StartDate = x.StarDate.ToString("F"),
                ProudsserName = x.Produsser.fullName,
                ImageURL = x.ImageUrl,
                Id = x.ID,
            }).ToListAsync();
            return Data;
        }

        public async Task<MoviewViewModel> GetbyID(int? Id)
        {
            var Data = await _context.Movies.Where(x => x.ID == Id).Select(x => new MoviewViewModel
            {
                Id = x.ID,
                Name = x.Name,
                ActorIds = x.Actor_Movies.Select(x => x.ActorID).ToList(),
                CinemaId = x.CinemaId,
                Description = x.Description,
                EndDate = x.endDate,
                StartDate = x.StarDate,
                ImageURL = x.ImageUrl,
                MovieCatogery = x.MovieCatogery,
                Price = x.Price,
                ProducerId = x.ProdusserId
            }).FirstOrDefaultAsync();
            return Data;
        }

        public async Task<ResulteViewModel> Update(MoviewViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel();
            var moive = await _context.Movies.FindAsync(mdl.Id);
            string OldPath = "";
            if (string.IsNullOrEmpty(mdl.Name))
            {

            }
            else
            {

                List<Actor_Movies> list = await _context.Actor_Movies.Where(x => x.MovieID == mdl.Id).ToListAsync();
                _context.RemoveRange(list);

                moive.CinemaId = mdl.CinemaId;
                moive.Name = mdl.Name;
                moive.Price = mdl.Price;
                if (mdl.Poster != null)
                {
                    OldPath = moive.ImageUrl;
                    moive.ImageUrl = UploadImages(mdl);

                }

                moive.StarDate = mdl.StartDate;
                moive.endDate = mdl.EndDate;
                moive.MovieCatogery = mdl.MovieCatogery;
                moive.ProdusserId = mdl.ProducerId;
                moive.Description = mdl.Description;
                moive.Actor_Movies = mdl.ActorIds.Select(x => new Actor_Movies
                {
                    ActorID = x,
                }).ToList();

                _context.Movies.Update(moive);

                //foreach (var item in mdl.ActorIds)
                //{
                //    Actor_Movies actor_Movies = new Actor_Movies()
                //    {
                //        ActorID = item,
                //        MovieID = movie.ID

                //    };
                //    await _context.Actor_Movies.AddAsync(actor_Movies);
                //}
                if (await _context.SaveChangesAsync() > 0)
                {
                    var pathexites = _webHostEnvironment.WebRootPath + "Images" + OldPath;
                    if (System.IO.File.Exists(pathexites))
                        System.IO.File.Delete(pathexites);
                    resulteViewModel.IsSuccess = true;
                    resulteViewModel.Message = "UpdateDone";
                    await _context.SaveChangesAsync();

                }
                else
                {
                    resulteViewModel.IsSuccess = false;
                    resulteViewModel.Message = "Failad Update";

                }

            }
            return resulteViewModel;
        }

        public async Task<DisplayMoiveDTO> Detalies(int? Id)
        {
            var Data = await _context.Movies.Where(x => x.ID == Id).Select(x => new DisplayMoiveDTO
            {
                Name = x.Name,
                CinmaName = x.Cinema.Name,
                Price = x.Price,
                Description = x.Description,
                MovieCatogery = x.MovieCatogery,
                EndDate = x.endDate.ToString("F"),
                StartDate = x.StarDate.ToString("F"),
                ProudsserName = x.Produsser.fullName,
                CinemaId = x.CinemaId,
                ProducerId = x.ProdusserId,
                ImageURL = x.ImageUrl,
                Id = x.ID,
                ActorIds = x.Actor_Movies.Where(xx => xx.MovieID == Id).Select(x => new ActorsDTO
                {
                    ActorId = x.ActorID,
                    ActorImage = x.Actor.profile,
                    ActorName = x.Actor.fullName
                }).ToList()
            }).FirstOrDefaultAsync();
            return Data;
        }


    }
}
