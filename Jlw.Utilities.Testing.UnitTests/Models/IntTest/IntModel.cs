using Jlw.Utilities.Data;
using System.Collections.Generic;

namespace Jlw.Utilities.Testing.UnitTests
{
    public class IntModel : NullModel, IIntModel
    {
        #region Fields
        public int _public = DataUtility.GenerateRandom<int>();
        private int _private = DataUtility.GenerateRandom<int>();
        private protected int _privateProtected = DataUtility.GenerateRandom<int>();
        protected int _protected = DataUtility.GenerateRandom<int>();
        protected internal int _protectedInternal = DataUtility.GenerateRandom<int>();
        internal int _internal = DataUtility.GenerateRandom<int>();

        public static int _publicStatic = DataUtility.GenerateRandom<int>();
        private static int _privateStatic = DataUtility.GenerateRandom<int>();
        private protected static int _privateProtectedStatic = DataUtility.GenerateRandom<int>();
        protected static int _protectedStatic = DataUtility.GenerateRandom<int>();
        protected internal static int _protectedInternalStatic = DataUtility.GenerateRandom<int>();
        internal static int _internalStatic = DataUtility.GenerateRandom<int>();

        public int _publicObject = DataUtility.GenerateRandom<int>();
        #endregion

        public int PublicNoTest { get; set; } = DataUtility.GenerateRandom<int>();

        public int PublicGetSet { get; set; } = DataUtility.GenerateRandom<int>();
        private int PrivateGetSet { get; set; }  = DataUtility.GenerateRandom<int>();
        private protected int PrivateProtectedGetSet { get; set; }= DataUtility.GenerateRandom<int>();
        protected int ProtectedGetSet { get; set; } = DataUtility.GenerateRandom<int>();
        protected internal int ProtectedInternalGetSet { get; set; } = DataUtility.GenerateRandom<int>();
        internal int InternalGetSet { get; set; } = DataUtility.GenerateRandom<int>();

        public static int PublicStaticGetSet { get; set; } = DataUtility.GenerateRandom<int>();
        private static int PrivateStaticGetSet { get; set; } = DataUtility.GenerateRandom<int>();
        private protected static int PrivateProtectedStaticGetSet { get; set; } = DataUtility.GenerateRandom<int>();
        protected static int ProtectedStaticGetSet { get; set; } = DataUtility.GenerateRandom<int>();
        protected internal static int ProtectedInternalStaticGetSet { get; set; } = DataUtility.GenerateRandom<int>();
        internal static int InternalStaticGetSet { get; set; } = DataUtility.GenerateRandom<int>();

        public int PublicGet { get; } = DataUtility.GenerateRandom<int>();
        private int PrivateGet { get; } = DataUtility.GenerateRandom<int>();
        private protected int PrivateProtectedGet { get; } = DataUtility.GenerateRandom<int>();
        protected int ProtectedGet { get; } = DataUtility.GenerateRandom<int>();
        protected internal int ProtectedInternalGet { get; } = DataUtility.GenerateRandom<int>();
        internal int InternalGet { get; } = DataUtility.GenerateRandom<int>();

        private int _backerInt = DataUtility.GenerateRandom<int>();

        public int PublicSet { set => _backerInt = value; }
        private int PrivateSet { set => _backerInt = value; }
        private protected int PrivateProtectedSet { set => _backerInt = value; }
        protected int ProtectedSet { set => _backerInt = value; }
        protected internal int ProtectedInternalSet { set => _backerInt = value; }
        internal int InternalSet { set => _backerInt = value; }
        
        public string GetHash
        {
            get => GenerateToken("token", _public.ToString(), PrivateGet);
        }

        #region Constructors
        static IntModel() { }

        public IntModel() { }

        public IntModel(short s)
        {
            PublicGet = s;
            PublicGetSet = s;
            PublicStaticGetSet = s;
            
            _public = s;
            _publicObject = s;
            _publicStatic = s;
        }

        protected IntModel(int i) { }

        private IntModel(long l) { }
        #endregion

        protected string GenerateToken(string type, string id, int id2)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(id) || id2 <= 0)
                return DataUtility.Md5Hash(DataUtility.GenerateSalt()); // Generate a random hash that will not be able to be matched.

            var salt = type.ToLower() + "-" + _public + "-" + id2.ToString("D");

            return DataUtility.Md5Hash(salt + id);
        }


    }
}
