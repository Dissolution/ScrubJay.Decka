using System.Collections;
using ScrubJay.Decka.Sandbox.Iterations.SB10.Formatting;


namespace ScrubJay.Decka.Sandbox.Iterations.SB10;

[PublicAPI]
public sealed class DisplayFormatRegistration<TEnum>
    where TEnum : struct, Enum
{
    public static DisplayFormatRegistration<TEnum> Instance { get; } = new();


    [PublicAPI]
    public sealed class DisplayFormatMemberRegistration : IEnumerable<(DisplayFormat Format, string Rendering)>
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

        public DisplayFormatMemberRegistration()
        {
        }

        [SetsRequiredMembers]
        public DisplayFormatMemberRegistration(TEnum member)
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


    private readonly Dictionary<TEnum, DisplayFormatMemberRegistration> _registrations = [];

    private DisplayFormatRegistration()
    {
    }

    public DisplayFormatRegistration<TEnum> Register(
        TEnum member,
        Func<DisplayFormatMemberRegistration> register)
    {
        var registration = register();
        _registrations[member] = registration;
        return this;
    }

    public DisplayFormatRegistration<TEnum> Register(
        TEnum member,
        string? @default,
        string? @short,
        string? unicode,
        string? emoji)
    {
        DisplayFormatMemberRegistration registration = new(member)
        {
            Default = @default,
            Short = @short,
            Unicode = unicode,
            Emoji = emoji,
        };
        _registrations[member] = registration;
        return this;
    }

    public Result<TEnum> TryParse(string? str, StringComparison comparison = StringComparison.Ordinal)
    {
        foreach (var thing in _registrations)
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
        if (_registrations.TryGetValue(member, out var registration))
        {
            return registration[format];
        }

        return member.ToString();
    }
}