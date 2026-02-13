using System.Collections;

namespace ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;

public interface IDisplayFormatter<TEnum>
    where TEnum : struct, Enum
{
    string Format(TEnum member, DisplayFormat format);

    Result<TEnum> TryParse(string? str, StringComparison comparison = StringComparison.Ordinal);
}

internal sealed class MapDisplayFormatter<TEnum> : IDisplayFormatter<TEnum>
    where TEnum : struct, Enum
{
    private sealed record class Value(string Name, string Short, string Unicode, string Emoji);

    private readonly Dictionary<TEnum, Value> _map = [];

    public void Add(TEnum member, string name, string @short, string unicode, string emoji)
    {
        _map[member] = new Value(name, @short, unicode, emoji);
    }

    public Result<TEnum> TryParse(string? str, StringComparison comparison = StringComparison.Ordinal)
    {
        if (str is null)
            return Ex.ArgNull(str);
        foreach (var pair in _map)
        {
            var (member, value) = pair;
            if (value.Name.Equals(str, comparison) ||
                value.Short.Equals(str, comparison) ||
                value.Unicode.Equals(str, comparison) ||
                value.Emoji.Equals(str, comparison))
                return member;
        }
        return Ex.Parse<TEnum>(str, $"with StringComparison.{comparison}");
    }

    public string Format(TEnum member, DisplayFormat format)
    {
        if (_map.TryGetValue(member, out var value))
        {
            return format switch
            {
                DisplayFormat.Default => value.Name,
                DisplayFormat.Short => value.Short,
                DisplayFormat.Unicode => value.Unicode,
                DisplayFormat.Emoji => value.Emoji,
                _ => throw Ex.UndefinedEnum(format),
            };
        }

        return member.ToString();
    }
}

internal sealed class FuncDisplayFormatter<TEnum> : IDisplayFormatter<TEnum>
    where TEnum : struct, Enum
{
    private readonly Func<TEnum, DisplayFormat, string> _format;
    private readonly Func<string?, StringComparison, Result<TEnum>> _tryParse;

    public FuncDisplayFormatter(
        Func<TEnum, DisplayFormat, string> format, 
        Func<string?, StringComparison, Result<TEnum>> tryParse)
    {
        _format = format;
        _tryParse = tryParse;
    }

    public string Format(TEnum member, DisplayFormat format)
    {
        return _format(member, format);
    }

    public Result<TEnum> TryParse(string? str, StringComparison comparison = StringComparison.Ordinal)
    {
        return _tryParse(str, comparison);
    }
}

public static class DisplayFormatter
{
    private static readonly TypeMap<object?> _formatters = [];


    public static DisplayFormatRegistration<TEnum> Register<TEnum>()
        where TEnum : struct, Enum
    {
        object? obj = _formatters.GetOrAdd<TEnum>(static () => new DisplayFormatRegistration<TEnum>());
        if (obj is not DisplayFormatRegistration<TEnum> registration)
            throw Ex.Invalid();
        return registration;
    }

    public static Result<TEnum> TryParse<TEnum>(string? str, StringComparison comparison = StringComparison.Ordinal)
        where TEnum : struct, Enum
    {
        if (_formatters.TryGetValue<TEnum>(out object? obj))
        {
            if (obj is IDisplayFormatter<TEnum> formatter)
            {
                return formatter.TryParse(str, comparison);
            }

            throw Ex.Invalid();
        }

        return Ex.Parse<TEnum>(str, $"with StringComparison.{comparison}");
    }

    public static string ToString<TEnum>(this TEnum member, DisplayFormat format)
        where TEnum : struct, Enum
    {
        if (_formatters.TryGetValue<TEnum>(out object? obj))
        {
            if (obj is IDisplayFormatter<TEnum> formatter)
            {
                return formatter.Format(member, format);
            }

            throw Ex.Invalid();
        }

        return member.ToString();
    }


    public sealed class DisplayFormatRegistration<TEnum>
        where TEnum : struct, Enum
    {
        private IDisplayFormatter<TEnum>? _formatter = null;


        public Type EnumType { get; } = typeof(TEnum);

        internal DisplayFormatRegistration()
        {
        }

        public DisplayFormatRegistration<TEnum> FormatAs(
            TEnum member,
            string name,
            string @short,
            string unicode,
            string emoji)
        {
            MapDisplayFormatter<TEnum> mapper;

            if (_formatter is null)
            {
                _formatter = mapper = new MapDisplayFormatter<TEnum>();
            }

            if (!_formatter.Is(out mapper!))
            {
                throw Ex.Invalid();
            }

            mapper.Add(member, name, @short, unicode, emoji);
            return this;
        }

        public DisplayFormatRegistration<TEnum> Use(
            Func<TEnum, DisplayFormat, string> format,
            Func<string?, StringComparison, Result<TEnum>> tryParse)
        {
            _formatter = new FuncDisplayFormatter<TEnum>(format, tryParse);
            return this;
        }
        

        public string Format(TEnum member, DisplayFormat format)
        {
            if (_formatter is not null)
            {
                return _formatter.Format(member, format);
            }

            return member.ToString();
        }
    }
}


