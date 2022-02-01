using E_TiketsMovie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Reposteries
{
    public interface IActorRepostery
    {
        Task<ResulteViewModel> Add(ActorViewModel mdl);
        Task<IEnumerable<ActorViewModel>> GetAll();

        Task<ActorViewModel> GetbyID(int Id);
        Task<ActorViewModel_Movie> GetByIdForDetalies(int Id);


        Task<ResulteViewModel> Delete(int Id);

        Task<ResulteViewModel> Update(ActorViewModel mdl);
       
    }
}
