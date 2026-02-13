using ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;
using static ScrubJay.Decka.Sandbox.Iterations.SB10.Rank;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/>'s rank:
/// <list type="table">
///     <listheader>
///         <term>Name</term>
///         <description><c>___Bits____  |  Value  |  Short  |  Unicode  |  Emoji</c></description>
///     </listheader>
///     <item>
///         <term>None</term>
///         <description>
///             <c>0b0000_0000  |  &#8199;&#8199;&#8199;&#8199;0  |  &#8199;&#8199;_&#8199;&#8199;  |  &#8199;&#8199;&#8199;？&#8199;&#8199;  |  &#8199;&#8199;⬜&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Ace</term>
///         <description>
///             <c>0b0000_0001  |  &#8199;&#8199;&#8199;&#8199;1  |  &#8199;&#8199;A&#8199;&#8199;  |  &#8199;&#8199;&#8199;Ａ&#8199;&#8199;  |  &#8199;&#8199;🅰️&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Two</term>
///         <description>
///             <c>0b0000_0010  |  &#8199;&#8199;&#8199;&#8199;2  |  &#8199;&#8199;2&#8199;&#8199;  |  &#8199;&#8199;&#8199;２&#8199;&#8199;  |  &#8199;&#8199;②&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Three</term>
///         <description>
///             <c>0b0000_0011  |  &#8199;&#8199;&#8199;&#8199;3  |  &#8199;&#8199;3&#8199;&#8199;  |  &#8199;&#8199;&#8199;３&#8199;&#8199;  |  &#8199;&#8199;③&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Four</term>
///         <description>
///             <c>0b0000_0100  |  &#8199;&#8199;&#8199;&#8199;4  |  &#8199;&#8199;4&#8199;&#8199;  |  &#8199;&#8199;&#8199;４&#8199;&#8199;  |  &#8199;&#8199;④&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Five</term>
///         <description>
///             <c>0b0000_0101  |  &#8199;&#8199;&#8199;&#8199;5  |  &#8199;&#8199;5&#8199;&#8199;  |  &#8199;&#8199;&#8199;５&#8199;&#8199;  |  &#8199;&#8199;⑤&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Six</term>
///         <description>
///             <c>0b0000_0110  |  &#8199;&#8199;&#8199;&#8199;6  |  &#8199;&#8199;6&#8199;&#8199;  |  &#8199;&#8199;&#8199;６&#8199;&#8199;  |  &#8199;&#8199;⑥&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Seven</term>
///         <description>
///             <c>0b0000_0111  |  &#8199;&#8199;&#8199;&#8199;7  |  &#8199;&#8199;7&#8199;&#8199;  |  &#8199;&#8199;&#8199;７&#8199;&#8199;  |  &#8199;&#8199;⑦&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Eight</term>
///         <description>
///             <c>0b0000_1000  |  &#8199;&#8199;&#8199;&#8199;8  |  &#8199;&#8199;8&#8199;&#8199;  |  &#8199;&#8199;&#8199;８&#8199;&#8199;  |  &#8199;&#8199;⑧&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Nine</term>
///         <description>
///             <c>0b0000_1001  |  &#8199;&#8199;&#8199;&#8199;9  |  &#8199;&#8199;9&#8199;&#8199;  |  &#8199;&#8199;&#8199;９&#8199;&#8199;  |  &#8199;&#8199;⑨&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Ten</term>
///         <description>
///             <c>0b0000_1010  |  &#8199;&#8199;&#8199;10  |  &#8199;&#8199;X&#8199;&#8199;  |  &#8199;&#8199;&#8199;Ｘ&#8199;&#8199;  |  &#8199;&#8199;🔟&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Jack</term>
///         <description>
///             <c>0b0000_1011  |  &#8199;&#8199;&#8199;11  |  &#8199;&#8199;J&#8199;&#8199;  |  &#8199;&#8199;&#8199;Ｊ&#8199;&#8199;  |  &#8199;&#8199;👦&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Knight</term>
///         <description>
///             <c>0b0000_1100  |  &#8199;&#8199;&#8199;12  |  &#8199;&#8199;N&#8199;&#8199;  |  &#8199;&#8199;&#8199;Ｎ&#8199;&#8199;  |  &#8199;&#8199;🐴&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Queen</term>
///         <description>
///             <c>0b0000_1101  |  &#8199;&#8199;&#8199;13  |  &#8199;&#8199;Q&#8199;&#8199;  |  &#8199;&#8199;&#8199;Ｑ&#8199;&#8199;  |  &#8199;&#8199;👸&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>King</term>
///         <description>
///             <c>0b0000_1110  |  &#8199;&#8199;&#8199;14  |  &#8199;&#8199;K&#8199;&#8199;  |  &#8199;&#8199;&#8199;Ｋ&#8199;&#8199;  |  &#8199;&#8199;🤴&#8199;&#8199;</c>
///         </description>
///     </item>
///     <item>
///         <term>Joker</term>
///         <description>
///             <c>0b0000_1111  |  &#8199;&#8199;&#8199;15  |  &#8199;&#8199;~&#8199;&#8199;  |  &#8199;&#8199;&#8199;～&#8199;&#8199;  |  &#8199;&#8199;🃏&#8199;&#8199;</c>
///         </description>
///     </item>
/// </list>
/// </summary>
/// <remarks>
/// The 1st through 4th bits of a <see cref="Card"/> <see cref="byte"/>
/// </remarks>
/// <default><see cref="Rank.None"/></default>
/// <mask><c>0b0000_1111</c></mask>
/// <seealso href="https://en.wikipedia.org/wiki/Playing_Cards_(Unicode_block)"/>
[PublicAPI]
[Flags]
public enum Rank : byte
{
    None = 0b0000,
    Ace = 0b0001,
    Two = 0b0010,
    Three = 0b0011,
    Four = 0b0100,
    Five = 0b0101,
    Six = 0b0110,
    Seven = 0b0111,
    Eight = 0b1000,
    Nine = 0b1001,
    Ten = 0b1010,
    Jack = 0b1011,
    Knight = 0b1100,
    Queen = 0b1101,
    King = 0b1110,
    Joker = 0b1111,
}

