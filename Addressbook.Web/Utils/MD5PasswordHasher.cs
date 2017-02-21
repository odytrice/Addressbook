using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Addressbook.Web.Utils
{
    public class MD5PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return
                System.Security
                    .Cryptography.MD5.Create()
                    .ComputeHash(Encoding.ASCII.GetBytes(password))
                    .Select(x => x.ToString("X2")).Aggregate((ag, s) => ag + s);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}