namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

public class BoardLog : IDeepCloneable<BoardLog>
{
    private readonly List<BoardMove> _moves;

    public BoardLog()
    {
        _moves = [];
    }

    public BoardLog(IEnumerable<BoardMove> moves)
    {
        _moves = moves.ToList();
    }

    public void Add(BoardMove move)
    {
        _moves.Add(move);
    }

    public void Add(PileIndex source, PileIndex destination, Card card, MoveReason reason)
    {
        _moves.Add(new BoardMove(source, destination, card, reason));
    }

    public BoardLog DeepClone()
    {
        return new(_moves.Select(static move => move.DeepClone()));
    }
}

public enum PileIndex
{
    Deck,
    PileA,
    PileB,
    PileC,
    PileD,
    Discard,
}

public enum MoveReason
{
    Deal,
    Collision,
    Gravity,
    Choice,
}

public sealed record class BoardMove(PileIndex Source, PileIndex Destination, Card Card, MoveReason Reason)
    : IDeepCloneable<BoardMove>
{
    public BoardMove DeepClone()
    {
        return new(Source, Destination, Card, Reason);
    }
}