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
    public class CinemaRepostery : ICinemaRepostery
    {

        private readonly EcommercdbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CinemaRepostery(EcommercdbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResulteViewModel> Add(CienmaViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            var valid = FormVliation(mdl.ID, mdl);
            if (valid != null)
            {
                resulteViewModel.IsSuccess = false;
            }
            else
            {
                Cinema cinema = new()
                {
                    Name = mdl.Name,
                    Logo = UploadImages(mdl),
                    Description = mdl.Description
                };
                var check = await _context.Cinemas.AddAsync(cinema);
                if (check != null)
                {
                    resulteViewModel.IsSuccess = true;
                    resulteViewModel.Message = "Saved";
                    resulteViewModel.Data = cinema;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    resulteViewModel.IsSuccess = false;
                    resulteViewModel.Message = "Saved Failad";
                }
            }
            return resulteViewModel;
        }


        public async Task<ResulteViewModel> Delete(int Id)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            var ele = await _context.Cinemas.FindAsync(Id);
            var CinmeMovies = _context.Cinemas.Where(xx => xx.Movies.Any(x => x.CinemaId == xx.ID)).Any();
            if(CinmeMovies)
            {
                resulteViewModel.Message = "Can Not Delete This Cinema ,It Have Movies To Dsipay";
                resulteViewModel.IsSuccess = false;
            }
            else
            {
                if (ele != null)
                {
                    _context.Cinemas.Remove(ele);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        resulteViewModel.Message = "Deleted Successfully";
                        resulteViewModel.IsSuccess = true;
                    }
                    else
                    {
                        resulteViewModel.Message = "Deleted Faliad";
                        resulteViewModel.IsSuccess = false;

                    }

                }
            }
           
            return resulteViewModel;
        }

        public async Task<IEnumerable<CienmaViewModel>> GetAll()
        {
            var data = await _context.Cinemas.Select(x => new CienmaViewModel
            {
                Logo = x.Logo,
                Description = x.Description,
                Name = x.Name,
                ID = x.ID
            }).ToListAsync();
            return data;
        }

        public async Task<CienmaViewModel> GetbyID(int id)
        {
            var x = await _context.Cinemas.FindAsync(id);
            CienmaViewModel mdl = new()
            {
                Name = x.Name,
                Description = x.Description,
                Logo = x.Logo,
                ID = x.ID
            };
            return mdl;
        }

        public async Task<ResulteViewModel> Update(CienmaViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            var oldImage = string.Empty;
            var cinam = _context.Cinemas.Find(mdl.ID);
            cinam.Name = mdl.Name;
            if (mdl.CinamaImage != null)
            {
                oldImage = cinam.Logo;
                cinam.Logo = UploadImages(mdl);
            }

            cinam.Description = mdl.Description;

            var check = _context.Cinemas.Update(cinam);
            if (check != null)
            {
                if (oldImage != null)
                {
                    var p = _webHostEnvironment.WebRootPath + "/Images/" + oldImage;
                    if (System.IO.File.Exists(p))
                        System.IO.File.Delete(p);
                }
                resulteViewModel.IsSuccess = true;
                resulteViewModel.Message = "Update";
                resulteViewModel.Data = cinam;
                await _context.SaveChangesAsync();
            }
            else
            {
                resulteViewModel.IsSuccess = false;
                resulteViewModel.Message = "Saved Failad";
            }

            return resulteViewModel;
        }

        public string UploadImages(CienmaViewModel mdl)
        {
            string uniqueFileName = null;
            if (mdl.CinamaImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + mdl.CinamaImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                try
                {
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    mdl.CinamaImage.CopyTo(fileStream);
                }

                catch (Exception)
                {
                    throw;
                }
            }


            return uniqueFileName;
        }
        public ResulteViewModel FormVliation(int? Id, CienmaViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            if (string.IsNullOrEmpty(mdl.Name))
            {
                resulteViewModel.Message = "Name Is Required";
                resulteViewModel.IsSuccess = false;
                return resulteViewModel;
            }
            if (string.IsNullOrEmpty(mdl.Description))
            {
                resulteViewModel.Message = "Bio Is Required";
                resulteViewModel.IsSuccess = false;
                return resulteViewModel;
            }
            if (mdl.CinamaImage == null)
            {
                resulteViewModel.Message = "Prfile Is Required";
                resulteViewModel.IsSuccess = false;
                return resulteViewModel;
            }
            IQueryable<Cinema> allActores = null;
            allActores = _context.Cinemas.Where(x => x.ID != Id);
            if (allActores != null)
            {
                if (allActores.Any(x => x.Name == mdl.Name))
                {
                    resulteViewModel.Message = "Full Name Must Be Uniqe";
                    resulteViewModel.IsSuccess = false;
                    return resulteViewModel;
                }
                if (allActores.Any(x => x.Logo == UploadImages(mdl)))
                {
                    resulteViewModel.Message = "This Image Is Here  Must Be Uniqe";
                    resulteViewModel.IsSuccess = false;
                    return resulteViewModel;
                }
                else { }
            }
            return null;
        }
    }
}
