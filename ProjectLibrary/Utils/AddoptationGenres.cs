using ProjectLibrary.Utils.Types;
using System.Text;

namespace ProjectLibrary.Utils
{
    public static class AddoptationGenres
    {
        public static List<Genre>? FromStringToGenresList(string? Genres, List<Genre> AllGenres)
        {
            if(string.IsNullOrEmpty(Genres)) return null;
            List<Genre> ResultList = new List<Genre>();
            string[] SplittedByCategories = Genres.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach(string Category in SplittedByCategories)
            {
                Genre TempGenre = new();
                string[] SplittedByClicks = Category.Split('!');
                int CategoryId = int.Parse(SplittedByClicks[0]);
                int ClickCount = int.Parse(SplittedByClicks[1]);
                TempGenre.ClickedCountity = ClickCount;
                TempGenre.Id = CategoryId;
                TempGenre.GenreName = AllGenres.Where(i => i.Id == CategoryId).First().GenreName;
                ResultList.Add(TempGenre);
            }
            return ResultList ;
        }
        public static string? FromGenresListToString(List<Genre> UserGenre)
        {
            StringBuilder result = new StringBuilder();
            foreach (Genre item in UserGenre)
            {
                result.Append(item.Id+ "!" + item.ClickedCountity + ";");
            }
            return result.ToString();
        }

    }
}
