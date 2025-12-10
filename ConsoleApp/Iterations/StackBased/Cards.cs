using static InlineIL.IL;

namespace ScrubJay.Decka.Sandbox.Iterations.StackBased;

public static class Cards
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void ClearCards(ref Card source, int count)
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