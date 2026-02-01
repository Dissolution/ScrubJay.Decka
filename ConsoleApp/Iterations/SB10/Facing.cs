using static ScrubJay.Decka.Sandbox.Iterations.SB10.Facing;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/>'s facing:
/// <list type="bullet">
/// <item><c>0b0_0_000000</c>🎴 Down</item>
/// <item><c>0b0_1_000000</c>🃏 Up</item>
/// </list>
/// </summary>
/// <remarks>
/// The 7th bit of a <see cref="Card"/>
/// </remarks>
/// <default><see cref="Facing.Down"/></default>
/// <mask><c>0b0_1_000000</c></mask>
[PublicAPI]
[Flags]
public enum Facing : byte
{
    Down = 0b0_0_000000,
    Up = 0b0_1_000000,
}

[PublicAPI]
public static class FacingExtensions
{
    static FacingExtensions()
    {
     
    }
    
    extension(Facing)
    {
        public static byte Mask => 0b0_1_000000;
        public static int BitCount => 1;
        public static Facing Default => Down;

        public static Result<Facing> TryParse(string str)
        {
            return DisplayFormat.For<Facing>().TryParse(str);
        }
    }

}