namespace ScrubJay.Decka.Sandbox.Iterations.StackBased.Display;

public sealed class DisplayFormatMap<P>
    where P : struct, ICardPart<P>
{
    public static DisplayFormatMap<P> Default { get; } = new();

    public sealed class PartMap
    {
        private readonly DisplayFormatMap<P> _map;
        
        public P Part { get; }

        public PartMap(DisplayFormatMap<P> map, P part)
        {
            _map = map;
            Part = part;
        }
        
        public PartMap Add(DisplayFormat format, string str)
        {
            _map.Add(Part, format, str);
            return this;
        }
    }
    

    private readonly Dictionary<P, string?[]> _map = new(PartComparer<P>.Default);


    private string?[] GetOrAdd(P part)
    {
        if (_map.TryGetValue(part, out var strings))
            return strings;

        strings = new string?[DisplayFormat.TotalFormatCount];
        _map[part] = strings;
        return strings;
    }

    public PartMap Map(P part)
    {
        return new PartMap(this, part);
    }
    
    public DisplayFormatMap<P> Add(P part, DisplayFormat format, string str)
    {
        var strings = GetOrAdd(part);
        strings[(int)format] = str;
        return this;
    }

    public Result<P> TryParse(string? input)
    {
        foreach (var pair in _map)
        {
            foreach (string? str in pair.Value)
            {
                if (input.Equate(str, StringComparison.OrdinalIgnoreCase))
                    return Ok(pair.Key);
            }
        }

        return Ex.Parse<P>(input);
    }

    public string Format(P part, DisplayFormat format)
    {
        string? str;

        if (_map.TryGetValue(part, out var strings))
        {
            str = strings[(int)format];
        }
        else
        {
            // unregistered
            str = "!!!!!";
        }

        return str ?? string.Empty;
    }
}