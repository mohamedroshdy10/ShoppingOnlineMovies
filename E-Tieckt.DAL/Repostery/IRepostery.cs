using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Tieckt.DAL.Repostery
{
    public interface IRepostery<T> where T:class
    {
        Task<object> Add(T entity);
        Task<object> Update(T entity);

        Task<IEnumerable<T>> GetAll();
        Task GetbyID(int id);

    }
}
