namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

[PublicAPI]
public static class DisplayFormatExtensions
{
    extension(DisplayFormat)
    {
        public static PartFormatDisplayMap<TEnum> For<TEnum>()
            where TEnum : struct, Enum
        {
            return PartFormatDisplayMap<TEnum>.Instance;
        }

    }
}