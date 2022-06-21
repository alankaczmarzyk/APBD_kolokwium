using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IRepoService
    {
        public IQueryable<Musician> getMusicianAsync(int id);

        public IQueryable<Album> getAlbumAsync(int id);
        public Task<IEnumerable<Track>> GetTracks(int idMusician);

        public Task<bool> IsMusicianValidDoDelete(int idMusician);
        public Task<bool> DoesMusicianExist(int idMusician);

        public Task DeleteMusician(int idMusician);
        public Task DeleteMusiciansTracks(int idMusician);
        public Task<IEnumerable<Musician_Track>> GetMusicianTracks(int idMusician);
        public Task SaveDatabase();
    }
}
