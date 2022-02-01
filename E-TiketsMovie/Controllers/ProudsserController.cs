using E_TiketsMovie.Reposteries;
using E_TiketsMovie.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Controllers
{
    public class ProudsserController: Controller
    {
        #region Fildes
        private readonly ProusserRepsotery _repoProdusser;

        public ProudsserController(ProusserRepsotery repoProdusser)
        {
            this._repoProdusser = repoProdusser;
        }
        #endregion
        #region Actions
        public async Task<IActionResult> Index()
        {
            return View(await _repoProdusser.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> Detalies(int id) => View(await _repoProdusser.GetbyID(id));
        [HttpGet]
        public IActionResult Create() => View();
        [HttpGet]
        public async Task<IActionResult> Edit(int id) => View(await _repoProdusser.GetbyID(id));

        #endregion
        #region Ajax
        [HttpPost]
        public async Task<JsonResult> Add(ProudusserViewModel mdl) => Json(await _repoProdusser.Add(mdl));


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _repoProdusser.Delete(id));
        }

        [HttpPost]
        public async Task<JsonResult> Update(ProudusserViewModel mdl) => Json(await _repoProdusser.Update(mdl));
        #endregion

    }
}
