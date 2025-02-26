namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.Logging;

public struct LogFriendlyTimeSpan
: IComparable,
	IComparable<TimeSpan>,
	IEquatable<TimeSpan>,
	IComparable<LogFriendlyTimeSpan>,
	IEquatable<LogFriendlyTimeSpan>,
	IFormattable
{
	readonly TimeSpan _encapsulated;
	LogFriendlyTimeSpan(TimeSpan timespan)
	{
		_encapsulated = timespan;
	}

	public static implicit operator TimeSpan(LogFriendlyTimeSpan obj) => obj._encapsulated;
	public static implicit operator LogFriendlyTimeSpan(TimeSpan timeSpan) => new LogFriendlyTimeSpan(timeSpan);

	public int CompareTo(object? obj) => ((IComparable)_encapsulated).CompareTo(obj);
	public int CompareTo(TimeSpan other) => ((IComparable<TimeSpan>)_encapsulated).CompareTo(other);
	public bool Equals(TimeSpan other) => ((IEquatable<TimeSpan>)_encapsulated).Equals(other);
	public int CompareTo(LogFriendlyTimeSpan other) => _encapsulated.CompareTo(other._encapsulated);
	public bool Equals(LogFriendlyTimeSpan other) => _encapsulated.Equals(other._encapsulated);

	public string ToString(string? format, IFormatProvider? formatProvider)
	{
		if (formatProvider == null)
		{
			return Render(_encapsulated);
		}

		return ((IFormattable)_encapsulated).ToString(format, formatProvider);
	}
	public override string ToString() => Render(_encapsulated);
	public static string Render(TimeSpan ts)
	{
		switch (ts)
		{
			case var _ when ts.Hours   > 0: return ts.ToString(@"h\hmm\mss\s");
			case var _ when ts.Minutes > 0: return ts.ToString(@"m\mss\s");
			case var _ when ts.Seconds > 0: return ts.ToString(@"s\.ff\s");
			default: return $"{ts.Milliseconds}ms";
		};
	}
}
