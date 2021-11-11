using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.Core.Interface
{
   public interface IMovieService 
    {
        public Task<List<Movie>> GetList();
        public Task<Movie> Get(int Id);
        public Task<bool> Insert(Movie Movie);
        public Task<bool> Delete(int Id);
        public Task<bool> Update(Movie Movie);

    }
}
