using System.Diagnostics.CodeAnalysis;
using ScrubJay.Decka.Sandbox.Iterations.StackBased.Display;
using static InlineIL.IL;

namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

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
/// <seealso href="https://en.wikipedia.org/wiki/Playing_card_suit"/>
/// <seealso href="https://en.wikipedia.org/wiki/Playing_Cards_(Unicode_block)"/>
[StructLayout(LayoutKind.Sequential, Size = 1, Pack = 1)]
public readonly struct Suit : ICardPart<Suit>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator byte(Suit suit)
    {
        Emit.Ldarg(nameof(suit));
        return Return<byte>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Suit(byte value)
    {
        Emit.Ldarg(nameof(value));
        Emit.Ldc_I4(MASK);
        Emit.And();
        return Return<Suit>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Suit left, Suit right)
    {
        Emit.Ldarg(nameof(left));
        Emit.Ldarg(nameof(right));
        Emit.Ceq();
        return Return<bool>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Suit left, Suit right)
    {
        return !(left == right);
    }

    public const byte MASK = 0b00_11_0000;


    public static readonly Suit Spade = new Suit(0b00_00_0000);
    public static readonly Suit Diamond = new Suit(0b00_01_0000);
    public static readonly Suit Club = new Suit(0b00_10_0000);
    public static readonly Suit Heart = new Suit(0b00_11_0000);

    static Suit()
    {
        var dm = DisplayFormat.For<Suit>();
        dm.Map(Spade)
            .Add(DisplayFormat.ToString, nameof(Spade))
            .Add(DisplayFormat.Short, "S")
            .Add(DisplayFormat.Unicode, "♠")
            .Add(DisplayFormat.Emoji, "♠️");
        dm.Map(Diamond)
            .Add(DisplayFormat.ToString, nameof(Diamond))
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

    public static byte Mask => MASK;
    
    public static int BitCount => 2;

    public static Suit Default { get; } = Spade;

    public static Result<Suit> TryParse(string? str, IFormatProvider? provider = null)
    {
        return DisplayFormat.For<Suit>().TryParse(str);
    }
    
    public static bool ContainsAll(scoped ReadOnlySpan<byte> cards)
    {
        if (cards.Length != 4)
            return false;

        int xorCard = cards[0] ^ cards[1] ^ cards[2] ^ cards[3];
        
        int orCard = cards[0] | cards[1] | cards[2] | cards[3];

        return (xorCard & MASK) == 0 && (orCard & MASK) == MASK;
    }

    public static bool ContainsAll(scoped ReadOnlySpan<Card> cards)
    {
        return ContainsAll(cards.AsBytes());
    }

    private readonly byte _value;

    public Color Color => (Color)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Suit(byte value) => _value = value;

    public Suit DeepClone()
    {
        Emit.Ldarg_0();
        return Return<Suit>();
    }

    public R Match<R>(
        Func<R> onSpade, 
        Func<R> onDiamond, 
        Func<R> onClub,
        Func<R> onHeart)
    {
        if (_value == 0)
            return onSpade();
        if (_value == 1)
            return onDiamond();
        if (_value == 2)
            return onClub();
        if (_value == 3)
            return onHeart();
        throw Ex.Arg(this);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Suit other)
    {
        Emit.Ldarg_0();
        Emit.Ldind_U1(); 
        Emit.Ldarg_1();
        Emit.Ceq();
        return Return<bool>();
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Suit suit)
            return Equals(suit);
        if (obj is byte u8)
            return Equals((Suit)u8);
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        Emit.Ldarg_0();
        Emit.Ldind_U1();
        return Return<int>();
    }
    
    public string ToString(DisplayFormat format)
    {
        return DisplayFormat.For<Suit>().Format(this, format);
    }

    public override string ToString()
    {
        return DisplayFormat.For<Suit>().Format(this, DisplayFormat.ToString);
    }
}