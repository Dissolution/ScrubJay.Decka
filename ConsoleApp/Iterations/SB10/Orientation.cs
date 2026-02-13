using ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;
using static ScrubJay.Decka.Sandbox.Iterations.SB10.Orientation;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/>'s orientation:
/// <list type="table">
///     <listheader>
///         <term>Name</term>
///         <description><c>____Bits___  |  Value  |  Short  |  Unicode  |  Emoji</c></description>
///     </listheader>
///     <item>
///         <term>Upright</term>
///         <description><c>0b0_0000000  |  &#8199;&#8199;&#8199;&#8199;0  |  &#8199;&#8199;U&#8199;&#8199;  |  &#8199;&#8199;&#8199;↑&#8199;&#8199;&#8199;  |  &#8199;&#8199;⬆️&#8199;</c></description>
///     </item>
///     <item>
///         <term>Reversed</term>
///         <description><c>0b1_0000000  |  &#8199;&#8199;128  |  &#8199;&#8199;R&#8199;&#8199;  |  &#8199;&#8199;&#8199;↓&#8199;&#8199;&#8199;  |  &#8199;&#8199;⬇️&#8199;</c></description>
///     </item>
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
    internal const byte MASK = 0b1_0000000;
    
    static OrientationExtensions()
    {
        DisplayFormat
            .Register<Orientation>()
            .FormatAs(Upright, nameof(Upright), "U", "↑", "⬆️")
            .FormatAs(Reversed, nameof(Reversed), "R", "↓", "⬇️");
    }
    
    extension(Orientation)
    {
        public static Card operator |(Orientation orientation, Card card) => card.With(orientation);

        public static Card operator +(Orientation orientation, Card card) => card.With(orientation);


        public static byte Mask => MASK;
        
        public static int BitCount => 1;
        
        public static Orientation Default => Upright;

        public static Orientation[] Values => [Upright, Reversed];
    }

    extension(Orientation orientation)
    {
        public void Match(Action? onUpright, Action? onReversed)
        {
            switch (orientation)
            {
                case Upright:
                    onUpright?.Invoke();
                    return;
                case Reversed:
                    onReversed?.Invoke();
                    return;
                default:
                    throw Ex.UndefinedEnum(orientation);
            }
        }

        public R Match<R>(Func<R> onUpright, Func<R> onReversed)
            => orientation switch
            {
                Upright => onUpright(),
                Reversed => onReversed(),
                _ => throw Ex.UndefinedEnum(orientation),
            };
    }
}