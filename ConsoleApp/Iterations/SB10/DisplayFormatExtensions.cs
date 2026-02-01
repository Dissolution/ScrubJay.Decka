namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

[PublicAPI]
public static class DisplayFormatExtensions
{
    private static readonly int _count = Enum.GetValues<DisplayFormat>().Length;
    
    extension(DisplayFormat)
    {
        public static int TotalFormatCount => _count;

        public static DisplayFormatMap<P> For<P>()
            where P : struct, Enum
        {
            return DisplayFormatMap<P>.Default;
        }

    }
}