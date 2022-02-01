using E_TiketsMovie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Tieckt.DAL.Repostery
{
    public class Repostery<T> : IRepostery<T> where T : class
    {
        private readonly EcommercdbContext _context;

        public Repostery(EcommercdbContext context)
        {
            this._context = context;
        }
        public async Task<object> Add(T entity)
        {
             _context.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task GetbyID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResuteViewModel> Update(T entity)
        {
            throw new NotImplementedException();
        }

        Task<object> IRepostery<T>.Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
