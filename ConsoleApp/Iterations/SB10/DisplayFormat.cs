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

//    Name = 0,
//    Black = 1 << 0,
//    Solid = 1 << 0,
//    White = 1 << 1,
//    Hollow = 1 << 1,
//    Unicode = 1 << 2,
//    Emoji = 1 << 3,
//    Character = 1 << 4,
}