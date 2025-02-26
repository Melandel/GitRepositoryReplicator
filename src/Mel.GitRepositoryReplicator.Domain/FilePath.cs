using System.Text.RegularExpressions;

namespace Mel.GitRepositoryReplicator.Domain;

public record FilePath
{
	public override string ToString() => _path;
	public static implicit operator string(FilePath obj) => obj._path;

	readonly string _path;
	readonly RepositoryRootNamespace _sourceRepositoryRootNamespace;
	FilePath(string path, RepositoryRootNamespace sourceRepositoryRootNamespace)
	{
		_path = path switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<FilePath>(nameof(_path), path),
			_ => path
		};
		_sourceRepositoryRootNamespace = sourceRepositoryRootNamespace;
	}

	public static FilePath From(
		string path,
		RepositoryRootNamespace sourceRepositoryRootNamespace)
	{
		try
		{
			return new(
				path,
				sourceRepositoryRootNamespace);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<FilePath>(path, sourceRepositoryRootNamespace);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<FilePath>(developerMistake, path, sourceRepositoryRootNamespace);
		}
	}

	public bool HasMarkdownExtension => _path.EndsWith(".md");
	public bool ContainsLanguageIndicator => Language.All.Any(lang => _path.Contains($"-{lang.Code}-"));

	public FilePath Duplicate()
	=> From(_path, _sourceRepositoryRootNamespace);

	public FilePath AdaptRootNamespaceTo(RepositoryRootNamespace targetRepositoryRootNamespace)
	=> new(
		_path.Replace(_sourceRepositoryRootNamespace, targetRepositoryRootNamespace),
		_sourceRepositoryRootNamespace);

	public FilePath AdaptDocumentationLanguageTo(Language targetRepositoryDocumentationLanguage)
	{
		var adaptedPath = _path;

		if (targetRepositoryDocumentationLanguage == Language.French)
		{
			adaptedPath = adaptedPath.Replace("-fr-", "-");
			adaptedPath = Regex.Replace(adaptedPath, "-fr.md$", ".md");
		}

		return new(adaptedPath, _sourceRepositoryRootNamespace);
	}

	public bool IsWrittenIn(Language targetRepositoryDocumentationLanguage)
	=> targetRepositoryDocumentationLanguage switch
	{
		var en when en == Language.English => !Language.All.Any(lang => _path.Contains($"-{lang.Code}-") || _path.Contains($"-{lang.Code}.")),
		var fr when fr == Language.French => _path.Contains($"-{Language.French.Code}-") || _path.Contains($"-{Language.French.Code}."),
		_ => throw new NotSupportedException()
	};
}
