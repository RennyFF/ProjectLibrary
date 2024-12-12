namespace ProjectLibrary.Core.Converters
{
    public static class UnixTimeConverter
    {
        private static DateTime dateTime { get; } = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static DateTime TimeStampToDateTime(int UnixTimeStamp)
        {
            DateTime Temporary = dateTime.AddSeconds(UnixTimeStamp).ToUniversalTime();
            return Temporary;
        }
        public static DateTime TimeStampToDateTime(long UnixTimeStamp)
        {
            DateTime Temporary = dateTime.AddSeconds(UnixTimeStamp).ToUniversalTime();
            return Temporary;
        }
        public static int DateTimeToTimeStamp(DateTime DateTime)
        {
            return (int)((DateTimeOffset)DateTime).ToUnixTimeSeconds();
        }
    }
}
