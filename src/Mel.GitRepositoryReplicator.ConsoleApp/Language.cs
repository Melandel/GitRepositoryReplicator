using Mel.GitRepositoryReplicator.CrossCuttingConcerns.EnumTypes;

namespace Mel.GitRepositoryReplicator.ConsoleApp;

public class Language : Enumeration
{
	public override string ToString() => Name;
	public static implicit operator string(Language obj) => obj.Name;

	Language(string name) : base(name, "")
	{
	}

	public static readonly Language English = new("fr");
	public static readonly Language French = new("en");
	public static Language From(string language)
	=> Enumeration.FromString<Language>(language);

	public static IReadOnlyCollection<Language> All
	=> Enumeration.GetAll<Language>();
}
