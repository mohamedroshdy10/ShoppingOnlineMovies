using E_TiketsMovie.Reposteries;
using E_TiketsMovie.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoiveRepostery _repoMoive;
        #region Fileds
        public MoviesController(MoiveRepostery repoMoive)
        {
            this._repoMoive = repoMoive;
        }
        #endregion
        public async Task<IActionResult> Index() => View(await _repoMoive.GetAll());
        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await _repoMoive.Filter(searchString);
            if (!string.IsNullOrEmpty(searchString))
                return View(nameof(Index), data);
            else
                return View(nameof(Index),await _repoMoive.GetAll());
        }


        //Actions
        public IActionResult Create() => View();
        public async Task<IActionResult> Edit(int? Id)
        {
            if(Id.HasValue)
            {
                return View(await _repoMoive.GetbyID(Id));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region Ajax
        [HttpPost]
        public async Task<IActionResult> Add(MoviewViewModel mdl)
        {

            if (!ModelState.IsValid) { return View(mdl); }
            return Json( await _repoMoive.Add(mdl));
        }
        [HttpPost]
        public async Task<IActionResult> Update(MoviewViewModel mdl)
        {
            if (!ModelState.IsValid) { return View(mdl); }
            return Json(await _repoMoive.Update(mdl));
        }

        public async Task<IActionResult> Detalies(int ?Id)
        {
            if(Id.HasValue)
            {
                return View(await _repoMoive.Detalies(Id));
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int Id) => Json(await _repoMoive.Delete(Id));





        #endregion


    }
}
