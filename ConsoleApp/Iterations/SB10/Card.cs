using ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;
using static InlineIL.IL;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A playing card
/// </summary>
/// <remarks>
/// A Card takes up exactly 1 <see cref="byte"/> (8 bits):<br/>
/// <c>OFSSRRRR</c><br/>
/// O - <see cref="Orientation"/><br/>
/// F — <see cref="Facing"/><br/>
/// S — <see cref="Suit"/> and <see cref="Color"/><br/>
/// R — <see cref="Rank"/><br/>
/// </remarks>
/// <default><see cref="None"/></default>
/// <mask><c>0b11111111</c></mask>
/// <seealso href="https://en.wikipedia.org/wiki/Playing_Cards_(Unicode_block)"/>
[PublicAPI]
[Flags]
public enum Card : byte
{
    // spade, diamond, club, heart

    AceOfSpades = Rank.Ace | Suit.Spade,
    TwoOfSpades = Rank.Two | Suit.Spade,
    ThreeOfSpades = Rank.Three | Suit.Spade,
    FourOfSpades = Rank.Four | Suit.Spade,
    FiveOfSpades = Rank.Five | Suit.Spade,
    SixOfSpades = Rank.Six | Suit.Spade,
    SevenOfSpades = Rank.Seven | Suit.Spade,
    EightOfSpades = Rank.Eight | Suit.Spade,
    NineOfSpades = Rank.Nine | Suit.Spade,
    TenOfSpades = Rank.Ten | Suit.Spade,
    JackOfSpades = Rank.Jack | Suit.Spade,
    KnightOfSpades = Rank.Knight | Suit.Spade,
    QueenOfSpades = Rank.Queen | Suit.Spade,
    KingOfSpades = Rank.King | Suit.Spade,
    JokerOfSpades = Rank.Joker | Suit.Spade,

    AceOfDiamonds = Rank.Ace | Suit.Diamond,
    TwoOfDiamonds = Rank.Two | Suit.Diamond,
    ThreeOfDiamonds = Rank.Three | Suit.Diamond,
    FourOfDiamonds = Rank.Four | Suit.Diamond,
    FiveOfDiamonds = Rank.Five | Suit.Diamond,
    SixOfDiamonds = Rank.Six | Suit.Diamond,
    SevenOfDiamonds = Rank.Seven | Suit.Diamond,
    EightOfDiamonds = Rank.Eight | Suit.Diamond,
    NineOfDiamonds = Rank.Nine | Suit.Diamond,
    TenOfDiamonds = Rank.Ten | Suit.Diamond,
    JackOfDiamonds = Rank.Jack | Suit.Diamond,
    KnightOfDiamonds = Rank.Knight | Suit.Diamond,
    QueenOfDiamonds = Rank.Queen | Suit.Diamond,
    KingOfDiamonds = Rank.King | Suit.Diamond,
    JokerOfDiamonds = Rank.Joker | Suit.Diamond,

    AceOfClubs = Rank.Ace | Suit.Club,
    TwoOfClubs = Rank.Two | Suit.Club,
    ThreeOfClubs = Rank.Three | Suit.Club,
    FourOfClubs = Rank.Four | Suit.Club,
    FiveOfClubs = Rank.Five | Suit.Club,
    SixOfClubs = Rank.Six | Suit.Club,
    SevenOfClubs = Rank.Seven | Suit.Club,
    EightOfClubs = Rank.Eight | Suit.Club,
    NineOfClubs = Rank.Nine | Suit.Club,
    TenOfClubs = Rank.Ten | Suit.Club,
    JackOfClubs = Rank.Jack | Suit.Club,
    KnightOfClubs = Rank.Knight | Suit.Club,
    QueenOfClubs = Rank.Queen | Suit.Club,
    KingOfClubs = Rank.King | Suit.Club,
    JokerOfClubs = Rank.Joker | Suit.Club,

    AceOfHearts = Rank.Ace | Suit.Heart,
    TwoOfHearts = Rank.Two | Suit.Heart,
    ThreeOfHearts = Rank.Three | Suit.Heart,
    FourOfHearts = Rank.Four | Suit.Heart,
    FiveOfHearts = Rank.Five | Suit.Heart,
    SixOfHearts = Rank.Six | Suit.Heart,
    SevenOfHearts = Rank.Seven | Suit.Heart,
    EightOfHearts = Rank.Eight | Suit.Heart,
    NineOfHearts = Rank.Nine | Suit.Heart,
    TenOfHearts = Rank.Ten | Suit.Heart,
    JackOfHearts = Rank.Jack | Suit.Heart,
    KnightOfHearts = Rank.Knight | Suit.Heart,
    QueenOfHearts = Rank.Queen | Suit.Heart,
    KingOfHearts = Rank.King | Suit.Heart,
    JokerOfHearts = Rank.Joker | Suit.Heart,
}

[PublicAPI]
public static class CardExtensions
{
    internal const byte MASK = 0b11111111;

    static CardExtensions()
    {
        DisplayFormat
            .Register<Card>()
            .Use(Format, TryParse);
    }

