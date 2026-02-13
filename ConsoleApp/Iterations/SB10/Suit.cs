using ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;
using static ScrubJay.Decka.Sandbox.Iterations.SB10.Suit;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/>'s suit:
/// <list type="table">
///     <listheader>
///         <term>Name</term>
///         <description><c>____Bits____  |  Value  |  Short  |  Unicode  |  Emoji</c></description>
///     </listheader>
///     <item>
///         <term>Spade</term>
///         <description><c>0b00_00_0000  |  &#8199;&#8199;&#8199;&#8199;0  |  &#8199;&#8199;S&#8199;&#8199;  |  &#8199;&#8199;♠&#8199;&#8199;&#8199;  |  &#8199;&#8199;♠️&#8199;</c></description>
///     </item>
///     <item>
///         <term>Diamond</term>
///         <description><c>0b00_01_0000  |  &#8199;&#8199;&#8199;16  |  &#8199;&#8199;D&#8199;&#8199;  |  &#8199;&#8199;♦&#8199;&#8199;&#8199;  |  &#8199;&#8199;♦️&#8199;</c></description>
///     </item>
///     <item>
///         <term>Club</term>
///         <description><c>0b00_10_0000  |  &#8199;&#8199;&#8199;32  |  &#8199;&#8199;C&#8199;&#8199;  |  &#8199;&#8199;♣&#8199;&#8199;&#8199;  |  &#8199;&#8199;♣️&#8199;</c></description>
///     </item>
///     <item>
///         <term>Heart</term>
///         <description><c>0b00_11_0000  |  &#8199;&#8199;&#8199;48  |  &#8199;&#8199;H&#8199;&#8199;  |  &#8199;&#8199;♥&#8199;&#8199;&#8199;  |  &#8199;&#8199;♥️&#8199;</c></description>
///     </item>
/// </list>
/// </summary>
/// <remarks>
/// The 5th and 6th bits of a <see cref="Card"/> <see cref="byte"/>.
/// </remarks>
/// <default><see cref="Suit.Spade"/></default>
/// <mask><c>0b00_11_0000</c></mask>
/// <seealso href="https://en.wikipedia.org/wiki/Playing_card_suit"/><br/>
/// <seealso href="https://en.wikipedia.org/wiki/Playing_Cards_(Unicode_block)"/><br/>
[PublicAPI]
[Flags]
public enum Suit : byte
{
    /// <summary>
    /// Spade ♠️ - <c>0b00_00_0000</c> 
    /// </summary>
    Spade = 0b00_00_0000,

    /// <summary>
    /// Diamond ♦️ - <c>0b00_01_0000</c>
    /// </summary>
    Diamond = 0b00_01_0000,

    /// <summary>
    /// Club ♣️ - <c>0b00_10_0000</c>
    /// </summary>
    Club = 0b00_10_0000,

    /// <summary>
    /// Heart ♥️ - <c>0b00_11_0000</c>
    /// </summary>
    Heart = 0b00_11_0000,
}

/// <summary>
/// Extensions on <see cref="Suit"/>
/// </summary>
[PublicAPI]
public static class SuitExtensions
{
    internal const byte MASK = 0b00_11_0000;
    
    static SuitExtensions()
    {
        DisplayFormat
            .Register<Suit>()
            .Register(Spade, nameof(Spade), "S", "♠", "♠️")
            .Register(Diamond, nameof(Diamond), "D", "♦", "♦️")
            .Register(Club, nameof(Club), "C", "♣", "♣️")
            .Register(Heart, nameof(Heart), "H", "♥", "♥️");
    }

    extension(Suit)
    {
        public static Card operator |(Suit suit, Rank rank)
        {
            return (Card)((byte)suit | (byte)rank);
        }

        public static Card operator +(Suit suit, Rank rank)
        {
            return (Card)((byte)suit | (byte)rank);
        }

        public static Card operator |(Suit suit, Card card) => card.With(suit);

        public static Card operator +(Suit suit, Card card) => card.With(suit);

        
        public static byte Mask => MASK;

        public static int BitCount => 2;

        public static Suit Default => Spade;

        public static Suit[] Values => [Spade, Diamond, Club, Heart];
    }

    extension(Suit suit)
    {
        public Color Color => (Color)((byte)suit & Color.Mask);

        public void Match(
            Action? onSpade,
            Action? onDiamond,
            Action? onClub,
            Action? onHeart)
        {
            switch (suit)
            {
                case Spade:
                    onSpade?.Invoke();
                    break;
                case Diamond:
                    onDiamond?.Invoke();
                    break;
                case Club:
                    onClub?.Invoke();
                    break;
                case Heart:
                    onHeart?.Invoke();
                    break;
                default:
                    throw Ex.UndefinedEnum(suit);
            }
        }

        public R Match<R>(
            Func<R> onSpade,
            Func<R> onDiamond,
            Func<R> onClub,
            Func<R> onHeart)
        {
            if (suit == Spade)
                return onSpade();
            if (suit == Diamond)
                return onDiamond();
            if (suit == Club)
                return onClub();
            if (suit == Heart)
                return onHeart();
            throw Ex.UndefinedEnum(suit);
        }
    }
}