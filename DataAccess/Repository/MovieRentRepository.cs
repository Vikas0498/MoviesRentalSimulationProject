using Microsoft.EntityFrameworkCore;
using Models;
using MoviesRentalSimulationProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class MovieRentRepository : IRepository<MovieRent>
    {
        private readonly ApplicationDbContext _context;
        public MovieRentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(MovieRent item)
        {
            _context.MovieRents.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public MovieRent Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieRent> GetAll()
        {
            return _context.MovieRents.Include(c => c.Movie).ToList();
        }

        public void Update(MovieRent item)
        {
            throw new NotImplementedException();
        }
    }
}
