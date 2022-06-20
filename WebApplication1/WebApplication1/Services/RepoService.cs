using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class RepoService : IRepoService
    {
        private readonly RepositoryContext _context;
        public RepoService(RepositoryContext context)
        {
            _context = context;
        }

        public  IQueryable<Musician> getMusicianAsync(int id)
        {
            return _context.Musicians.Where(e => e.IdMusician == id);


        }
    }
}
