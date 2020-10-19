using Microsoft.EntityFrameworkCore;
using Models;
using MoviesRentalSimulationProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Movie item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Movie Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.Include(c => c.Genre).ToList();
        }

        public void Update(Movie item)
        {
            throw new NotImplementedException();
        }
    }
}
