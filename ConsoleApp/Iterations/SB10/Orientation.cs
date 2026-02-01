using static ScrubJay.Decka.Sandbox.Iterations.SB10.Orientation;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/>'s orientation:
/// <list type="bullet">
/// <item><c>0b0_0000000</c>⬆️ Upright</item>
/// <item><c>0b1_0000000</c>🔃 Reversed</item>
/// </list>
/// </summary>
/// <remarks>
/// The 8th bit of a <see cref="Card"/>
/// </remarks>
/// <default><see cref="Orientation.Upright"/></default>
/// <mask><c>0b1_0000000</c></mask>
[PublicAPI]
[Flags]
public enum Orientation : byte
{
    Upright = 0b0_0000000,
    
    Reversed = 0b1_0000000,
}

[PublicAPI]
public static class OrientationExtensions
{
    static OrientationExtensions()
    {
     
    }
    
    extension(Orientation)
    {
        public static byte Mask => 0b1_0000000;
        public static int BitCount => 1;
        public static Orientation Default => Upright;

        public static Result<Orientation> TryParse(string str)
        {
            return DisplayFormat.For<Orientation>().TryParse(str);
        }
    }

}