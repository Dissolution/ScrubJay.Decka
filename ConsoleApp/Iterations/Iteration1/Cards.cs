namespace ScrubJay.Decka.Sandbox.Iterations.Iteration1;

public static class Cards
{
    public static IReadOnlyList<Card> Standard52 { get; }

    static Cards()
    {
        List<Card> standard52 = new(52);
        foreach (var suit in Enum.GetValues<Suit>())
        foreach (var rank in Enum.GetValues<Rank>())
        {
            Card card = new Card(rank, suit);
            standard52.Add(card);
        }
        Standard52 = standard52;
    }
}