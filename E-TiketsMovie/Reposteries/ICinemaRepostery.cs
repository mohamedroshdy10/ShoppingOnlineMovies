using E_TiketsMovie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Reposteries
{
    public interface ICinemaRepostery
    {
        Task<ResulteViewModel> Add(CienmaViewModel mdl);
        Task<IEnumerable<CienmaViewModel>> GetAll();

        Task<CienmaViewModel> GetbyID(int Id);

        Task<ResulteViewModel> Delete(int Id);

        Task<ResulteViewModel> Update(CienmaViewModel mdl);
    }
}
