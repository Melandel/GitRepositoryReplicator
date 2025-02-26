using Mel.GitRepositoryReplicator.CrossCuttingConcerns.EnumTypes;

namespace Mel.GitRepositoryReplicator.Domain;

public class Language : Enumeration
{
	public override string ToString() => Name;
	public static implicit operator string(Language obj) => obj.Name;

	Language(string name) : base(name, "")
	{
	}

	public static readonly Language English = new(nameof(English));
	public static readonly Language French = new(nameof(French));
	public static Language From(string language)
	=> Enumeration.FromString<Language>(language);

	public static IReadOnlyCollection<Language> All
	=> Enumeration.GetAll<Language>();
}
