namespace ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;

[PublicAPI]
public static class DisplayFormatExtensions
{
    extension(DisplayFormat)
    {
        /// <summary>
        /// Start registering the mapping of <typeparamref name="TEnum"/> members to <see cref="DisplayFormat"/> renderings.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisplayFormatter.DisplayFormatRegistration<TEnum> Register<TEnum>()
            where TEnum : struct, Enum
            => DisplayFormatter.Register<TEnum>();
    }


    extension<E>(E)
        where E : struct, Enum
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<E> TryParse(string? str, StringComparison comparison = StringComparison.Ordinal) =>
            DisplayFormatter.TryParse<E>(str, comparison);
    }

    
    
}