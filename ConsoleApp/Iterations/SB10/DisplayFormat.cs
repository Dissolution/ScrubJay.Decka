namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

[PublicAPI]
public enum DisplayFormat
{
    /// <summary>
    /// Default format is calling <see cref="M:object.ToString()"/>
    /// </summary>
    ToString = 0,
    
    Short,
    Unicode,
    Emoji,
}