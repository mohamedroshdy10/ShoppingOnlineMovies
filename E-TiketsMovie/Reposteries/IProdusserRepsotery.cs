using E_TiketsMovie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Reposteries
{
    public interface IProdusserRepsotery
    {
        Task<ResulteViewModel>Add(ProudusserViewModel mdl);
        Task<IEnumerable<ProudusserViewModel>> GetAll();

        Task<ProudusserViewModel> GetbyID(int Id);

        Task<ResulteViewModel> Delete(int Id);

        Task<ResulteViewModel> Update(ProudusserViewModel mdl);
    }
}
