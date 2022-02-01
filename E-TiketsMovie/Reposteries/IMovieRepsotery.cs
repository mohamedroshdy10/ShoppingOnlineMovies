using E_TiketsMovie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Reposteries
{
    public interface IMovieRepsotery
    {
        Task<ResulteViewModel> Add(MoviewViewModel mdl);
        Task<IEnumerable<DisplayMoiveDTO>> GetAll();

        Task<MoviewViewModel> GetbyID(int? Id);
        Task<DisplayMoiveDTO> Detalies(int? Id);


        Task<ResulteViewModel> Delete(int Id);

        Task<ResulteViewModel> Update(MoviewViewModel mdl);




    }
}
