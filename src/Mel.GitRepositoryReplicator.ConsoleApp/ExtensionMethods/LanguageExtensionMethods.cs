namespace Mel.GitRepositoryReplicator.ConsoleApp.ExtensionMethods;

static class LanguageExtensionMethods
{
	public static Application.Language ToApplicationLanguage(this Language language)
	=> language switch
	{
		var l when l == Language.French => Application.Language.French,
		var l when l == Language.English => Application.Language.English,
		_ => throw new NotImplementedException()
	};
}
