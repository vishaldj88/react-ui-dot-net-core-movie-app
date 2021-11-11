using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Core.Interface
{
   public interface ICinemaService 
    {
        public Task<List<CinemaModel>> GetList();
        public Task<CinemaModel> Get(int Id);
        public bool Insert(CinemaModel Movie);
        public bool Delete(int Id);
        public bool Update(CinemaModel Movie);
        public bool Save(CinemaModel Movie);
    }
}
