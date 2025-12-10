namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

[StructLayout(LayoutKind.Explicit, Size = (52 + (13 * 4) + 48))]
public ref struct Board
{
    [FieldOffset(0)]
    public Pile Deck;

    [FieldOffset(52)] // deck is 52 cards
    public Pile PileA;

    [FieldOffset(52 + 13)] // each pile is a max of 13 cards
    public Pile PileB;

    [FieldOffset(52 + 13 + 13)]
    public Pile PileC;

    [FieldOffset(52 + 13 + 13 + 13)]
    public Pile PileD;

    [FieldOffset(52 + 13 + 13 + 13 + 13)]   // discard is a max of 48 cards
    public Pile Discard;
}