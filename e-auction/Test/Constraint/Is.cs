using System;
using System.Collections.Generic;
using System.Text;
using user.Models;

namespace Test.UserConstraints
{

    // This class allows the use of Custom Constraint Extensions with 'Is.' or 'Is.Not.' Comparisions
    internal class Is : NUnit.Framework.Is
    {
        public static IdenticalUserConstraint IdenticalTo(UserDetails expected)
        {
            return new IdenticalUserConstraint(expected);
        }
    }

}
