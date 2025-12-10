namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

public class PartComparer<P> : IEqualityComparer<P>, IHasDefault<PartComparer<P>>
    where P : struct, ICardPart<P>
{
    public static PartComparer<P> Default { get; } = new ();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(P x, P y)
    {
        return x == y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetHashCode(P obj)
    {
        return obj.GetHashCode();
    }
}