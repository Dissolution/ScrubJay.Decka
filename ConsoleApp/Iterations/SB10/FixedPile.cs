using System.Diagnostics;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

public sealed class FixedPile : IDeepCloneable<FixedPile>
{
    private Card[] _cards;
    private int _position;

    public int Count => _position;

    public bool IsEmpty => _position == 0;

    public int Capacity => _cards.Length;

    public ref Card this[int offset]
    {
        get
        {
            Debug.Assert(offset >= 0 && offset < _position);
            return ref _cards[offset];
        }
    }
    
    public ref Card this[StackIndex index]
    {
        get
        {
            int offset = index.GetOffset(_position);
            Debug.Assert(offset >= 0 && offset < _position);
            return ref _cards[offset];
        }
    }
    

    public FixedPile(int capacity)
    {
        _cards = new Card[capacity];
        _position = 0;
    }

    public FixedPile(params Card[] cards)
    {
        _cards = cards;
        _position = 0;
    }

    public void Push(Card card)
    {
        Debug.Assert(_position < Capacity);
        _cards[_position++] = card;
    }

    public void PushMany(ReadOnlySpan<Card> cards)
    {
        Debug.Assert(_position + cards.Length <= Capacity);
        CardHelper.Notsafe.CopyTo(cards, _cards.AsSpan(_position));
        _position += cards.Length;
    }

    public Card Pop()
    {
        Debug.Assert(_position > 0);
        _position--;
        Card card = _cards[_position];
        return card;
    }

    public ReadOnlySpan<Card> PopMany(int count)
    {
        Debug.Assert(count >= 0);
        Debug.Assert(_position + count <= Capacity);
        var slice = _cards.AsSpan(_position - count, count);
        _position -= count;
        return slice;
    }
    
    public Card Peek()
    {
        Debug.Assert(_position > 0);
        Card card = _cards[_position-1];
        return card;
    }

    public FixedPile DeepClone()
    {
        var clone = new FixedPile(Capacity);
        CardHelper.Notsafe.CopyTo(_cards.AsSpan(0, _position), clone._cards);
        clone._position = _position;
        return clone;
    }
}