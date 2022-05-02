using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using user.Models;

namespace Test.UserConstraints
{
    // This class compares two users by their emails.
    // If two contacts have same emails, only then they are treated as equal
    internal class UserComparer : IComparer<UserDetails>
    {
        public int Compare([AllowNull] UserDetails x, [AllowNull] UserDetails y)
        {
            return x.Email.CompareTo(y.Email);
        }
    }
}