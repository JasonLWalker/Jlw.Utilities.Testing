﻿using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.ProtectedInternalTests
{
    [TestClass]
    public class ProtectedInternalReadDoubleFixture : BasePropertyFixture<SampleModelForTesting, double>
    {
        
        public ProtectedInternalReadDoubleFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "ProtectedInternalReadDouble";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.ProtectedInternal)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(null)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}