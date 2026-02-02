using static ScrubJay.Decka.Sandbox.Iterations.SB10.Suit;


namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/>'s Suit:
/// <list type="bullet">
/// <item><c>0b00_00_0000</c> ♠️ Spade</item>
/// <item><c>0b00_01_0000</c> ♦️ Diamond</item>
/// <item><c>0b00_10_0000</c> ♣️ Club</item>
/// <item><c>0b00_11_0000</c> ♥️ Heart</item>
/// </list>
/// </summary>
/// <remarks>
/// The 5th and 6th bits of a <see cref="Card"/> <see cref="byte"/>
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
    /// ♠️ Spade  <c>0b00_00_0000</c> 
    /// </summary>
    Spade = 0b00_00_0000,

    /// <summary>
    /// ♦️ Diamond  <c>0b00_01_0000</c>
    /// </summary>
    Diamond = 0b00_01_0000,

    /// <summary>
    /// ♣️ Club   <c>0b00_10_0000</c>
    /// </summary>
    Club = 0b00_10_0000,

    /// <summary>
    /// ♥️ Heart   <c>0b00_11_0000</c>
    /// </summary>
    Heart = 0b00_11_0000,
}

/// <summary>
/// Extensions on <see cref="Suit"/>
/// </summary>
[PublicAPI]
public static class SuitExtensions
{
    static SuitExtensions()
    {
        var dm = DisplayFormat.For<Suit>();
        dm.Map(Spade)
            .Add(DisplayFormat.ToString, nameof(Suit.Spade))
            .Add(DisplayFormat.Short, "S")
            .Add(DisplayFormat.Unicode, "♠")
            .Add(DisplayFormat.Emoji, "♠️");
        dm.Map(Diamond)
            .Add(DisplayFormat.ToString, nameof(Suit.Diamond))
            .Add(DisplayFormat.Short, "D")
            .Add(DisplayFormat.Unicode, "♦")
            .Add(DisplayFormat.Emoji, "♦️");
        dm.Map(Club)
            .Add(DisplayFormat.ToString, nameof(Club))
            .Add(DisplayFormat.Short, "C")
            .Add(DisplayFormat.Unicode, "♣")
            .Add(DisplayFormat.Emoji, "♣️");
        dm.Map(Heart)
            .Add(DisplayFormat.ToString, nameof(Heart))
            .Add(DisplayFormat.Short, "H")
            .Add(DisplayFormat.Unicode, "♥")
            .Add(DisplayFormat.Emoji, "♥️");
    }

    extension(Suit)
    {
        public static Card operator |(Suit suit, Rank rank)
        {
            return Card.New(suit, rank);
        }
        
        public static byte Mask => 0b00_11_0000;
        public static int BitCount => 2;
        public static Suit Default => Spade;

        public static Suit[] Values => [Spade, Diamond, Club, Heart];
        
        public static Result<Suit> TryParse(string str)
        {
            return DisplayFormat.For<Suit>().TryParse(str);
        }
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
                    throw Ex.Arg(suit);
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
            throw Ex.Arg(suit);
        }

        public string ToString(DisplayFormat format)
        {
            return DisplayFormat.For<Suit>().Display(suit, format);
        }
    }
}