


using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ScrubJay.Decka.Sandbox;

[StructLayout(LayoutKind.Sequential, Size = 1, Pack = 1)]
public struct Card
{
    private const byte RANK_MASK = 0b0000_1111;
    private const byte SUIT_MASK = 0b00_11_0000;

    private readonly byte _value;
    
    public Rank Rank
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (Rank)(_value & RANK_MASK);
    }
    
    public Suit Suit
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (Suit)((_value & SUIT_MASK) >> 4);
    }
}

public enum Suit : byte
{
    Spade,
    Diamond,
    Club,
    Heart,
}

public enum Rank : byte
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace,
}


public class GameState
{
    public Stack<Card> this[int pile]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (pile == 0)
                return Deck;
            if (pile == 1)
                return PileA;
            if (pile == 2)
                return PileB;
            if (pile == 3)
                return PileC;
            if (pile == 4)
                return PileD;
            if (pile == 5)
                return Discard;
            throw new UnreachableException();
        }
    }
    
    public Stack<Card> Deck { get; }
    public Stack<Card> PileA { get; }
    public Stack<Card> PileB { get; }
    public Stack<Card> PileC { get; }
    public Stack<Card> PileD { get; }
    public Stack<Card> Discard { get; }

    public GameState()
    {
        Deck = new Stack<Card>(52);
        PileA = new Stack<Card>(13);
        PileB = new Stack<Card>(13);
        PileC = new Stack<Card>(13);
        PileD = new Stack<Card>(13);
        Discard = new Stack<Card>(52-4);
    }

    public void Deconstruct(out Stack<Card> pileA, out Stack<Card> pileB, out Stack<Card> pileC, out Stack<Card> pileD)
    {
        pileA = PileA;
        pileB = PileB;
        pileC = PileC;
        pileD = PileD;
    }

    public GameStatus GetStatus()
    {
        bool win = Deck.Count == 0 &&
                   PileA.TryPeek(out var a) && a.Rank == Rank.Ace &&
                   PileB.TryPeek(out var b) && b.Rank == Rank.Ace &&
                   PileC.TryPeek(out var c) && c.Rank == Rank.Ace &&
                   PileD.TryPeek(out var d) && d.Rank == Rank.Ace;
        int pileCount = PileA.Count + PileB.Count + PileC.Count + PileD.Count;
        return new (win, pileCount);
    }
}

public record GameStatus(bool Win, int PileCount);

public class Player
{
    private bool Deal(GameState state)
    {
        if (state.Deck.Count < 4)
            return false;
        state.Deck.Pop().PushTo(state.PileA);
        state.Deck.Pop().PushTo(state.PileB);
        state.Deck.Pop().PushTo(state.PileC);
        state.Deck.Pop().PushTo(state.PileD);
        return true;
    }

    private bool Collide(GameState state)
    {
        bool collided = false;
        Stack<Card>[] piles = [state.PileA, state.PileB, state.PileC, state.PileD];
        for (var s = 0; s < 3; s++)
        {
            SOURCE_START:
            
            if (piles[s].TryPeek(out var source))
            {
                for (var t = s + 1; t < 4; t++)
                {
                    TARGET_START:
                    
                    if (piles[t].TryPeek(out var target))
                    {
                        if (target.Suit == source.Suit)
                        {
                            collided = true;
                            if (target.Rank > source.Rank)
                            {
                                // destroy source
                                piles[s].PopNPush(state.Discard);
                                // continue checking with source again
                                goto SOURCE_START;
                            }
                            else
                            {
                                Debug.Assert(target.Rank < source.Rank); // cannot be equal
                                piles[t].PopNPush(state.Discard);
                                // continue checking with target again
                                goto TARGET_START;
                            }
                        }
                    }
                }
            }
        }

        return collided;
    }

    private GameState[] Gravity(GameState state)
    {
        Span<int> heights = stackalloc int[4];
        heights[0] = state.PileA.Count;
        heights[1] = state.PileB.Count;
        heights[2] = state.PileC.Count;
        heights[3] = state.PileD.Count;
        
    }
}