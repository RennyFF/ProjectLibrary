using Npgsql.Internal;
using ProjectLibrary.MVVM.View.CoreViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Utils.Converters
{
    public static class PasswordConverters
    {
        public static string? GetPasswordFromSecureString(object Parameter)
        {
            var passwordContainer = Parameter as IAuthPassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                return ConvertToUnsecureString(secureString);
            }
            return null;
        }
        private static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            nint unmanagedString = nint.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        public static string FromPasswordToHash(string Input)
        {
            using (HashAlgorithm Algo = SHA256.Create())
            {
                byte[] Hash = Algo.ComputeHash(Encoding.UTF8.GetBytes(Input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in Hash)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }
    }
}
