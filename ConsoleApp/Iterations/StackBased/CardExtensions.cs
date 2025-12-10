using static InlineIL.IL;

namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

[PublicAPI]
public static class CardExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void BitwiseCopy(in Card source, ref Card destination, int count)
    {
        Emit.Ldarg(nameof(destination));
        Emit.Ldarg(nameof(source));
        Emit.Ldarg(nameof(count));
        Emit.Cpblk();
    }
    
    extension(ReadOnlySpan<Card> cards)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<byte> AsBytes()
        {
            return MemoryMarshal.Cast<Card, byte>(cards);
        }

        internal void NotsafeCopyTo(Span<Card> destination)
        {
            BitwiseCopy(in cards.GetPinnableReference(), ref destination.GetPinnableReference(), cards.Length);
        }
    }

    public static Card[] DeepClone(this Card[] cards)
    {
        int count = cards.Length;
        if (count == 0)
            return [];
        Card[] clone = new Card[count];
        BitwiseCopy(in MemoryMarshal.GetArrayDataReference(cards),
            ref MemoryMarshal.GetArrayDataReference(clone),
            count);
        return clone;
    }
    
    
    
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<byte> AsBytes(this Span<Card> cards)
    {
        return MemoryMarshal.Cast<Card, byte>(cards);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<Card> AsCards(this Span<byte> bytes)
    {
        return MemoryMarshal.Cast<byte, Card>(bytes);
    }

  
    


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<Card> AsCards(this ReadOnlySpan<byte> bytes)
    {
        return MemoryMarshal.Cast<byte, Card>(bytes);
    }
}