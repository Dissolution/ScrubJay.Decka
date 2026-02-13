
using static ScrubJay.Decka.Sandbox.Iterations.SB10.Facing;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/>'s facing:
/// <list type="table">
///     <listheader>
///         <term>Name</term>
///         <description><c>____Bits____  |  Value  |  Short  |  Unicode  |  Emoji</c></description>
///     </listheader>
///     <item>
///         <term>Down</term>
///         <description><c>0b0_0_000000  |  &#8199;&#8199;&#8199;&#8199;0  |  &#8199;&#8199;D&#8199;&#8199;  |  &#8199;&#8199;🂠&#8199;&#8199;&#8199;  |  &#8199;&#8199;🎴&#8199;</c></description>
///     </item>
///     <item>
///         <term>Up</term>
///         <description><c>0b0_1_000000  |  &#8199;&#8199;&#8199;64  |  &#8199;&#8199;U&#8199;&#8199;  |  &#8199;&#8199;&#8199;🂿&#8199;&#8199;&#8199;  |  &#8199;&#8199;🃏&#8199;</c></description>
///     </item>
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
    internal const byte MASK = 0b0_1_000000;
    
    static FacingExtensions()
    {
     
    }
    
    extension(Facing)
    {
        public static byte Mask => MASK;
        
        public static int BitCount => 1;
        
        public static Facing Default => Down;

        public static Facing[] Values => [Down, Up];
    }

    extension(Facing facing)
    {
        public void Match(Action? onDown, Action? onUp)
        {
            switch (facing)
            {
                case Down:
                    onDown?.Invoke();
                    return;
                case Up:
                    onUp?.Invoke();
                    return;
                default:
                    throw Ex.UndefinedEnum(facing);
            }
        }
        
        public R Match<R>(Func<R> onDown, Func<R> onUp) 
            => facing switch
            {
                Down => onDown(),
                Up => onUp(),
                _ => throw Ex.UndefinedEnum(facing),
            };
    }
}