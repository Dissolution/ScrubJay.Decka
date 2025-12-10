using System.Numerics;
using ScrubJay.Decka.Sandbox.Iterations.StackBased.Display;
using ScrubJay.Parsing;

namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

public interface ICardPart<P> :
    ITryParsable<P>,
    IHasDefault<P>,
    IDeepCloneable<P>,
    IEqualityOperators<P, P, bool>,
    IEquatable<P>
    where P : struct, ICardPart<P>
{
    static abstract implicit operator P(byte u8);
    static abstract implicit operator byte(P part);
    
    static abstract byte Mask { get; }

    static abstract int BitCount { get; }

    string ToString(DisplayFormat format);
}