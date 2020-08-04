namespace Jlw.Standard.Utilities.Testing.Tests
{
    public interface ISampleModelForTesting
    {
        int PublicReadWriteInt { get; set; }
        int PublicReadInt { get; }
        int PublicWriteInt { set; }
        uint PublicUIntMax();
    }
}