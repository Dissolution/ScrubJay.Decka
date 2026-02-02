using static ScrubJay.Decka.Sandbox.Iterations.SB10.Color;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/> and/or <see cref="Suit"/>'s Color:
/// <list type="bullet">
/// <item><c>0b000_0_0000</c> ◼️ Black</item>
/// <item><c>0b000_1_0000</c> 🔴 Red</item>
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
    static ColorExtensions()
    {
        var dm = DisplayFormat.For<Color>();
        dm.Map(Black)
            .Add(DisplayFormat.ToString, nameof(Black))
            .Add(DisplayFormat.Short, "B")
            .Add(DisplayFormat.Unicode, "⬛")
            .Add(DisplayFormat.Emoji, "◼️");
        dm.Map(Red)
            .Add(DisplayFormat.ToString, nameof(Red))
            .Add(DisplayFormat.Short, "R")
            .Add(DisplayFormat.Unicode, "🟥")
            .Add(DisplayFormat.Emoji, "🔴");
    }
    
    extension(Color)
    {
        public static byte Mask => 0b000_1_0000;
        public static int BitCount => 1;
        public static Color Default => Black;

        public static Result<Color> TryParse(string str)
        {
            return DisplayFormat.For<Color>().TryParse(str);
        }
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
        
        public string ToString(DisplayFormat format)
        {
            return DisplayFormat.For<Color>().Display(color, format);
        }
    }
}