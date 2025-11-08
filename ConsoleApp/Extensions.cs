namespace ScrubJay.Decka.Sandbox;

public static class Extensions
{
    extension(Card card)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushTo(Stack<Card> stack) => stack.Push(card);
    }

    extension(Stack<Card> pile)
    {
        public void PopNPush(Stack<Card> other)
        {
            other.Push(pile.Pop());
        }
    }
}