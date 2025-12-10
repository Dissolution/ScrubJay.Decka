using System.Diagnostics.CodeAnalysis;
using ScrubJay.Decka.Sandbox.Iterations.StackBased.Display;
using ScrubJay.Functional.IMPL;
using static InlineIL.IL;

namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

/// <summary>
/// A playing card
/// </summary>
/// <remarks>
/// A Card takes up exactly 1 <see cref="byte"/> (8 bits):<br/>
/// <c>OFSSRRRR</c><br/>
/// O - <see cref="Orientation"/><br/>
/// F — <see cref="Face"/><br/>
/// S — <see cref="Suit"/> and <see cref="Color"/><br/>
/// R — <see cref="Rank"/><br/>
/// </remarks>
/// <default><see cref="None"/></default>
/// <mask><c>0b11111111</c></mask>
/// <seealso href="https://en.wikipedia.org/wiki/Playing_Cards_(Unicode_block)"/>
[StructLayout(LayoutKind.Sequential, Size = 1, Pack = 1)]
public readonly struct Card : ICardPart<Card>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator byte(Card card)
    {
        Emit.Ldarg(nameof(card));
        return Return<byte>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Card(byte value)
    {
        Emit.Ldarg(nameof(value));
        return Return<Card>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Card left, Card right)
    {
        Emit.Ldarg(nameof(left));
        Emit.Ldarg(nameof(right));
        Emit.Ceq();
        return Return<bool>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Card left, Card right)
    {
        return !(left == right);
    }

    public static byte Mask => 0b11111111;

    public static int BitCount => 8;

    public static Card Default { get; } = default;


    public static Card New(Rank rank, Suit suit)
    {
        Emit.Ldarga(nameof(rank));
        Emit.Ldobj<byte>();
        Emit.Ldarga(nameof(suit));
        Emit.Ldobj<byte>();
        Emit.Or();
        return Return<Card>();
    }

    public static Result<Card> TryParse(string? str, IFormatProvider? provider = null)
    {
        throw Ex.NotImplemented();
    }

    private readonly byte _value;

    /*public Orientation Orientation
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (Orientation)(_value & Orientations.MASK);
    }

    public Face Face
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (Face)(_value & Faces.MASK);
    }*/

    public Suit Suit
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Emit.Ldarg_0();
            Emit.Ldc_I4(Suit.MASK);
            Emit.And();
            return Return<Suit>();
        }
    }

    public Rank Rank
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Emit.Ldarg_0();
            Emit.Ldc_I4(Rank.MASK);
            Emit.And();
            return Return<Rank>();
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Card(byte value) => _value = value;

    //
    // public Card With(Orientation orientation)
    // {
    //     int value = _value;
    //     // clear the orientation bit(s)
    //     value &= ~Orientations.MASK;
    //     // set the orientation bit(s)
    //     value |= (byte)orientation;
    //     // fin
    //     return new Card((byte)value);
    // }
    //
    // public Card With(Face face)
    // {
    //     int value = _value;
    //     // clear the face bit(s)
    //     value &= ~Faces.MASK;
    //     // set the face bit(s)
    //     value |= (byte)face;
    //     // fin
    //     return new Card((byte)value);
    // }

    public Card With(Suit suit)
    {
        int value = _value;
        // clear the suit bit(s)
        value &= ~Suit.MASK;
        // set the suit bit(s)
        value |= (byte)suit;
        // fin
        return new Card((byte)value);
    }

    public Card With(Rank rank)
    {
        int value = _value;
        // clear the rank bit(s)
        value &= ~Rank.MASK;
        // set the rank bit(s)
        value |= (byte)rank;
        // fin
        return new Card((byte)value);
    }

    public Card DeepClone()
    {
        Emit.Ldarg_0();
        return Return<Card>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Card other)
    {
        Emit.Ldarg_0();
        Emit.Ldind_U1(); 
        Emit.Ldarg_1();
        Emit.Ceq();
        return Return<bool>();
    }

    public bool Equals(byte u8) => _value == u8;

    // public bool Equals(Orientation orientation) => this.Orientation == orientation;
    // public bool Equals(Face face) => this.Face == face;
    public bool Equals(Rank rank) => this.Rank == rank;
    public bool Equals(Suit suit) => this.Suit == suit;


    public override bool Equals([NotNullWhen(true)] object? obj) => obj switch
    {
        Card card => Equals(card),
        byte u8 => _value == u8,
        // Orientation orientation => Equals(orientation),
        // Face face => Equals(face),
        Rank rank => Equals(rank),
        Suit suit => Equals(suit),
        _ => false,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        Emit.Ldarg_0();
        Emit.Ldind_U1();
        return Return<int>();
    }

    public string ToString(DisplayFormat format)
    {
        if (format == DisplayFormat.ToString)
            return ToString();
        if (format == DisplayFormat.Short || format == DisplayFormat.Emoji)
            return Rank.ToString(format) + Suit.ToString(format);

        if (Suit == Suit.Spade)
        {
            return this.Rank.Match(
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
        else if (Suit == Suit.Diamond)
        {
            return this.Rank.Match(
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
        else if (Suit == Suit.Club)
        {
            return this.Rank.Match(
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
        else if (Suit == Suit.Heart)
        {
            return this.Rank.Match(
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
            throw Ex.Argument(this);
        }
    }

    public override string ToString()
    {
        return Build($"{Rank} of {Suit}s");
    }
}