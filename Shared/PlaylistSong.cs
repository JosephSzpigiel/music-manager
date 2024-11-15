using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace music_manager_starter.Shared
{
  public sealed class PlaylistSong
    {
        public Guid Id { get; set; }

        public string SongId { get; set; }
        public string PlaylistId { get; set; }
    }
}