/*

[PublicAPI]
public sealed class MemberDisplayMapFormatter<TEnum> :
    IEnumerable<(DisplayFormat Format, string Rendering)>
    where TEnum : struct, Enum
{
    public required TEnum Member { get; init; }

    [NotNull, AllowNull]
    public string Default
    {
        get;
        set => field = value ?? string.Empty;
    } = string.Empty;

    [NotNull, AllowNull]
    public string Short
    {
        get;
        set => field = value ?? string.Empty;
    } = string.Empty;

    [NotNull, AllowNull]
    public string Unicode
    {
        get;
        set => field = value ?? string.Empty;
    } = string.Empty;

    [NotNull, AllowNull]
    public string Emoji
    {
        get;
        set => field = value ?? string.Empty;
    } = string.Empty;

    [NotNull, AllowNull]
    public string this[DisplayFormat format]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return format switch
            {
                DisplayFormat.Default => Default,
                DisplayFormat.Short => Short,
                DisplayFormat.Unicode => Unicode,
                DisplayFormat.Emoji => Emoji,
                _ => throw Ex.UndefinedEnum(format),
            };
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            switch (format)
            {
                case DisplayFormat.Default:
                {
                    Default = value;
                    return;
                }
                case DisplayFormat.Short:
                {
                    Short = value;
                    return;
                }
                case DisplayFormat.Unicode:
                {
                    Unicode = value;
                    return;
                }
                case DisplayFormat.Emoji:
                {
                    Emoji = value;
                    return;
                }
                default:
                    throw Ex.UndefinedEnum(format);
            }
        }
    }

    public MemberDisplayMapFormatter()
    {
    }

    [SetsRequiredMembers]
    public MemberDisplayMapFormatter(TEnum member)
    {
        this.Member = member;
        Default = member.ToString();
    }

    public void Add(DisplayFormat format, string? rendering)
        => this[format] = rendering;

    public void Add((DisplayFormat Format, string? Rendering) tuple)
        => this[tuple.Format] = tuple.Rendering;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<(DisplayFormat Format, string Rendering)> GetEnumerator()
    {
        yield return (DisplayFormat.Default, Default);
        yield return (DisplayFormat.Short, Short);
        yield return (DisplayFormat.Unicode, Unicode);
        yield return (DisplayFormat.Emoji, Emoji);
    }

    public override string ToString()
    {
        return $"{Default}  |  {Short}  | {Unicode}  |  {Emoji}";
    }
}

public interface IMemberDisplayFormatter<in TEnum>
    where TEnum : struct, Enum
{
    string Format(TEnum member, DisplayFormat format);
}

public sealed class MapMemberDisplayFormatter<TEnum> : IMemberDisplayFormatter<TEnum>
    where TEnum : struct, Enum
{
    private readonly
}

public sealed class FuncMemberDisplayFormatter<TEnum> : IMemberDisplayFormatter<TEnum>
    where TEnum : struct, Enum
{
    private readonly Func<TEnum, DisplayFormat, string> _func;

    public FuncMemberDisplayFormatter(Func<TEnum, DisplayFormat, string> func)
    {
        _func = func;
    }

    public string Format(TEnum member, DisplayFormat format)
    {
        return _func(member, format);
    }
}

[PublicAPI]
public sealed class DisplayFormatter<TEnum>
    where TEnum : struct, Enum
{
    public static DisplayFormatter<TEnum> Instance { get; } = new();


    private readonly Dictionary<TEnum, IMemberDisplayFormatter<TEnum>> _formatters = [];

    private DisplayFormatter()
    {
    }

    public DisplayFormatter<TEnum> Register(
        TEnum member,
        Func<MemberDisplayMapFormatter<TEnum>> register)
    {
        var registration = register();
        _formatters[member] = registration;
        return this;
    }

    public DisplayFormatter<TEnum> FormatAs(
        TEnum member,
        string? @default,
        string? @short,
        string? unicode,
        string? emoji)
    {
        MemberDisplayMapFormatter<TEnum> registration = new(member)
        {
            Default = @default,
            Short = @short,
            Unicode = unicode,
            Emoji = emoji,
        };
        _formatters[member] = registration;
        return this;
    }

    public Result<TEnum> TryParse(string? str, StringComparison comparison = StringComparison.Ordinal)
    {
        foreach (var thing in _formatters)
        {
            TEnum member = thing.Key;
            foreach (var thing2 in thing.Value)
            {
                if (string.Equals(thing2.Rendering, str, comparison))
                    return member;
            }
        }

        return Ex.Parse<TEnum>(str);
    }

    public string Format(TEnum member, DisplayFormat format)
    {
        if (_formatters.TryGetValue(member, out var registration))
        {
            return registration[format];
        }

        return member.ToString();
    }
}
*/