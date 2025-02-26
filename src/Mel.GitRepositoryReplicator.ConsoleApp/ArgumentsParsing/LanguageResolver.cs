namespace Mel.GitRepositoryReplicator.ConsoleApp.ArgumentsParsing;

class LanguageResolver : ILanguageResolver
{
	public Language Resolve(string str)
	=> str switch
	{
		var _ when string.Equals(str, "en",      StringComparison.InvariantCultureIgnoreCase) => Language.English,
		var _ when string.Equals(str, "english", StringComparison.InvariantCultureIgnoreCase) => Language.English,
		var _ when string.Equals(str, "fr",     StringComparison.InvariantCultureIgnoreCase) => Language.French,
		var _ when string.Equals(str, "french", StringComparison.InvariantCultureIgnoreCase) => Language.French,
		_ => Language.English
	};
}