    private static string Format(Card card, DisplayFormat format)
    {
        if (format == DisplayFormat.Default)
            return card.ToString();

        if (format is DisplayFormat.Short or DisplayFormat.Emoji)
            return card.Rank.ToString(format) + card.Suit.ToString(format);

        if (card.Suit == Suit.Spade)
        {
            return card.Rank.Match(
                onAce: () => "🂡",
                onTwo: () => "🂢",
                onThree: () => "🂣",
                onFour: () => "🂤",
                onFive: () => "🂥",
                onSix: () => "🂦",
                onSeven: () => "🂧",
                onEight: () => "🂨",
                onNine: () => "🂩",
                onTen: () => "🂪",
                onJack: () => "🂫",
                onKnight: () => "🂬",
                onQueen: () => "🂭",
                onKing: () => "🂮",
                onJoker: () => "🃟"
            );
        }
        else if (card.Suit == Suit.Diamond)
        {
            return card.Rank.Match(
                onAce: () => "🃁",
                onTwo: () => "🃂",
                onThree: () => "🃃",
                onFour: () => "🃄",
                onFive: () => "🃅",
                onSix: () => "🃆",
                onSeven: () => "🃇",
                onEight: () => "🃈",
                onNine: () => "🃉",
                onTen: () => "🃊",
                onJack: () => "🃋",
                onKnight: () => "🃌",
                onQueen: () => "🃍",
                onKing: () => "🃎",
                onJoker: () => "🂿"
            );
        }
        else if (card.Suit == Suit.Club)
        {
            return card.Rank.Match(
                onAce: () => "🃑",
                onTwo: () => "🃒",
                onThree: () => "🃓",
                onFour: () => "🃔",
                onFive: () => "🃕",
                onSix: () => "🃖",
                onSeven: () => "🃗",
                onEight: () => "🃘",
                onNine: () => "🃙",
                onTen: () => "🃚",
                onJack: () => "🃛",
                onKnight: () => "🃜",
                onQueen: () => "🃝",
                onKing: () => "🃞",
                onJoker: () => "🃟"
            );
        }
        else if (card.Suit == Suit.Heart)
        {
            return card.Rank.Match(
                onAce: () => "🂱",
                onTwo: () => "🂲",
                onThree: () => "🂳",
                onFour: () => "🂴",
                onFive: () => "🂵",
                onSix: () => "🂶",
                onSeven: () => "🂷",
                onEight: () => "🂸",
                onNine: () => "🂹",
                onTen: () => "🂺",
                onJack: () => "🂻",
                onKnight: () => "🂼",
                onQueen: () => "🂽",
                onKing: () => "🂾",
                onJoker: () => "🂿"
            );
        }
        else
        {
            throw Ex.UndefinedEnum(card);
        }
    }

    private static Result<Card> TryParse(string? str, StringComparison comparison)
    {
        return Ex.NotImplemented();
    }

    extension(Card)
    {
        public static byte Mask => MASK;

        public static int BitCount => 8;

        public static Card Default => default(Card);

        public static Card New(Rank rank, Suit suit)
        {
            return (Card)((byte)rank | (byte)suit);
        }

        public static Card New(Suit suit, Rank rank)
        {
            return (Card)((byte)suit | (byte)rank);
        }
    }

    extension(Card card)
    {
        public Orientation Orientation
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Orientation)((byte)card & OrientationExtensions.MASK);
        }

        public Facing Facing
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Facing)((byte)card & FacingExtensions.MASK);
        }

        public Suit Suit
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Suit)((byte)card & SuitExtensions.MASK);
        }

        public Color Color
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Color)((byte)card & ColorExtensions.MASK);
        }
        
        public Rank Rank
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Rank)((byte)card & RankExtensions.MASK);
        }
    }

    public static Card With(this Card card, Orientation orientation)
    {
        Emit.Ldarg(nameof(card));
        Emit.Ldc_I4(OrientationExtensions.MASK);
        Emit.Not();
        Emit.And();
        Emit.Ldarg(nameof(orientation));
        Emit.Or();
        return Return<Card>();
    }
    
    public static Card With(this Card card, Facing facing)
    {
        Emit.Ldarg(nameof(card));
        Emit.Ldc_I4(FacingExtensions.MASK);
        Emit.Not();
        Emit.And();
        Emit.Ldarg(nameof(facing));
        Emit.Or();
        return Return<Card>();
    }
    
    public static Card With(this Card card, Suit suit)
    {
        Emit.Ldarg(nameof(card));
        Emit.Ldc_I4(SuitExtensions.MASK);
        Emit.Not();
        Emit.And();
        Emit.Ldarg(nameof(suit));
        Emit.Or();
        return Return<Card>();
    }

    public static Card With(this Card card, Rank rank)
    {
        Emit.Ldarg(nameof(card));
        Emit.Ldc_I4(RankExtensions.MASK);
        Emit.Not();
        Emit.And();
        Emit.Ldarg(nameof(rank));
        Emit.Or();
        return Return<Card>();
    }
}