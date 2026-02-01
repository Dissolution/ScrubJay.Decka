using System.Diagnostics.CodeAnalysis;
using ScrubJay.Decka.Sandbox.Iterations.StackBased.Display;
using static InlineIL.IL;

namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

/// <summary>
/// A <see cref="Card"/>'s Rank:
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
/// <default><see cref="Rank.None"/></default>
/// <mask><c>0b0000_1111</c></mask>
/// <seealso href="https://en.wikipedia.org/wiki/Playing_Cards_(Unicode_block)"/>
[StructLayout(LayoutKind.Sequential, Size = 0b1, Pack = 0b1)]
public readonly struct Rank : ICardPart<Rank>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator byte(Rank rank)
    {
        Emit.Ldarg(nameof(rank));
        return Return<byte>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Rank(byte value)
    {
        Emit.Ldarg(nameof(value));
        Emit.Ldc_I4(MASK);
        Emit.And();
        return Return<Rank>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Rank left, Rank right)
    {
        Emit.Ldarg(nameof(left));
        Emit.Ldarg(nameof(right));
        Emit.Ceq();
        return Return<bool>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Rank left, Rank right)
    {
        return !(left == right);
    }

    public const byte MASK = 0b0000_1111;


    public static readonly Rank None = new Rank(0b0000);
    public static readonly Rank Ace = new Rank(0b0001);
    public static readonly Rank Two = new Rank(0b0010);
    public static readonly Rank Three = new Rank(0b0011);
    public static readonly Rank Four = new Rank(0b0100);
    public static readonly Rank Five = new Rank(0b0101);
    public static readonly Rank Six = new Rank(0b0110);
    public static readonly Rank Seven = new Rank(0b0111);
    public static readonly Rank Eight = new Rank(0b1000);
    public static readonly Rank Nine = new Rank(0b1001);
    public static readonly Rank Ten = new Rank(0b1010);
    public static readonly Rank Jack = new Rank(0b1011);
    public static readonly Rank Knight = new Rank(0b1100);
    public static readonly Rank Queen = new Rank(0b1101);
    public static readonly Rank King = new Rank(0b1110);
    public static readonly Rank Joker = new Rank(0b1111);

    static Rank()
    {
        var dm = DisplayFormat.For<Rank>();
        dm.Map(Rank.None)
            .Add(DisplayFormat.ToString, nameof(None))
            .Add(DisplayFormat.Short, "_")
            .Add(DisplayFormat.Unicode, "❓")
            .Add(DisplayFormat.Emoji, "❔");
        dm.Map(Rank.Ace)
            .Add(DisplayFormat.ToString, nameof(Ace))
            .Add(DisplayFormat.Short, "A")
            .Add(DisplayFormat.Unicode, "Ａ")
            .Add(DisplayFormat.Emoji, "🅰️");
        dm.Map(Rank.Two)
            .Add(DisplayFormat.ToString, nameof(Two))
            .Add(DisplayFormat.Short, "2")
            .Add(DisplayFormat.Unicode, "２")
            .Add(DisplayFormat.Emoji, "2️⃣");
        dm.Map(Rank.Three)
            .Add(DisplayFormat.ToString, nameof(Three))
            .Add(DisplayFormat.Short, "3")
            .Add(DisplayFormat.Unicode, "３")
            .Add(DisplayFormat.Emoji, "3️⃣");
        dm.Map(Rank.Four)
            .Add(DisplayFormat.ToString, nameof(Four))
            .Add(DisplayFormat.Short, "4")
            .Add(DisplayFormat.Unicode, "４")
            .Add(DisplayFormat.Emoji, "4️⃣");
        dm.Map(Rank.Five)
            .Add(DisplayFormat.ToString, nameof(Five))
            .Add(DisplayFormat.Short, "5")
            .Add(DisplayFormat.Unicode, "５")
            .Add(DisplayFormat.Emoji, "5️⃣");
        dm.Map(Rank.Six)
            .Add(DisplayFormat.ToString, nameof(Six))
            .Add(DisplayFormat.Short, "6")
            .Add(DisplayFormat.Unicode, "６")
            .Add(DisplayFormat.Emoji, "6️⃣");
        dm.Map(Rank.Seven)
            .Add(DisplayFormat.ToString, nameof(Seven))
            .Add(DisplayFormat.Short, "7")
            .Add(DisplayFormat.Unicode, "７")
            .Add(DisplayFormat.Emoji, "7️⃣");
        dm.Map(Rank.Eight)
            .Add(DisplayFormat.ToString, nameof(Eight))
            .Add(DisplayFormat.Short, "8")
            .Add(DisplayFormat.Unicode, "８")
            .Add(DisplayFormat.Emoji, "8️⃣");
        dm.Map(Rank.Nine)
            .Add(DisplayFormat.ToString, nameof(Nine))
            .Add(DisplayFormat.Short, "9")
            .Add(DisplayFormat.Unicode, "９")
            .Add(DisplayFormat.Emoji, "9️⃣");
        dm.Map(Rank.Ten)
            .Add(DisplayFormat.ToString, nameof(Ten))
            .Add(DisplayFormat.Short, "X")
            .Add(DisplayFormat.Unicode, "Ｘ")
            .Add(DisplayFormat.Emoji, "🔟");
        dm.Map(Rank.Jack)
            .Add(DisplayFormat.ToString, nameof(Jack))
            .Add(DisplayFormat.Short, "J")
            .Add(DisplayFormat.Unicode, "Ｊ")
            .Add(DisplayFormat.Emoji, "🅹");
        dm.Map(Rank.Knight)
            .Add(DisplayFormat.ToString, nameof(Knight))
            .Add(DisplayFormat.Short, "N")
            .Add(DisplayFormat.Unicode, "Ｎ")
            .Add(DisplayFormat.Emoji, "♞");
        dm.Map(Rank.Queen)
            .Add(DisplayFormat.ToString, nameof(Queen))
            .Add(DisplayFormat.Short, "Q")
            .Add(DisplayFormat.Unicode, "Ｑ")
            .Add(DisplayFormat.Emoji, "🆀");
        dm.Map(Rank.King)
            .Add(DisplayFormat.ToString, nameof(King))
            .Add(DisplayFormat.Short, "K")
            .Add(DisplayFormat.Unicode, "Ｋ")
            .Add(DisplayFormat.Emoji, "🅺");
        dm.Map(Rank.Joker)
            .Add(DisplayFormat.ToString, nameof(Joker))
            .Add(DisplayFormat.Short, "~")
            .Add(DisplayFormat.Unicode, "～")
            .Add(DisplayFormat.Emoji, "🃏");

    }

    public static byte Mask => MASK;

    public static int BitCount => 4;

    public static Rank Default { get; } = None;

    public static Result<Rank> TryParse(string? str, IFormatProvider? provider = null)
    {
        return DisplayFormat.For<Rank>().TryParse(str);
    }

    private readonly byte _value;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Rank(byte value) => _value = value;

    public Rank DeepClone()
    {
        Emit.Ldarg_0();
        return Return<Rank>();
    }

    public R Match<R>(
        Func<R> onAce, 
        Func<R> onTwo, 
        Func<R> onThree, 
        Func<R> onFour, 
        Func<R> onFive, 
        Func<R> onSix, 
        Func<R> onSeven, 
        Func<R> onEight, 
        Func<R> onNine, 
        Func<R> onTen, 
        Func<R> onJack, 
        Func<R> onKnight, 
        Func<R> onQueen, 
        Func<R> onKing, 
        Func<R> onJoker)
    {
        if (_value == 1)
            return onAce();
        if (_value == 2)
            return onTwo();
        if (_value == 3)
            return onThree();
        if (_value == 4)
            return onFour();
        if (_value == 5)
            return onFive();
        if (_value == 6)
            return onSix();
        if (_value == 7)
            return onSeven();
        if (_value == 8)
            return onEight();
        if (_value == 9)
            return onNine();
        if (_value == 10)
            return onTen();
        if (_value == 11)
            return onJack();
        if (_value == 12)
            return onKnight();
        if (_value == 13)
            return onQueen();
        if (_value == 14)
            return onKing();
        if (_value == 15)
            return onJoker();
        throw Ex.Arg(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Rank other)
    {
        Emit.Ldarg_0();
        Emit.Ldind_U1(); 
        Emit.Ldarg_1();
        Emit.Ceq();
        return Return<bool>();
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Rank rank)
            return Equals(rank);
        if (obj is byte u8)
            return Equals((Rank)u8);
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
        return DisplayFormat.For<Rank>().Format(this, format);
    }

    public override string ToString()
    {
        return DisplayFormat.For<Rank>().Format(this, DisplayFormat.ToString);
    }
}