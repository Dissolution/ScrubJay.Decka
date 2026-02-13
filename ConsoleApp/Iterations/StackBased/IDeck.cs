namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

public interface IDeck<D> : IHasDefault<D>
    where D : IDeck<D>
{
    Suit[] Suits { get; }

    Rank[] Ranks { get; }
    
    Card[] Cards { get; }
}