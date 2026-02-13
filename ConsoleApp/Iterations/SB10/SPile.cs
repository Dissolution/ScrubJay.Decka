using System.Diagnostics;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

[StructLayout(LayoutKind.Explicit)]
public ref struct SpanPile
{
    [FieldOffset(0)]
    public readonly byte Capacity;

    [FieldOffset(1)]
    public byte Count;

    [FieldOffset(2)]
    private ref Card _firstCard;

    public ref Card this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if ((uint)index >= Count)
                throw Ex.Index(index, Count);
            return ref Unsafe.Add(ref _firstCard, (nint)(uint)index /* force zero-extension */);
        }
    }

    public ref Card this[StackIndex index]
    {
        get
        {
            int offset = index.GetOffset(Count);
            if ((uint)offset >= Count)
                throw Ex.Index(index, Count);
            return ref Unsafe.Add(ref _firstCard, (nint)(uint)offset);
        }
    }

    public bool IsEmpty => Count == 0;

    public SpanPile(Span<Card> buffer)
    {
        Capacity = (byte)buffer.Length;
        Count = 0;
        _firstCard = ref buffer.GetPinnableReference();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref Card CardAt(int index)
        => ref Unsafe.Add(ref _firstCard, (nint)(uint)index);

    public void Push(Card card)
    {
        if (Count >= Capacity)
            throw Ex.Invalid($"Cannot push {card}: No capacity remains");
        CardAt(Count) = card;
        Count++;
    }

    public void PushMany(scoped ReadOnlySpan<Card> cards)
    {
        int count = cards.Length;
        if (Count + count > Capacity)
            throw Ex.Invalid($"Cannot push {count} cards: Only {Capacity - Count} capacity remains");
        CardHelper.Notsafe.CopyTo(
            in cards.GetPinnableReference(),
            ref CardAt(Count),
            count);
        Count = (byte)(Count + count);
    }

    public Card Peek()
    {
        if (Count == 0)
            throw Ex.Invalid($"Cannot peek: No cards in pile");
        
        return CardAt(Count - 1);
    }

    public ReadOnlySpan<Card> PeekMany(int count)
    {
        if (count <= 0)
            return [];
        
        if (count > Count)
            throw Ex.Arg(count, $"Cannot peek {count} cards: Only {Count} cards in pile");
        
        unsafe
        {
            return new ReadOnlySpan<Card>(
                Notsafe.RefAsVoidPtr<Card>(ref CardAt(Count - count)),
                count);
        }
    }
    
    public Card Pop()
    {
        if (Count == 0)
            throw Ex.Invalid($"Cannot pop: No cards in pile");

        // fake pop, we just decrement count, we never clear the value
        Count--;
        return CardAt(Count);
    }

    public ReadOnlySpan<Card> PopMany(int count)
    {
        if (count <= 0)
            return [];
        
        if (count > Count)
            throw Ex.Arg(count, $"Cannot pop {count} cards: Only {Count} cards in pile");

        // also a fake pop
        Count = (byte)(Count - count);
        unsafe
        {
            return new ReadOnlySpan<Card>(
                Notsafe.RefAsVoidPtr<Card>(ref CardAt(Count)),
                count);
        }
    }
    
    public bool TryCopyTo(Span<Card> destination, bool
}