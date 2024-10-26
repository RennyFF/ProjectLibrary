﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Utils
{
    public static class UnixTimeConverter
    {
        private static DateTime dateTime { get; } = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static DateTime TimeStampToDateTime(int UnixTimeStamp)
        {
            DateTime Temporary = dateTime.AddSeconds(UnixTimeStamp).ToLocalTime();
            return Temporary;
        }
        public static int DateTimeToTimeStamp(DateTime DateTime)
        {
            return (int)((DateTimeOffset)DateTime).ToUnixTimeSeconds();
        }
    }
}
