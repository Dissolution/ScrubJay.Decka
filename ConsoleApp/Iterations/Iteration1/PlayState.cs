using ScrubJay.Maths;
using ScrubJay.Randomization;
using ScrubJay.Randomization.Seeding;

namespace ScrubJay.Decka.Sandbox.Iterations.Iteration1;

public class PlayState
{
    private readonly PrngBase _random;

    public RandSeed Seed => _random.Seed;
    public Board Board { get; }
    public Rational Chance { get; }
    public List<Move> Moves { get; }

    public bool IsFinished => Board.Deck.Count == 0;

    public PlayState()
    {
        _random = new SmallPrng(RandSeed.Known());
        this.Board = new Board();
        Board.Deck.AddRange(Cards.Standard52);
        this.Chance = Rational.One;
        this.Moves = [];
    }
}