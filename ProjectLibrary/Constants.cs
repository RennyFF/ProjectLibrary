namespace ProjectLibrary
{
    public enum PreviousViewModels
    {
        CatalogVM,
        MainVM,
        HistoryVM,
        AuthorPreviewVM,
        GenrePreviewVM,
        BookPreviewVM,
        AuthorsVM,
        GenresVM,
        FavoriteVM,
        SearchVM
    }
    public static class Constants
    {
        public static List<PreviousViewModels?> PreviousVM { get; set; }
        public static string ServerAdress { get; } = "https://localhost:7210";
        public static string CachePath { get; } = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%");
        public static int ActiveUserId { get; set; }
        public static int CountityOnPage { get; } = 27;
    }
}
