// using ScrubJay.Validation;
//
// namespace ScrubJay.Decka.Sandbox.Collections;
//
// public class Pile<T> : IList<T>, IReadOnlyList<T>, ICollection<T>, IReadOnlyCollection<T>, IEnumerable<T>
// {
//     private T[] _items;
//     private int _position;
//     
//     
// }
//
// public static class PileExtensions
// {
//     extension<T>(Pile<T> pile)
//         where T : ICloneable<T>
//     {
//         public Pile<T> Clone()
//         {
//             throw Ex.NotImplemented();
//         }
//     }
//     
//     extension<T>(Pile<T> pile)
//         where T : IDeepCloneable<T>
//     {
//         public Pile<T> DeepClone()
//         {
//             throw Ex.NotImplemented();
//         }
//     }
// }