using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music_manager_starter.Data.Models
{
    public sealed class Playlist
    {
        public Guid Id { get; set; }
        public string Title {get; set; }
        public List<Song> Songs { get; set; } = [];
    }
}
