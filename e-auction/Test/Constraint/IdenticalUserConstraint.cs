using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Constraints;
using user.Models;

namespace Test.UserConstraints
{

    // This class contains logic to compare two contacts and use it for creating Customized Constraint for Assertion
    internal class IdenticalUserConstraint : Constraint
    {
        private readonly UserDetails expectedUser;

        public IdenticalUserConstraint(UserDetails expectedValue)
        {
            this.expectedUser = expectedValue;
        }

        public override string Description { get => base.Description; protected set => base.Description = value; }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            var result = NUnit.Framework.Is.EqualTo(this.expectedUser).Using(new UserComparer()).ApplyTo(actual).IsSuccess;
            var constraintResult = new ConstraintResult(this, actual, result);

            if (!constraintResult.IsSuccess)
                this.Description = string.Format("Expected {0} and Actual {1} should be same", this.expectedUser, actual);

            return constraintResult;
        }
    }
}

