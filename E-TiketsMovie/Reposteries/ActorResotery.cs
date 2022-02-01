using E_TiketsMovie.AppConst;
using E_TiketsMovie.Data;
using E_TiketsMovie.Models.Tables;
using E_TiketsMovie.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Reposteries
{
    public class ActorResotery : IActorRepostery
    {
        private readonly EcommercdbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ActorResotery(EcommercdbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResulteViewModel> Add(ActorViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            var valid = FormVliation(mdl);
            if (valid != null)
            {
                resulteViewModel.IsSuccess = false;
            }
            else
            {
                Actor actor = new()
                {
                    fullName = mdl.FullName,
                    profile = UploadImages(mdl),
                    Bio = mdl.Bio
                };
                var check = await _context.Actors.AddAsync(actor);
                if (check != null)
                {
                    resulteViewModel.IsSuccess = true;
                    resulteViewModel.Message = AppConst.Message.SavedSuccessFully;
                    resulteViewModel.Data = actor;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    resulteViewModel.IsSuccess = false;
                    resulteViewModel.Message = AppConst.Message.SavedFaild;
                }
           }
            return resulteViewModel;
        }


        public async Task<ResulteViewModel> Delete(int Id)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            var Actor_Movies = _context.Actor_Movies.Where(x => x.ActorID == Id).Any();
            if(Actor_Movies)
            {
                resulteViewModel.Message = "Can Not Delete This Actor Now! ";
                resulteViewModel.IsSuccess = false;
            }
            else
            {
                var ele = await _context.Actors.FindAsync(Id);

                if (ele != null)
                {
                    _context.Actors.Remove(ele);
                    if (await _context.SaveChangesAsync() > 0)
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
            }
           
            return resulteViewModel;
        }

        public async Task<IEnumerable<ActorViewModel>> GetAll()
        {
            var data = await _context.Actors.Select(x => new ActorViewModel
            {
                Profile = x.profile,
                Bio = x.Bio,
                FullName = x.fullName,
                ID = x.ID
            }).ToListAsync();
            return data;
        }

        public async Task<ActorViewModel> GetbyID(int id)
        {
            var x = await _context.Actors.FindAsync(id);
            ActorViewModel mdl = new ActorViewModel()
            {
                FullName = x.fullName,
                Bio = x.Bio,
                Profile = x.profile,
                ID = x.ID
            };
            return mdl;
        }


        public async Task<ResulteViewModel> Update(ActorViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            var oldImage = string.Empty;
            var actor = _context.Actors.Find(mdl.ID);
            if(!string.IsNullOrEmpty(mdl.FullName))
            {
                actor.fullName = mdl.FullName;
            }
            if (mdl.ActorImage != null)
            {
                oldImage = actor.profile;
                actor.profile = UploadImages(mdl);
            }
            actor.Bio = mdl.Bio;
            var check = _context.Actors.Update(actor);
            if (check != null)
            {
                if (oldImage != null)
                {
                    var p = _webHostEnvironment.WebRootPath + AppConst.Constents.PathImage + oldImage;
                    if (System.IO.File.Exists(p))
                        System.IO.File.Delete(p);
                }
                resulteViewModel.IsSuccess = true;
                resulteViewModel.Message = AppConst.Message.UpdateSuccess;
                resulteViewModel.Data = actor;
                await _context.SaveChangesAsync();
            }
            else
            {
                resulteViewModel.IsSuccess = false;
                resulteViewModel.Message = AppConst.Message.UpdaeFaliad;
            }

            return resulteViewModel;
        }

        public string UploadImages(ActorViewModel mdl)
        {
            string uniqueFileName = null;
            if (mdl.ActorImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + mdl.ActorImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                try
                {
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    mdl.ActorImage.CopyTo(fileStream);
                }

                catch (Exception)
                {
                    throw;
                }
            }


            return uniqueFileName;
        }
        public ResulteViewModel FormVliation(ActorViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            if (string.IsNullOrEmpty(mdl.FullName))
            {
                resulteViewModel.Message = "Name Is Required";
                resulteViewModel.IsSuccess = false;
                return resulteViewModel;
            }
            if (string.IsNullOrEmpty(mdl.Bio))
            {
                resulteViewModel.Message = "Bio Is Required";
                resulteViewModel.IsSuccess = false;
                return resulteViewModel;
            }
            if (mdl.ActorImage == null)
            {
                resulteViewModel.Message = "Prfile Is Required";
                resulteViewModel.IsSuccess = false;
                return resulteViewModel;
            }
            IQueryable<Actor> allActores = null;
            allActores =  _context.Actors.Where(x => x.ID != mdl.ID);
            if (allActores != null)
            {
                if (allActores.Any(x => x.fullName == mdl.FullName))
                {
                    resulteViewModel.Message = "Full Name Must Be Uniqe";
                    resulteViewModel.IsSuccess = false;
                    return resulteViewModel;
                }
                if (allActores.Any(x => x.profile == UploadImages(mdl)))
                {
                    resulteViewModel.Message = "This Image Is Here  Must Be Uniqe";
                    resulteViewModel.IsSuccess = false;
                    return resulteViewModel;
                }
                else { }
            }
            
            return null;
        }

        public async Task<ActorViewModel_Movie> GetByIdForDetalies(int Id)
        {
            var data = await _context.Actors.Where(z=>z.ID==Id).Include(m => m.Actor_Movies).
                ThenInclude(c => c.Movie).Select(x => new ActorViewModel_Movie
                {
                    Profile = x.profile,
                    Bio = x.Bio,
                    FullName = x.fullName,
                    ID = x.ID,
                    Actor_MoviewDTOs = x.Actor_Movies.Select(xx => new Actor_MoviewDTO
                    {
                        MovieID = xx.MovieID,
                        MovieImageUrl = xx.Movie.ImageUrl,
                        MovieName = xx.Movie.Name
                    }).ToList()
                }).FirstOrDefaultAsync();
            return data;
        }
    }
}
