using E_Tieckt.DAL.Repostery;
using E_TiketsMovie.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Tieckts.Bll
{
  public  class ActorBll
    {
        private readonly IRepostery<Actors> _repoActor;

        public ActorBll(IRepostery<Actors> repoActor)
        {
            this._repoActor = repoActor;
        }
       public ResulteViewModel Add(Actors mdl)
        {

        }
    }
}
