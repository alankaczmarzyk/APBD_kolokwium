using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IRepoService
    {
        public IQueryable<Musician> getMusicianAsync(int id);

    }
}
