using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace music_manager_starter.Shared
{
  public sealed class Playlist
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public List<Song> Songs { get; set; } = [];
    }
}
