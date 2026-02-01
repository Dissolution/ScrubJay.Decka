#define CHECK_BOUNDS

namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

[StructLayout(LayoutKind.Sequential)]
public ref struct Pile
{
    internal readonly byte _capacity;
    internal byte _count;
    internal readonly ref Card _firstCard;

    internal ref Card OpenSlot
    {
#if CHECK_BOUNDS
        get
        {
            if (_count >= _capacity)
                throw Ex.Invalid();
            return ref Notsafe.Add(ref _firstCard, _count);
        }
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Notsafe.Add(ref _firstCard, _count);
#endif
    }

    public ref Card this[int offset]
    {
#if CHECK_BOUNDS
        get
        {
            if ((uint)offset >= (uint)_count)
                throw Ex.Arg(offset);
            return ref Unsafe.Add<Card>(ref _firstCard, offset);
        }
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Unsafe.Add<Card>(ref _firstCard, offset);
#endif
    }

    public ref Card this[StackIndex stackIndex]
    {
#if CHECK_BOUNDS
        get
        {
            int offset = stackIndex.GetOffset(_count);
            if ((uint)offset >= (uint)_count)
                throw Ex.Arg(stackIndex);
            return ref Unsafe.Add<Card>(ref _firstCard, offset);
        }
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            int offset = stackIndex.GetOffset(_count);
            return ref Unsafe.Add<Card>(ref _firstCard, offset);
        }
#endif
    }

    public int Count => _count;

    public bool IsEmpty => _count == 0;

    public Pile(Span<Card> cards, byte count = 0)
    {
        _capacity = (byte)cards.Length;
        _firstCard = ref cards.GetPinnableReference();
        _count = count;
    }

    public void Push(Card card)
    {
#if CHECK_BOUNDS
        if (_count >= _capacity)
            throw Ex.Invalid();
#endif
        OpenSlot = card;
        _count++;
    }

    public void PushMany(params ReadOnlySpan<Card> cards)
    {
#if CHECK_BOUNDS
        if (_count + cards.Length > _capacity)
            throw Ex.Invalid();
#endif
        Cards.CopyTo(in cards.GetPinnableReference(), ref OpenSlot, cards.Length);
    }


    public bool TryCopyTo(Span<Card> destination, bool popOrder = true)
    {
        if (destination.Length < _count)
            return false;

        Cards.CopyTo(in _firstCard, ref destination.GetPinnableReference(), _count);
        if (popOrder)
        {
            destination[.._count].Reverse();
        }

        return true;
    }

    public Card Pop()
    {
#if CHECK_BOUNDS
        if (_count <= 0)
            throw Ex.Invalid();
#endif
        var card = Interlocked.Exchange<Card>(ref Notsafe.Add(ref _firstCard, _count), default);
        _count--;
        return card;
    }

    public bool TryPop(out Card card)
    {
        if (_count > 0)
        {
            card = Interlocked.Exchange<Card>(ref Notsafe.Add(ref _firstCard, _count), default);
            _count--;
            return true;
        }
        else
        {
            card = default;
            return false;
        }
    }


    public Card Peek()
    {
#if CHECK_BOUNDS
        if (_count <= 0)
            throw Ex.Invalid();
#endif
        var card = Notsafe.Add(ref _firstCard, _count);
        return card;
    }

    public void Clear()
    {
        Cards.ClearCards(ref _firstCard, _count);
        _count = 0;
    }

    public Span<Card> AsSpan()
    {
        unsafe
        {
            return new Span<Card>(Notsafe.RefAsVoidPtr(ref _firstCard), _count);
        }
    }
}