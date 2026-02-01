using static ScrubJay.Decka.Sandbox.Iterations.SB10.Rank;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

/// <summary>
/// A <see cref="Card"/>'s Rank:
/// <list type="bullet">
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
    static RankExtensions()
    {
          var dm = DisplayFormat.For<Rank>();
        dm.Map(Rank.None)
            .Add(DisplayFormat.ToString, nameof(Rank.None))
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

    extension(Rank)
    {
        public static byte Mask => 0b0000_1111;
        
        public static int BitCount => 4;
        
        public static Rank Default => Rank.None;

        public static Result<Rank> TryParse(string str)
        {
            return DisplayFormat.For<Rank>().TryParse(str);
        }
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
        
        public string ToString(DisplayFormat format)
        {
            return DisplayFormat.For<Rank>().Format(rank, format);
        }
    }
}