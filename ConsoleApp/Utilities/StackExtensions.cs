namespace ScrubJay.Decka.Sandbox.Utilities;

public static class StackExtensions
{
    extension<T>(Stack<T> stack)
    {
        public void PushMany(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                stack.Push(item);
            }
        }
    }
}