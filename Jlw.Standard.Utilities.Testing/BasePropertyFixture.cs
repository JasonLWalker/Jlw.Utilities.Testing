using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing
{
    [TestClass]
    public class BasePropertyFixture<TModel, TProperty> : BaseModelFixture<TModel> 
        where TModel : class, new() 
    {
        protected static string PropertyName = "";
        protected static TProperty PropertyInstance = default;

        [TestMethod]
        public virtual void Should_Exist()
        {
            AssertPropertyExists(PropertyName);
        }

        [TestMethod]
        public virtual void Should_Match_ForPropertyType()
        {
            AssertTypeAssignmentForObjectProperty(DefaultInstance, PropertyName, typeof(TProperty), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        }

        public override void Should_BeInstanceOf(Type t)
        {
            Assert.IsNotNull(t);
            AssertTypeAssignmentForObjectProperty(DefaultInstance, PropertyName, t, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        }

        [TestMethod]
        public virtual void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            var propInfo = AssertPropertyScopeForGetAccessor(PropertyName, attr);
        }


        [TestMethod]
        public virtual void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            var propInfo = AssertPropertyScopeForSetAccessor(PropertyName, attr);
        }

        public virtual void Should_MatchValue(TModel o, TProperty expected)
        {
            var p = AssertPropertyIsReadable(PropertyName);
            TProperty v = (TProperty)p.GetValue(o);
            Assert.AreEqual(expected, v);
        }



    }
}