using System.Diagnostics.CodeAnalysis;
using ScrubJay.Decka.Sandbox.Iterations.StackBased.Display;
using static InlineIL.IL;
// ReSharper disable EntityNameCapturedOnly.Global

namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

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
[StructLayout(LayoutKind.Sequential, Size = 1, Pack = 1)]
public readonly struct Color : ICardPart<Color>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator byte(Color color)
    {
        Emit.Ldarg(nameof(color));
        return Return<byte>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Color(byte u8)
    {
        Emit.Ldarg(nameof(u8));
        Emit.Ldc_I4(MASK);
        Emit.And();
        return Return<Color>();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Color(Suit suit)
    {
        Emit.Ldarg(nameof(suit));
        Emit.Ldc_I4(MASK);
        Emit.And();
        return Return<Color>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Color left, Color right)
    {
        Emit.Ldarg(nameof(left));
        Emit.Ldarg(nameof(right));
        Emit.Ceq();
        return Return<bool>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Color left, Color right)
    {
        return !(left == right);
    }

    public const byte MASK = 0b000_1_0000;


    public static readonly Color Black = new Color(0b000_0_0000);
    public static readonly Color Red = new Color(0b000_1_0000);

    static Color()
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

    public static byte Mask => MASK;
    
    public static int BitCount => 1;

    public static Color Default { get; } = Black;

    public static Result<Color> TryParse(string? str, IFormatProvider? provider = null)
    {
        return DisplayFormat.For<Color>().TryParse(str);
    }
    
    public static bool ContainsAll(scoped ReadOnlySpan<byte> cards)
    {
        if (cards.Length != 2)
            return false;

        int xorCard = cards[0] ^ cards[1] ;

        int orCard = cards[0] | cards[1];

        return (xorCard & MASK) == 0 && (orCard & MASK) == MASK;
    }

    public static bool ContainsAll(scoped ReadOnlySpan<Card> cards)
    {
        return ContainsAll(cards.AsBytes());
    }
    
    private readonly byte _value;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Color(byte value) => _value = value;

    public Color DeepClone()
    {
        Emit.Ldarg_0();
        return Return<Color>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Color other)
    {
        Emit.Ldarg_0();
        Emit.Ldind_U1(); 
        Emit.Ldarg_1();
        Emit.Ceq();
        return Return<bool>();
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Color color)
            return Equals(color);
        if (obj is byte u8)
            return Equals((Color)u8);
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
        return DisplayFormat.For<Color>().Format(this, format);
    }

    public override string ToString()
    {
        return DisplayFormat.For<Color>().Format(this, DisplayFormat.ToString);
    }
}