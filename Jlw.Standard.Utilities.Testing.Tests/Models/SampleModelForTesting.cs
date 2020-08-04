namespace Jlw.Standard.Utilities.Testing.Tests
{
    public class SampleModelForTesting : ISampleModelForTesting
    {
        internal int _publicWriteInt = int.MinValue;
        internal short _privateWriteShort = short.MinValue;
        internal long _protectedWriteLong = long.MinValue;
        internal sbyte _internalWriteByte = sbyte.MinValue;
        internal float _privateProtectedWriteFloat = float.MinValue;
        internal double _privateInternalWriteDouble = double.MinValue;

        public static int PublicStaticReadWriteInt { get; set; } = int.MinValue;
        private static short PrivateStaticReadWriteShort { get; set; } = short.MinValue;
        protected static long ProtectedStaticReadWriteLong { get; set; } = long.MinValue;
        internal static sbyte InternalStaticReadWriteSByte { get; set; } = sbyte.MinValue;
        private protected static float PrivateProtectedStaticReadWriteFloat { get; set; } = float.MinValue;
        protected internal static double ProtectedInternalStaticReadWriteDouble { get; set; } = double.MinValue;


        public int PublicReadWriteInt { get; set; } = int.MinValue;
        private short PrivateReadWriteShort { get; set; } = short.MinValue;
        protected long ProtectedReadWriteLong { get; set; } = long.MinValue;
        internal sbyte InternalReadWriteSByte { get; set; } = sbyte.MinValue;
        private protected float PrivateProtectedReadWriteFloat { get; set; } = float.MinValue;
        protected internal double ProtectedInternalReadWriteDouble { get; set; } = double.MinValue;

        public int PublicReadInt { get; } = int.MaxValue;
        private short PrivateReadShort { get; } = short.MaxValue;
        protected long ProtectedReadLong { get; } = long.MaxValue;
        internal sbyte InternalReadSByte { get; } = sbyte.MaxValue;
        private protected float PrivateProtectedReadFloat { get; } = float.MaxValue;
        protected internal double ProtectedInternalReadDouble { get; } = double.MaxValue;

        public int PublicWriteInt
        {
            set => _publicWriteInt = value;
        } 

        private short PrivateWriteShort
        {
            set => _privateWriteShort = value;
        }

        protected long ProtectedWriteLong
        {
            set => _protectedWriteLong = value;
        }

        internal sbyte InternalWriteByte
        {
            set => _internalWriteByte = value;
        }

        private protected float PrivateProtectedWriteFloat
        {
            set => _privateProtectedWriteFloat = value;
        }

        protected internal double ProtectedInternalWriteDouble
        {
            set => _privateInternalWriteDouble = value;
        }

        public uint PublicUIntMax() => uint.MaxValue;

        private ushort PrivateUShortMax() => ushort.MaxValue;

        protected ulong ProtectedULongMax() => ulong.MaxValue;

        internal byte InternalByteMax() => byte.MaxValue;

        private protected float PrivateProtectedFloatInfinity() => float.PositiveInfinity;

        protected internal double ProtectedInternalDoubleInfinity() => double.PositiveInfinity;


    }
}
