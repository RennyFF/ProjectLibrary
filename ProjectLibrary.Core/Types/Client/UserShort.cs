using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Core.Types.Client
{
    public class UserShort
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PatronomycName { get; set; }
        public List<FavGenreType>? ClickedGenres { get; set; }
    }
}
