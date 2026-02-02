namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

public interface IBoard<TSelf> : IDeepCloneable<TSelf>
    where TSelf : IBoard<TSelf>
{
}

public class AcesUpBoard : IBoard<AcesUpBoard>
{
    public Pile Deck { get; init; } = new(52);

    public Pile PileA { get; init; } = new(13);
    public Pile PileB { get; init; } = new(13);
    public Pile PileC { get; init; } = new(13);
    public Pile PileD { get; init; } = new(13);

    public Pile Discard { get; init; } = new(48);

    public BoardLog Log { get; init; } = new();

    public Pile this[PileIndex index] => index switch
        {
            PileIndex.Deck => Deck,
            PileIndex.PileA => PileA,
            PileIndex.PileB => PileB,
            PileIndex.PileC => PileC,
            PileIndex.PileD => PileD,
            PileIndex.Discard => Discard,
            _ => throw Ex.UndefinedEnum(index)
        };


    public void Move(PileIndex source, PileIndex dest, MoveReason reason)
    {
        var card = this[source].Pop();
        this[dest].Push(card);
        Log.Add(source, dest, card, reason);
    }
    
    
    public AcesUpBoard DeepClone()
    {
        var clone = new AcesUpBoard()
        {
            Deck = this.Deck.DeepClone(),
            PileA = this.PileA.DeepClone(),
            PileB = this.PileB.DeepClone(),
            PileC = this.PileC.DeepClone(),
            PileD = this.PileD.DeepClone(),
            Discard = this.Discard.DeepClone(),
            Log = this.Log.DeepClone(),
        };
        return clone;
    }
}