using System.Collections.Generic;

namespace WebApplication1.DTOs
{
    public class Musician
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public ICollection<DTOs.Track> Tracks { get; set; }
        

    }


}
