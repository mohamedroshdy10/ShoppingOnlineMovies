using E_TiketsMovie.Reposteries;
using E_TiketsMovie.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Controllers
{
    public class CinemaController : Controller
    {
        #region Fildes
        private readonly CinemaRepostery _repoCinam;

        public CinemaController(CinemaRepostery  repoCinam)
        {
            this._repoCinam = repoCinam;
        }
        #endregion
        #region Actions
        public async Task<IActionResult> Index()
        {
            return View(await _repoCinam.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> Detalies(int id) => View(await _repoCinam.GetbyID(id));
        [HttpGet]
        public IActionResult Create() => View();
        [HttpGet]
        public async Task<IActionResult> Edit(int id) => View(await _repoCinam.GetbyID(id));

        #endregion
        #region Ajax
        [HttpPost]
        public async Task<JsonResult> Add(CienmaViewModel mdl) => Json(await _repoCinam.Add(mdl));


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _repoCinam.Delete(id));
        }

        [HttpPost]
        public async Task<JsonResult> Update(CienmaViewModel mdl) => Json(await _repoCinam.Update(mdl));

        #endregion
    }
}
