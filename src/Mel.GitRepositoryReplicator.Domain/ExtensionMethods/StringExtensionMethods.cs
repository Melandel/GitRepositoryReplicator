using System.Text.RegularExpressions;

namespace Mel.GitRepositoryReplicator.Domain.ExtensionMethods;

static class StringExtensionMethods
{
	public static string ToKebabCase(this string str)
	=> new KebabCaseConverter().Convert(str)!;

	public static string ToSnakeCase(this string str)
	=> new SnakeCaseConverter().Convert(str)!;

	public static string ToSpaceSeparatedWords(this string str)
	=> new SpaceSeparatedWordsConverter().Convert(str)!;

	public static string ReplaceCaseInsensitive(
		this string input,
		string search,
		string replacement)
	=> Regex.Replace(
		input,
		Regex.Escape(search),
		match => match.Value switch
		{
		[] => "",
		[ var singleCharacter ] => $"{singleCharacter}",
			var captured when char.IsUpper(captured[0]) => $"{char.ToUpper(replacement[0])}{replacement[1..]}".Replace("$", "$$"),
			var captured when char.IsLower(captured[0]) => $"{char.ToLower(replacement[0])}{replacement[1..]}".Replace("$", "$$"),
			_ => replacement.Replace("$", "$$")
		},
		RegexOptions.IgnoreCase);
}
