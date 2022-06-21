using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IRepoService _service;

        public AlbumController(IRepoService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMusician(int id)
        {
            return Ok(
                  await _service.getAlbumAsync(id).Select(e=> new DTOs.Album
                  {
                      AlbumName = e.AlbumName,
                      PublishDate = e.PublishDate,
                      Tracks = e.Tracks.Select(e => new DTOs.Track
                      {
                          TrackName = e.TrackName,
                          IdTrack = e.IdTrack,
                          Duration = e.Duration
                      }).ToList()

                  
                  }).ToListAsync()
                );
        }
    }
}