namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

public sealed class Standard52 : IDeck<Standard52>
{
    public static Standard52 Default { get; } = new();
    
    public static RelativeIndexComparer<Rank> AceLow { get; } =
        new([Rank.Ace, Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King]);
    
    public static RelativeIndexComparer<Rank> AceHigh { get; } =
        new([Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace]);

    
    public Suit[] Suits { get; } =
    [
        Suit.Spade,
        Suit.Diamond,
        Suit.Club,
        Suit.Heart,
    ];

    public Rank[] Ranks { get; } =
    [
        Rank.Ace,
        Rank.Two,
        Rank.Three,
        Rank.Four,
        Rank.Five,
        Rank.Six,
        Rank.Seven,
        Rank.Eight,
        Rank.Nine,
        Rank.Ten,
        Rank.Jack,
        Rank.Queen,
        Rank.King,
    ];

    public Card[] Cards { get; }

    public Standard52()
    {
        Cards = new Card[52];
        int i = 0;
        foreach (var suit in Suits)
        foreach (var rank in Ranks)
        {
            var card = Card.New(rank, suit);
            Cards[i++] = card;
        }
    }

}