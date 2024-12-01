using Microsoft.Windows.Themes;
using Newtonsoft.Json;
using ProjectLibrary.Utils.Components.CustomView;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Utils
{
    public class BookHistory
    {
        public int BookId { get; set; }
        public DateTime AddedInHistory { get; set; }
    }
    public class AuthorHistory
    {
        public int AuthorId { get; set; }
        public DateTime AddedInHistory { get; set; }
    }
    public class GenreHistory
    {
        public int GenreId { get; set; }
        public DateTime AddedInHistory { get; set; }
    }
    public class HistoryStruct
    {
        public List<BookHistory> bookHistory { get; set; } = new List<BookHistory>();
        public List<AuthorHistory> authorHistory { get; set; } = new List<AuthorHistory>();
        public List<GenreHistory> genreHistory { get; set; } = new List<GenreHistory>();
    }
    public enum HistoryType
    {
        Book,
        Genre,
        Author
    }
    public static class HistoryCache
    {
        private static string CachePath { get; set; } = Path.Combine(Constants.CachePath, "LibraryCave");
        private static string CacheFile { get; set; } = Path.Combine(CachePath, "history.json");
        public static HistoryStruct GetHistory()
        {
            var History = File.ReadAllText(CacheFile);
            return JsonConvert.DeserializeObject<HistoryStruct>(History) ?? new HistoryStruct();
        }
        public static void CreateCacheJSON()
        {
            CreateCacheDirectory();
            if (!File.Exists(CacheFile))
            {
                File.WriteAllText(CacheFile, JsonConvert.SerializeObject(new HistoryStruct()));
            }
        }
        private static void CreateCacheDirectory()
        {
            if (!Directory.Exists(CachePath))
            {
                Directory.CreateDirectory(CachePath);
            }
        }
        public static async void AppendHistoryCache(int Id, HistoryType historyType)
        {
            CreateCacheJSON();
            HistoryStruct History = GetHistory();
            switch (historyType)
            {
                case HistoryType.Book:
                    BookHistory? BHistoryPrevious = History.bookHistory.Where(i => i.BookId == Id).FirstOrDefault();
                    if (BHistoryPrevious != null) { History.bookHistory.Remove(BHistoryPrevious); };
                    int LengthBookHistory = History.bookHistory.Count;
                    if (LengthBookHistory == 10)
                    {
                        History.bookHistory.RemoveAt(LengthBookHistory - 1);
                    }
                    History.bookHistory.Insert(0, new BookHistory { BookId = Id, AddedInHistory = DateTime.Now });
                    break;
                case HistoryType.Genre:
                    GenreHistory? GHistoryPrevious = History.genreHistory.Where(i => i.GenreId == Id).FirstOrDefault();
                    if (GHistoryPrevious != null) { History.genreHistory.Remove(GHistoryPrevious); };
                    int LengthGenreHistory = History.genreHistory.Count;
                    if (LengthGenreHistory == 10)
                    {
                        History.genreHistory.RemoveAt(LengthGenreHistory - 1);
                    }
                    History.genreHistory.Insert(0, new GenreHistory { GenreId = Id, AddedInHistory = DateTime.Now });
                    break;
                case HistoryType.Author:
                    AuthorHistory? AHistoryPrevious = History.authorHistory.Where(i => i.AuthorId == Id).FirstOrDefault();
                    if (AHistoryPrevious != null) { History.authorHistory.Remove(AHistoryPrevious); };
                    int LengthAuthHistory = History.authorHistory.Count;
                    if (LengthAuthHistory == 10)
                    {
                        History.authorHistory.RemoveAt(LengthAuthHistory - 1);
                    }
                    History.authorHistory.Insert(0, new AuthorHistory { AuthorId = Id, AddedInHistory = DateTime.Now });
                    break;
                default:
                    break;
            }
            File.WriteAllText(CacheFile, JsonConvert.SerializeObject(History));
        }
        public static async void AppendHistoryCacheAll(BookCard Card)
        {
            await Task.Run(() => AppendHistoryCache(Card.BookId, HistoryType.Book));
            await Task.Run(() => AppendHistoryCache(Card.AuthorId, HistoryType.Author));
            await Task.Run(() => AppendHistoryCache(Card.GenreId, HistoryType.Genre));
        }
    }
}
