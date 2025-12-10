namespace ScrubJay.Decka.Sandbox.Iterations.Iteration1;

public sealed record class Card(Rank Rank, Suit Suit)
{
    public override string ToString() => $"{Rank} of {Suit}s";
}