using static InlineIL.IL;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

[PublicAPI]
public static class Cards
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
        internal static void CopyTo(in Card source, ref Card destination, int count)
        {
            // dest addr, source addr, byte count
            Emit.Ldarg(nameof(destination));
            Emit.Ldarg(nameof(source));
            Emit.Ldarg(nameof(count));
            Emit.Cpblk();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CopyTo(in Card source, ref byte destination, int count)
        {
            // dest addr, source addr, byte count
            Emit.Ldarg(nameof(destination));
            Emit.Ldarg(nameof(source));
            Emit.Ldarg(nameof(count));
            Emit.Cpblk();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CopyTo(in byte source, ref Card destination, int count)
        {
            // dest addr, source addr, byte count
            Emit.Ldarg(nameof(destination));
            Emit.Ldarg(nameof(source));
            Emit.Ldarg(nameof(count));
            Emit.Cpblk();
        }
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
            Notsafe.CopyTo(in cards.GetPinnableReference(), ref destination.GetPinnableReference(), cards.Length);
        }
    }

    extension(Card[] cards)
    {
        public Card[] DeepClone()
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
}