using E_TiketsMovie.Reposteries;
using E_TiketsMovie.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Controllers
{
    public class ActorController : Controller
    {
        #region Fildes
        private readonly ActorResotery _repoActor;

        public ActorController(ActorResotery repoActor)
        {
            this._repoActor = repoActor;
        }
        #endregion
        #region Actions
        public async Task<IActionResult> Index()
        {
            return View(await _repoActor.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> Detalies(int id) => View(await _repoActor.GetByIdForDetalies(id));
        [HttpGet]
        public IActionResult Create() => View();
        [HttpGet]
        public async Task<IActionResult> Edit(int id) => View(await _repoActor.GetbyID(id));
      
        #endregion
        #region Ajax
        [HttpPost]
        public async Task<JsonResult> Add(ActorViewModel mdl)=> Json(await _repoActor.Add(mdl));
            
                
          [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _repoActor.Delete(id));
        }

        [HttpPost]
        public async Task<JsonResult>  Update(ActorViewModel mdl) => Json( await _repoActor.Update(mdl));

        #endregion

    }
}
