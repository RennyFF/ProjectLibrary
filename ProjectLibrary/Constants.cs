using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary
{
    public static class Constants
    {
        public static string ConnectionStringDB { get; } = @"Server=localhost;Port=5432;User id=postgres;Password=root;Database=ProjectLibrary";
        public static int ActiveUserId { get; set; }
    }
}
