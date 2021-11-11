using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaApi.Entities;


namespace CinemaApi.Repository
{
    /// <summary>>
    /// Movie Repository
    /// </summary>
    public interface ICinemaRepository
    {
        Task<IEnumerable<CinemaDto>> GetList();
        Task<CinemaDto> GetMovieByID(int Id);
        void  Insert(CinemaDto Movie);
        void Delete(int Id);
        void Update(CinemaDto Movie);
        void Save(CinemaDto Movie);
    }
}
