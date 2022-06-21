using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
                return NotFound("Nie znaleziono muzyka");

            return Ok(
                await _service.getMusicianAsync(id).Select(e => new DTOs.Musician
                                {
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    Nickname = e.Nickname,
                                    Tracks = e.Musician_Tracks.Select(e=> new DTOs.Track
                                    {
                                        IdTrack = e.IdTrack,
                                        TrackName = e.Track.TrackName,
                                        Duration = e.Track.Duration
                                    }).ToList()
                                }
                            ).ToListAsync()
                        );

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMusician(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _service.DoesMusicianExist(id))
                return NotFound("Musician does not exist");

            if (!await _service.IsMusicianValidDoDelete(id))
                return NotFound("Cannot delete this musician");


            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _service.DeleteMusiciansTracks(id);
                    await _service.SaveDatabase();
                    await _service.DeleteMusician(id);
                    scope.Complete();
                }
                catch (Exception)
                {
                    return Problem("Unexpected problem with database");
                }
            }
            await _service.SaveDatabase();
            return NoContent();
        }


        
        }




    
}
