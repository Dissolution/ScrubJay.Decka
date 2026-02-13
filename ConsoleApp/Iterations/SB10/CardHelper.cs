using static InlineIL.IL;
// ReSharper disable EntityNameCapturedOnly.Global

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

[PublicAPI]
public static class CardHelper
{
    internal class Notsafe
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear(ref Card source, int count)
        {
            // address, value, byte count
            Emit.Ldarg(nameof(source));
            Emit.Ldc_I4_0();
            Emit.Ldarg(nameof(count));
            Emit.Initblk();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo(in Card source, ref Card destination, int count)
        {
            // dest addr, source addr, byte count
            Emit.Ldarg(nameof(destination));
            Emit.Ldarg(nameof(source));
            Emit.Ldarg(nameof(count));
            Emit.Cpblk();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo(scoped ReadOnlySpan<Card> source, scoped Span<Card> destination) 
            => CopyTo(in source.GetPinnableReference(), ref destination.GetPinnableReference(), source.Length);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Clear(Span<Card> cards)
    {
        Notsafe.Clear(ref cards.GetPinnableReference(), cards.Length);
    }

    public static bool TryCopyTo(scoped ReadOnlySpan<Card> source, scoped Span<Card> destination)
    {
        int count = source.Length;
        if (count > destination.Length)
            return false;
        Notsafe.CopyTo(in source.GetPinnableReference(), ref destination.GetPinnableReference(), count);
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<byte> AsBytes(ReadOnlySpan<Card> cards)
    {
        return MemoryMarshal.Cast<Card, byte>(cards);
    }

    public static Card[] DeepClone(Card[] cards)
    {
        int count = cards.Length;
        if (count == 0)
            return [];
        Card[] clone = new Card[count];
        Notsafe.CopyTo(in MemoryMarshal.GetArrayDataReference(cards),
            ref MemoryMarshal.GetArrayDataReference(clone),
            count);
        return clone;
    }
}