using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing
{
    [TestClass]
    public class BasePropertyFixture<TModel, TProperty> : BaseModelFixture<TModel> 
        where TModel : class, new() 
    {
        protected string PropertyName = "";
        protected TProperty PropertyInstance = default;

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


        /// <summary>
        /// Test method that should be overridden in descendant class with correct DataRow attributes for derived class.
        /// Can call base method in overridden class.
        /// </summary>
        /// <param name="t"></param>
        public override void Should_BeInstanceOf(Type t)
        {
            Assert.IsNotNull(t);
            AssertTypeAssignmentForObjectProperty(DefaultInstance, PropertyName, t, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        }


        /// <summary>
        /// Test method that should be overridden in descendant class with correct DataRow attributes for derived class.
        /// Can call base method in overridden class.
        /// </summary>
        /// <param name="attr"></param>
        [TestMethod]
        [DataRow(MethodAttributes.Public)]
        public virtual void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            var propInfo = AssertPropertyScopeForGetAccessor(PropertyName, attr);
        }


        /// <summary>
        /// Test method that should be overridden in descendant class with correct DataRow attributes for derived class.
        /// Can call base method in overridden class.
        /// </summary>
        /// <param name="attr"></param>
        [TestMethod]
        [DataRow(MethodAttributes.Public)]
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