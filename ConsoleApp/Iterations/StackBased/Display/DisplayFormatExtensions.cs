namespace ScrubJay.Decka.Sandbox.Iterations.StackBased.Display;

[PublicAPI]
public static class DisplayFormatExtensions
{
    private static readonly int _count = Enum.GetValues<DisplayFormat>().Length;
    
    extension(DisplayFormat)
    {
        public static int TotalFormatCount => _count;

        public static DisplayFormatMap<P> For<P>()
            where P : struct, ICardPart<P>
        {
            return DisplayFormatMap<P>.Default;
        }

    }
}