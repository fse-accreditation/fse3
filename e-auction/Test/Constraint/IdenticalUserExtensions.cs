using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Test.UserConstraints;
using user.Models;

namespace test.UserConstraints
{
    // This class contains Constraint extension for comparing two contacts using Custom Constraints
    internal static class IdenticalUserExtensions
    {
        public static IdenticalUserConstraint IdenticalTo(this ConstraintExpression expression, UserDetails expected)
        {
            var constraint = new IdenticalUserConstraint(expected);
            expression.Append(constraint);
            return constraint;
        }
    }
}
