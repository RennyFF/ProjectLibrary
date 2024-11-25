using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Utils.Types
{
    public class Book
    {
        public int Id { get; set; }
        public Author Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PagesCout { get; set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public DateTime AddedInDatabase { get; set; }
        public int RatingStars { get; set; }
    }
}
