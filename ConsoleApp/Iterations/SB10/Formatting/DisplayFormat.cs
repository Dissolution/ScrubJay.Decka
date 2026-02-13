namespace ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;

/// <summary>
/// A particular way to format an <see cref="Enum"/> instance.
/// </summary>
[PublicAPI]
public enum DisplayFormat
{
    /// <summary>
    /// Default format is calling <see cref="M:object.ToString()"/>
    /// </summary>
    Default = 0,

    /// <summary>
    /// Short-form of the name, 1-2 characters
    /// </summary>
    Short = 1,

    /// <summary>
    /// Unicode representation
    /// </summary>
    Unicode = 2,

    /// <summary>
    /// Emoji representation
    /// </summary>
    Emoji = 3,
}