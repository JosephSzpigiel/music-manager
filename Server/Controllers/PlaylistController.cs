using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using music_manager_starter.Data;
using music_manager_starter.Data.Models;
using System;

namespace music_manager_starter.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly DataDbContext _context;

        public PlaylistsController(DataDbContext context)
        {
            _context = context;
        }

  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            var results = await _context.Playlists
                .Include(p => p.Songs)
                .ToListAsync();
            return results;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(Guid id)
        {
            var playlist = await _context.Playlists
                                 .Include(p => p.Songs)  
                                 .FirstOrDefaultAsync(p => p.Id == id); 

            if (playlist == null)
            {
                return NotFound();
            }

            return playlist;
        }

        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
        {
            if (playlist == null)
            {
                return BadRequest("PlaylistSong cannot be null.");
            }

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("AddSong/{id}")]
        public async Task<IActionResult> AddSongToPlaylist(Guid id, Song songToAdd)
        {
            // Fetch the playlist by its ID
            var playlist = await _context.Playlists
                .Include(p => p.Songs)  // Include the Songs in the playlist
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playlist == null)
            {
                return NotFound();
            }

            // Check if the song already exists in the playlist
            var existingSong = playlist.Songs.FirstOrDefault(s => s.Id == songToAdd.Id);
            if (existingSong != null)
            {
                return BadRequest("This song is already added to the playlist.");
            }

            // Add the new song to the playlist's Songs collection
            playlist.Songs.Add(songToAdd);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(playlist);
        }

                // PUT: api/Playlists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylist(Guid id, Playlist playlist)
        {
            if (id != playlist.Id)
            {
                return BadRequest("Playlist ID mismatch.");
            }
            var existingPlaylist = await _context.Playlists.FindAsync(id);
            if (existingPlaylist == null)
            {
                return NotFound();
            }

            existingPlaylist.Title = playlist.Title;
            existingPlaylist.Songs = playlist.Songs;

            _context.Entry(existingPlaylist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(existingPlaylist); // 204 status code for successful update with no content to return
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 status code for successful deletion
        }

        private bool PlaylistExists(Guid id)
        {
            return _context.Playlists.Any(e => e.Id == id);
        }

    }
}
