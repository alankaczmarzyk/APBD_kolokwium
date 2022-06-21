using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IQueryable<Album> getAlbumAsync(int id)
        {
            return _context.Albums.Where(e => e.IdAlbum == id);
        }

        public  IQueryable<Musician> getMusicianAsync(int id)
        {
            return _context.Musicians.Where(e => e.IdMusician == id);


        }

        public async Task DeleteMusiciansTracks(int idMusician)
        {
            var musicianTracks = await GetMusicianTracks(idMusician);
            foreach (Musician_Track musicianTrack in musicianTracks)
            {
                _context.Entry(musicianTrack).State = EntityState.Deleted;
            }

        }

        public async Task DeleteMusician(int idMusician)
        {
            var musician = getMusicianAsync(idMusician);
            _context.Entry(musician).State = EntityState.Deleted;


        }

        public async Task<bool> DoesMusicianExist(int idMusician)
        {
            return await _context.Musicians.AnyAsync(e => e.IdMusician == idMusician);
        }

        public async Task<IEnumerable<Musician_Track>> GetMusicianTracks(int idMusician)
        {
            return await _context.Musician_Tracks.Where(e => e.IdMusician == idMusician).ToListAsync();
        }

        public async Task<IEnumerable<Track>> GetTracks(int idMusician)
        {
            return await _context.Musician_Tracks.Where(e => e.IdMusician == idMusician)
                .Include(e => e.Track).Select(e => e.Track).ToListAsync();
        }

        public async Task<bool> IsMusicianValidDoDelete(int idMusician)
        {
            var tracks = await GetTracks(idMusician);

            foreach (Track track in tracks)
            {
                if (track.IdMusicAlbum != null)
                    return false;
            }
            return true;
        }

        public async Task SaveDatabase()
        {
            await _context.SaveChangesAsync();
        }
    }
}
