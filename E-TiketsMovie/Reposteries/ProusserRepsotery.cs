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
    public class ProusserRepsotery : IProdusserRepsotery
    {
        private readonly EcommercdbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProusserRepsotery(EcommercdbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
        }


        public string UploadImages(ProudusserViewModel mdl)
        {
            string uniqueFileName = null;
            if (mdl.ProudserImge != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + mdl.ProudserImge.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                try
                {
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    mdl.ProudserImge.CopyTo(fileStream);
                }

                catch (Exception)
                {
                    throw;
                }
            }


            return uniqueFileName;
        }
        public async Task<ResulteViewModel> Add(ProudusserViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            //var valid = FormVliation(mdl.ID, mdl);
            ////if (valid != null)
            ////{
            ////    resulteViewModel.ISSuccess = false;
            ////}
            //else
            //{
                Produsser produsser = new()
                {
                    fullName = mdl.FullName,
                    profile = UploadImages(mdl),
                    Bio = mdl.Bio,
                };
                var check = await _context.Produssers.AddAsync(produsser);
                if (check != null)
                {
                    resulteViewModel.IsSuccess = true;
                    resulteViewModel.Message = "Saved";
                    resulteViewModel.Data = produsser;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    resulteViewModel.IsSuccess = false;
                    resulteViewModel.Message = "Saved Failad";
                }
            //}


            return resulteViewModel;
        }

        public async Task<IEnumerable<ProudusserViewModel>> GetAll()
        {
            var data = await _context.Produssers.Select(x => new ProudusserViewModel
            {
                Profile = x.profile,
                Bio = x.Bio,
                FullName = x.fullName,
                ID = x.ID
            }).ToListAsync();
            return data;
        }

        public async Task<ProudusserViewModel> GetbyID(int id)
        {
            var x = await _context.Produssers.FindAsync(id);
            ProudusserViewModel mdl = new ProudusserViewModel()
            {
                FullName = x.fullName,
                Bio = x.Bio,
                Profile = x.profile,
                ID = x.ID
            };
            return mdl;
        }

        public async Task<ResulteViewModel> Delete(int Id)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            var ele = await _context.Produssers.FindAsync(Id);
            if (ele != null)
            {
                _context.Produssers.Remove(ele);
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
            return resulteViewModel;
        }
        public ResulteViewModel FormVliation(int? Id, ProudusserViewModel mdl)
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
            if (mdl.ProudserImge == null)
            {
                resulteViewModel.Message = "Prfile Is Required";
                resulteViewModel.IsSuccess = false;
                return resulteViewModel;
            }
            IQueryable<ProudusserViewModel> allActores = null;
            allActores = (IQueryable<ProudusserViewModel>)_context.Produssers.Where(x => x.ID != Id).FirstOrDefault();
            if (allActores != null)
            {
                if (allActores.Any(x => x.FullName == mdl.FullName))
                {
                    resulteViewModel.Message = "Full Name Must Be Uniqe";
                    resulteViewModel.IsSuccess = false;
                    return resulteViewModel;
                }
                if (allActores.Any(x => x.Profile == UploadImages(mdl)))
                {
                    resulteViewModel.Message = "This Image Is Here  Must Be Uniqe";
                    resulteViewModel.IsSuccess = false;
                    return resulteViewModel;
                }
                else { }
            }
            return null;
        }

        public async Task<ResulteViewModel> Update(ProudusserViewModel mdl)
        {
            ResulteViewModel resulteViewModel = new ResulteViewModel() { IsSuccess = false };
            var oldImage = string.Empty;
            var produsser  = _context.Produssers.Find(mdl.ID);
            produsser.fullName = mdl.FullName;
            if (mdl.ProudserImge != null)
            {
                oldImage = produsser.profile;
                produsser.profile = UploadImages(mdl);
            }

            produsser.Bio = mdl.Bio;

            var check = _context.Produssers.Update(produsser);
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
                resulteViewModel.Data = produsser;
                await _context.SaveChangesAsync();
            }
            else
            {
                resulteViewModel.IsSuccess = false;
                resulteViewModel.Message = "Saved Failad";
            }

            return resulteViewModel;
        }
    }
}