[PublicAPI]
public static class RankExtensions
{
    internal const byte MASK = 0b0000_1111;
    
    static RankExtensions()
    {
        DisplayFormat
            .Register<Rank>()
            .FormatAs(Rank.None, nameof(Rank.None), "_", "？", "⬜")
            .FormatAs(Rank.Ace, nameof(Ace), "A", "Ａ", "🅰️")
            .FormatAs(Rank.Two, nameof(Two), "2", "２", "②")
            .FormatAs(Rank.Three, nameof(Three), "3", "３", "③")
            .FormatAs(Rank.Four, nameof(Four), "4", "４", "④")
            .FormatAs(Rank.Five, nameof(Five), "5", "５", "⑤")
            .FormatAs(Rank.Six, nameof(Six), "6", "６", "⑥")
            .FormatAs(Rank.Seven, nameof(Seven), "7", "７", "⑦")
            .FormatAs(Rank.Eight, nameof(Eight), "8", "８", "⑧")
            .FormatAs(Rank.Nine, nameof(Nine), "9", "９", "⑨")
            .FormatAs(Rank.Ten, nameof(Ten), "X", "Ｘ", "🔟")
            .FormatAs(Rank.Jack, nameof(Jack), "J", "Ｊ", "👦")
            .FormatAs(Rank.Knight, nameof(Knight), "N", "Ｎ", "🐴")
            .FormatAs(Rank.Queen, nameof(Queen), "Q", "Ｑ", "👸")
            .FormatAs(Rank.King, nameof(King), "K", "Ｋ", "🤴")
            .FormatAs(Rank.Joker, nameof(Joker), "~", "～", "🃏");
    }

    extension(Rank)
    {
        public static Card operator |(Rank rank, Suit suit)
        {
            return (Card)((byte)rank | (byte)suit);
        }
        
        public static Card operator +(Rank rank, Suit suit)
        {
            return (Card)((byte)rank | (byte)suit);
        }

        public static Card operator |(Rank rank, Card card) => card.With(rank);
        
        public static Card operator +(Rank rank, Card card) => card.With(rank);
        
        
        public static byte Mask => MASK;
        
        public static int BitCount => 4;
        
        public static Rank Default => Rank.None;
    }

    extension(Rank rank)
    {
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
            if (rank == Ace)
                return onAce();
            if (rank == Two)
                return onTwo();
            if (rank == Three)
                return onThree();
            if (rank == Four)
                return onFour();
            if (rank == Five)
                return onFive();
            if (rank == Six)
                return onSix();
            if (rank == Seven)
                return onSeven();
            if (rank == Eight)
                return onEight();
            if (rank == Nine)
                return onNine();
            if (rank == Ten)
                return onTen();
            if (rank == Jack)
                return onJack();
            if (rank == Knight)
                return onKnight();
            if (rank == Queen)
                return onQueen();
            if (rank == King)
                return onKing();
            if (rank == Joker)
                return onJoker();
            throw Ex.UndefinedEnum(rank);
        }
    }
}