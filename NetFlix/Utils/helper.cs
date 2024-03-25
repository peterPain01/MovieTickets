using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetFlix.Utils
{
    public static class helper
    {
        public static bool CompareSecureStrings(SecureString s1, SecureString s2)
        {
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
        
        // 0 - Horizo 
        // 1 - vertical 
        public static string CreateImagePath(string sourcePath, int mode)
        {
            Random random = new Random();
            string destinationFilePath = ""; 
            if (mode  == 0)
            {
                 destinationFilePath = @"C:\Learning\School\CURRENT\Window\Project\NetFlix\NetFlix\Images\slider-" + random.NextDouble() + Path.GetExtension(sourcePath);
            }
            if(mode == 1)
            {
                destinationFilePath = @"C:\Learning\School\CURRENT\Window\Project\NetFlix\NetFlix\Images\mv-" + random.NextDouble() + Path.GetExtension(sourcePath);
            }
            File.Copy(sourcePath, destinationFilePath);
            return destinationFilePath; 
        }
    }
}
