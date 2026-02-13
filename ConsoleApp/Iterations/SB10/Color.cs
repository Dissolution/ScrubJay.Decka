using ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;
using static ScrubJay.Decka.Sandbox.Iterations.SB10.Color;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/> and/or <see cref="Suit"/>'s color:
/// <list type="table">
///     <listheader>
///         <term>Name</term>
///         <description><c>____Bits____  |  Value  |  Short  |  Unicode  |  Emoji</c></description>
///     </listheader>
///     <item>
///         <term>Black</term>
///         <description><c>0b000_0_0000  |  &#8199;&#8199;&#8199;&#8199;0  |  &#8199;&#8199;B&#8199;&#8199;  |  &#8199;&#8199;&#8199;●&#8199;&#8199;&#8199;  |  &#8199;&#8199;◼️&#8199;</c></description>
///     </item>
///     <item>
///         <term>Red</term>
///         <description><c>0b000_1_0000  |  &#8199;&#8199;&#8199;16  |  &#8199;&#8199;R&#8199;&#8199;  |  &#8199;&#8199;&#8199;○&#8199;&#8199;&#8199;  |  &#8199;&#8199;🔴&#8199;</c></description>
///     </item>
/// </list>
/// </summary>
/// <remarks>
/// The 5th bit of a <see cref="Card"/> <see cref="byte"/>
/// </remarks>
/// <default><see cref="Color.Black"/></default>
/// <mask><c>0b000_1_0000</c></mask>
/// <seealso href="https://en.wikipedia.org/wiki/Playing_card_Color"/>
/// <seealso href="https://en.wikipedia.org/wiki/Playing_Cards_(Unicode_block)"/>
[PublicAPI]
[Flags]
public enum Color : byte
{
    /// <summary>
    /// ◼️ Black   <c>0b000_0_0000</c>
    /// </summary>
    Black = 0b000_0_0000,

    /// <summary>
    /// 🔴 Red   <c>0b000_1_0000</c>
    /// </summary>
    Red = 0b000_1_0000,
}

[PublicAPI]
public static class ColorExtensions
{
    internal const byte MASK = 0b000_1_0000;

    static ColorExtensions()
    {
        DisplayFormat.Register<Color>()
            .FormatAs(Black, nameof(Black), "B", "●", "◼️")
            .FormatAs(Red, nameof(Red), "R", "○", "🔴");
    }

    extension(Color)
    {
        public static byte Mask => MASK;
        
        public static int BitCount => 1;
        
        public static Color Default => Black;

        public static Color[] Values => [Black, Red];
    }

    extension(Color color)
    {
        public void Match(
            Action? onBlack,
            Action? onRed)
        {
            switch (color)
            {
                case Black:
                    onBlack?.Invoke();
                    break;
                case Red:
                    onRed?.Invoke();
                    break;
                default:
                    throw Ex.Arg(color);
            }
        }

        public R Match<R>(
            Func<R> onBlack,
            Func<R> onRed)
        {
            return color switch
            {
                Black => onBlack(),
                Red => onRed(),
                _ => throw Ex.UndefinedEnum(color),
            };
        }
    }
}