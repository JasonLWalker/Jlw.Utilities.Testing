using System;

namespace Jlw.Utilities.Testing.Tests
{
    public class SampleModelForTesting : ISampleModelForTesting
    {

        #region Internal Fields
            internal int _publicWriteInt = int.MinValue;
            internal short _privateWriteShort = short.MinValue;
            internal long _protectedWriteLong = long.MinValue;
            internal sbyte _internalWriteSByte = sbyte.MinValue;
            internal float _privateProtectedWriteFloat = float.MinValue;
            internal double _privateInternalWriteDouble = double.MinValue;
        #endregion

        #region Internal Properties
            internal sbyte InternalReadSByte { get; } = sbyte.MaxValue;
            internal sbyte InternalReadWriteSByte { get; set; } = sbyte.MinValue;
            internal static sbyte InternalStaticReadWriteSByte { get; set; } = sbyte.MinValue;
            internal sbyte InternalWriteSByte
            {
                set => _internalWriteSByte = value;
            }
        #endregion

        #region Private Protected Properties
            private protected float PrivateProtectedReadFloat { get; } = float.MaxValue;
            private protected static float PrivateProtectedStaticReadWriteFloat { get; set; } = float.MinValue;
            private protected float PrivateProtectedReadWriteFloat { get; set; } = float.MinValue;

            private protected float PrivateProtectedWriteFloat
            {
                set => _privateProtectedWriteFloat = value;
            }
        #endregion

        #region Private Properties
            private short PrivateReadShort { get; } = short.MaxValue;
            private short PrivateReadWriteShort { get; set; } = short.MinValue;

            private static short PrivateStaticReadWriteShort { get; set; } = short.MinValue;
            
            private short PrivateWriteShort
            {
                set => _privateWriteShort = value;
            }
        #endregion

        #region Protected Properties
            protected static long ProtectedStaticReadWriteLong { get; set; } = long.MinValue;
            protected long ProtectedReadWriteLong { get; set; } = long.MinValue;
            protected long ProtectedReadLong { get; } = long.MaxValue;
            protected long ProtectedWriteLong
            {
                set => _protectedWriteLong = value;
            }

        #endregion

        #region Protected Internal Properties
            protected internal static double ProtectedInternalStaticReadWriteDouble { get; set; } = double.MinValue;
            protected internal double ProtectedInternalReadWriteDouble { get; set; } = double.MinValue;
            protected internal double ProtectedInternalReadDouble { get; } = double.MaxValue;
            protected internal double ProtectedInternalWriteDouble
            {
                set => _privateInternalWriteDouble = value;
            }

        #endregion

        #region Public Properties
            public static int PublicStaticReadWriteInt { get; set; } = int.MinValue;

            public int PublicReadWriteInt { get; set; } = int.MinValue;

            public int PublicReadInt { get; } = int.MaxValue;


            public int PublicWriteInt
            {
                set => _publicWriteInt = value;
            } 


        #endregion

        #region Methods

            public uint PublicUIntMax() => uint.MaxValue;

            private ushort PrivateUShortMax() => ushort.MaxValue;

            protected ulong ProtectedULongMax() => ulong.MaxValue;

            internal byte InternalByteMax() => byte.MaxValue;

            private protected float PrivateProtectedFloatInfinity() => float.PositiveInfinity;

            protected internal double ProtectedInternalDoubleInfinity() => double.PositiveInfinity;

            public DateTime? NullDateTest => null;
        

        #endregion
    }
}
