namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

[PublicAPI]
public static class PileExtensions
{
    /* Pile Serialization
     * Since we do not care about the highest 2 bits of any given card, we're going to bitshift the cards
     * into a much more compact representation
     *
     *
     * 
     
     */
    
    extension(Pile)
    {
        public static RefResult<Pile> TryRead(Span<byte> bytes)
        {
            if (bytes.Length < 2)
                return Ex.Arg(bytes);
            byte capacity = bytes[0];
            byte count = bytes[1];
            if (bytes.Length != (capacity + 2))
                return Ex.Arg(bytes);
            Pile pile = new(bytes[2..].AsCards(), count);
            return RefResult<Pile>.Ok(pile);
        }
    }

    extension(ref Pile pile)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PopAndPushTo(ref Pile destPile)
        {
            destPile.Push(pile.Pop());
        }
    }

    extension(Card card)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushTo(ref Pile pile)
        {
            pile.Push(card);
        }
    }


    public static Span<byte> AsBytes(this ref Pile pile)
    {
        unsafe
        {
            void* ptr = Notsafe.RefAsVoidPtr(ref pile);
            Span<byte> bytes = new Span<byte>(ptr, pile._count + 2);
            return bytes;
        }
    }
}