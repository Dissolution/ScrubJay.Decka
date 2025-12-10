namespace ScrubJay.Decka.Sandbox.Iterations.Iteration1;

public class Board : ICloneable<Board>
{
    public List<Card> Deck { get; } = new(52);
    public List<Card> PileA { get; } = new(13);
    public List<Card> PileB { get; } = new(13);
    public List<Card> PileC { get; } = new(13);
    public List<Card> PileD { get; } = new(13);
    public List<Card> Discard { get; } = new(48);


    public void Deconstruct(out List<Card> pileA, out List<Card> pileB, out List<Card> pileC, out List<Card> pileD)
    {
        pileA = PileA;
        pileB = PileB;
        pileC = PileC;
        pileD = PileD;
    }

    public Board Clone()
    {
        var clone = new Board();
        clone.Deck.AddRange(this.Deck);
        clone.PileA.AddRange(this.PileA);
        clone.PileB.AddRange(this.PileB);
        clone.PileC.AddRange(this.PileC);
        clone.PileD.AddRange(this.PileD);
        clone.Discard.AddRange(this.Discard);
        return clone;
    }
}