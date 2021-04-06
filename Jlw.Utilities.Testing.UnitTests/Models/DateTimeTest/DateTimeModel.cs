using System;
using Jlw.Utilities.Data;

namespace Jlw.Utilities.Testing.UnitTests
{
    public class DateTimeModel : NullModel
    {
        #region Fields
        public DateTime _public = DataUtility.GenerateRandom<DateTime>();
        private DateTime _private = DataUtility.GenerateRandom<DateTime>();
        private protected DateTime _privateProtected = DataUtility.GenerateRandom<DateTime>();
        protected DateTime _protected = DataUtility.GenerateRandom<DateTime>();
        protected internal DateTime _protectedInternal = DataUtility.GenerateRandom<DateTime>();
        internal DateTime _internal = DataUtility.GenerateRandom<DateTime>();

        public static DateTime _publicStatic = DataUtility.GenerateRandom<DateTime>();
        private static DateTime _privateStatic = DataUtility.GenerateRandom<DateTime>();
        private protected static DateTime _privateProtectedStatic = DataUtility.GenerateRandom<DateTime>();
        protected static DateTime _protectedStatic = DataUtility.GenerateRandom<DateTime>();
        protected internal static DateTime _protectedInternalStatic = DataUtility.GenerateRandom<DateTime>();
        internal static DateTime _internalStatic = DataUtility.GenerateRandom<DateTime>();

        public DateTime _publicObject = DataUtility.GenerateRandom<DateTime>();
        #endregion

        public DateTime PublicNoTest { get; set; } = DataUtility.GenerateRandom<DateTime>();

        public DateTime PublicGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();
        private DateTime PrivateGetSet { get; set; }  = DataUtility.GenerateRandom<DateTime>();
        private protected DateTime PrivateProtectedGetSet { get; set; }= DataUtility.GenerateRandom<DateTime>();
        protected DateTime ProtectedGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();
        protected internal DateTime ProtectedInternalGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();
        internal DateTime InternalGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();

        public static DateTime PublicStaticGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();
        private static DateTime PrivateStaticGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();
        private protected static DateTime PrivateProtectedStaticGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();
        protected static DateTime ProtectedStaticGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();
        protected internal static DateTime ProtectedInternalStaticGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();
        internal static DateTime InternalStaticGetSet { get; set; } = DataUtility.GenerateRandom<DateTime>();

        public DateTime PublicGet { get; } = DataUtility.GenerateRandom<DateTime>();
        private DateTime PrivateGet { get; } = DataUtility.GenerateRandom<DateTime>();
        private protected DateTime PrivateProtectedGet { get; } = DataUtility.GenerateRandom<DateTime>();
        protected DateTime ProtectedGet { get; } = DataUtility.GenerateRandom<DateTime>();
        protected internal DateTime ProtectedInternalGet { get; } = DataUtility.GenerateRandom<DateTime>();
        internal DateTime InternalGet { get; } = DataUtility.GenerateRandom<DateTime>();

        private DateTime _backerInt = DataUtility.GenerateRandom<DateTime>();

        public DateTime PublicSet { set => _backerInt = value; }
        private DateTime PrivateSet { set => _backerInt = value; }
        private protected DateTime PrivateProtectedSet { set => _backerInt = value; }
        protected DateTime ProtectedSet { set => _backerInt = value; }
        protected internal DateTime ProtectedInternalSet { set => _backerInt = value; }
        internal DateTime InternalSet { set => _backerInt = value; }

        #region Constructors
        static DateTimeModel() { }

        public DateTimeModel() { }
        #endregion
    }
}
