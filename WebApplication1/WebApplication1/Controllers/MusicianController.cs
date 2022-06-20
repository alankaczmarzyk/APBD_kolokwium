using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicianController : ControllerBase
    {
        private readonly IRepoService _service;

        public MusicianController(IRepoService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMusician(int id)
        {
            if (await _service.getMusicianAsync(id).FirstOrDefaultAsync() == null)
                return NotFound("Nie znaleziono albumu");

            return Ok(
                await _service.getMusicianAsync(id).Select(
                                e => new DTOs.Musician
                                {
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    Nickname = e.Nickname,
                                    Tracks = e.Musician_Tracks.Select(e=> new DTOs.Track
                                    {
                                        IdTrack = e.IdTrack,
                                        TrackName = e.Track.TrackName

                                    }).ToList()
                                }
                            ).ToListAsync()
                        );

        }
    }
}
