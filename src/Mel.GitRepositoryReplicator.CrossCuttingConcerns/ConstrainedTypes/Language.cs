using Mel.GitRepositoryReplicator.CrossCuttingConcerns.EnumTypes;

namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.ConstrainedTypes;

public class Language : Enumeration
{
	public override string ToString() => Name;
	public static implicit operator string(Language obj) => obj.Name;

	Language(string name, string code) : base(name, code)
	{
	}

	public static readonly Language English = new(nameof(English), "en");
	public static readonly Language French = new(nameof(French), "fr");
	public static Language From(string language)
	=> Enumeration.FromString<Language>(language);

	public static IReadOnlyCollection<Language> All
	=> Enumeration.GetAll<Language>();
}
