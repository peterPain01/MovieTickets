using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.Utils
{
    public static class helper
    {
        public static bool CompareSecureStrings(SecureString s1, SecureString s2)
        {
            // If lengths are different, the strings can't be equal
            if (s1.Length != s2.Length)
                return false;

            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;
            try
            {
                // Marshal SecureStrings into BSTRs
                bstr1 = Marshal.SecureStringToBSTR(s1);
                bstr2 = Marshal.SecureStringToBSTR(s2);

                // Compare BSTRs
                return Marshal.PtrToStringBSTR(bstr1) == Marshal.PtrToStringBSTR(bstr2);
            }
            finally
            {
                // Free the memory allocated for BSTRs
                if (bstr1 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(bstr1);
                if (bstr2 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(bstr2);
            }
        }

        public static string HassPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPassword = hash.ComputeHash(passwordBytes); 
        
            return Convert.ToHexString(hashedPassword);
        }
    }
}
