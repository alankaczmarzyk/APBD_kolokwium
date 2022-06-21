using System;
using System.Collections.Generic;

namespace WebApplication1.DTOs
{
    public class Album
    {

        public string AlbumName { get; set; }
        public DateTime PublishDate { get; set; }
        public int IdMusicLabel { get; set; }
        public virtual ICollection<DTOs.Track> Tracks { get; set; }
   
    }
}
