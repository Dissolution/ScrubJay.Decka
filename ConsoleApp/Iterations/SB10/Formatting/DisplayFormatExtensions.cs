namespace ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;

[PublicAPI]
public static class DisplayFormatExtensions
{
    extension(DisplayFormat)
    {
        /// <summary>
        /// Start registering the mapping of <typeparamref name="E"/> members to <see cref="DisplayFormat"/> renderings.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisplayFormatRegistration<E> Register<E>()
            where E : struct, Enum 
            => DisplayFormatRegistration<E>.Instance;
    }


    extension<E>(E)
        where E : struct, Enum
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<E> TryParse(string? str, StringComparison comparison = StringComparison.Ordinal) =>
            DisplayFormatRegistration<E>.Instance
                .TryParse(str, comparison);
    }

    extension<E>(E @enum)
        where E : struct, Enum
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(DisplayFormat format) 
            => DisplayFormatRegistration<E>.Instance.Format(@enum, format);
    }
    
    
}