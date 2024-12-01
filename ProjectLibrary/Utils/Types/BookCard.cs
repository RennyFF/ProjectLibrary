using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Utils.Types
{
    public class BookCard
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public byte[] ImageSource { get; set; }
        public int RatingStars { get; set; }
        public DateTime AddedInDatabase { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
