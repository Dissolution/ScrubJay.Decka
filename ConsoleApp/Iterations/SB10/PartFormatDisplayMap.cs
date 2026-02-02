using ScrubJay.Decka.Sandbox.Iterations.StackBased;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

public sealed class PartFormatDisplayMap<TEnum> : Dictionary<TEnum, FormatDisplayMap>
    where TEnum : struct, Enum
{
    public static PartFormatDisplayMap<TEnum> Instance { get; } = new();

    private PartFormatDisplayMap()
    {
    }

    public FormatDisplayMap Map(TEnum part)
    {
        if (!this.TryGetValue(part, out var submap))
        {
            submap = new FormatDisplayMap();
            this[part] = submap;
        }

        return submap;
    }


    public Result<TEnum> TryParse(string? str)
    {
        foreach (var pair in this)
        {
            foreach (var subpair in pair.Value)
            {
                if (subpair.Value == str)
                    return pair.Key;
            }
        }
        
        return Ex.Parse<TEnum>(str);
    }

    public string Display(TEnum part, DisplayFormat format)
    {
        if (this.TryGetValue(part, out var submap))
        {
            if (submap.TryGetValue(format, out var display))
                return display;
        }

        return "???";
    }
}

public sealed class FormatDisplayMap : Dictionary<DisplayFormat, string>
{
    public new FormatDisplayMap Add(DisplayFormat format, string display)
    {
        base.Add(format, display);
        return this;
    }
}