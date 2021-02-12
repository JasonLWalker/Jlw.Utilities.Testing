using System;

namespace Jlw.Utilities.Testing.Tests
{
    public class SampleModelForTesting : ISampleModelForTesting
    {

        #region Internal Fields
            public int _publicInt = int.MinValue;
            private short _privateShort = short.MinValue;
            protected long _protectedLong = long.MinValue;
            internal sbyte _internalSByte = sbyte.MinValue;
            private protected float _privateProtectedFloat = float.MinValue;
            protected internal double _protectedInternalDouble = double.MinValue;

            public static int _publicStaticInt = int.MaxValue;
            private static short _privateStaticShort = short.MaxValue;
            protected static long _protectedStaticLong = long.MaxValue;
            internal static sbyte _internalStaticSByte = sbyte.MaxValue;
            private protected static float _privateProtectedStaticFloat = float.MaxValue;
            protected internal static double _protectedInternalStaticDouble = double.MaxValue;
        #endregion

        #region Internal Properties
            internal sbyte InternalReadSByte { get; } = sbyte.MaxValue;
            internal sbyte InternalReadWriteSByte { get; set; } = sbyte.MinValue;
            internal static sbyte InternalStaticReadWriteSByte { get; set; } = sbyte.MinValue;
            internal sbyte InternalWriteSByte
            {
                set => _internalSByte = value;
            }
        #endregion

        #region Private Protected Properties
            private protected float PrivateProtectedReadFloat { get; } = float.MaxValue;
            private protected static float PrivateProtectedStaticReadWriteFloat { get; set; } = float.MinValue;
            private protected float PrivateProtectedReadWriteFloat { get; set; } = float.MinValue;

            private protected float PrivateProtectedWriteFloat
            {
                set => _privateProtectedFloat = value;
            }
        #endregion

        #region Private Properties
            private short PrivateReadShort { get; } = short.MaxValue;
            private short PrivateReadWriteShort { get; set; } = short.MinValue;

            private static short PrivateStaticReadWriteShort { get; set; } = short.MinValue;
            
            private short PrivateWriteShort
            {
                set => _privateShort = value;
            }
        #endregion

        #region Protected Properties
            protected static long ProtectedStaticReadWriteLong { get; set; } = long.MinValue;
            protected long ProtectedReadWriteLong { get; set; } = long.MinValue;
            protected long ProtectedReadLong { get; } = long.MaxValue;
            protected long ProtectedWriteLong
            {
                set => _protectedLong = value;
            }

        #endregion

        #region Protected Internal Properties
            protected internal static double ProtectedInternalStaticReadWriteDouble { get; set; } = double.MinValue;
            protected internal double ProtectedInternalReadWriteDouble { get; set; } = double.MinValue;
            protected internal double ProtectedInternalReadDouble { get; } = double.MaxValue;
            protected internal double ProtectedInternalWriteDouble
            {
                set => _protectedInternalDouble = value;
            }

        #endregion

        #region Public Properties
            public static int PublicStaticReadWriteInt { get; set; } = int.MinValue;

            public int PublicReadWriteInt { get; set; } = int.MinValue;

            public int PublicReadInt { get; } = int.MaxValue;


            public int PublicWriteInt
            {
                set => _publicInt = value;
            } 


        #endregion

        #region Methods

            public uint PublicUIntMax() => uint.MaxValue;

            private ushort PrivateUShortMax() => ushort.MaxValue;

            protected ulong ProtectedULongMax() => ulong.MaxValue;

            internal byte InternalByteMax() => byte.MaxValue;

            private protected float PrivateProtectedFloatInfinity() => float.PositiveInfinity;

            protected internal double ProtectedInternalDoubleInfinity() => double.PositiveInfinity;

            public DateTime? PublicNullDateTest => null;
        

        #endregion
    }
}
