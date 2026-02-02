using System.Diagnostics;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

[PublicAPI]
public interface IDeck
{
    Suit[] Suits { get; }

    Rank[] Ranks { get; }

    Card[] Cards { get; }
}

[PublicAPI]
public sealed class Standard52 : IDeck
{
    public Suit[] Suits { get; } = [Suit.Spade, Suit.Diamond, Suit.Club, Suit.Heart];

    public Rank[] Ranks { get; } =
    [
        Rank.Ace,
        Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten,
        Rank.Jack, Rank.Queen, Rank.King,
    ];

    public Card[] Cards { get; }

    public Standard52()
    {
        Cards = new Card[52];
        int c = 0;

        foreach (var suit in Suits)
        foreach (var rank in Ranks)
        {
            Cards[c++] = suit | rank;
        }

        Debug.Assert(c == 52);
    }
}

[PublicAPI]
public static class Decks
{
    public static IDeck Standard52 { get; } = new Standard52();
}