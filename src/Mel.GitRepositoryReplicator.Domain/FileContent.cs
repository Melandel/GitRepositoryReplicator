using Mel.GitRepositoryReplicator.Domain.ExtensionMethods;

namespace Mel.GitRepositoryReplicator.Domain;

public record FileContent
{
	public override string ToString() => _content;
	public static implicit operator string(FileContent obj) => obj._content;

	readonly string _content;
	readonly RepositoryName _sourceRepositoryName;
	readonly RepositoryRootNamespace _sourceRepositoryRootNamespace;
	const string SourceRepositoryDateOfWriting = "2023-04-07";
	FileContent(string content, RepositoryName sourceRepositoryName, RepositoryRootNamespace sourceRepositoryRootNamespace)
	{
		_content = content switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<FileContent>(nameof(_content), content),
			_ => content
		};
		_sourceRepositoryName = sourceRepositoryName;
		_sourceRepositoryRootNamespace = sourceRepositoryRootNamespace;
	}

	public static FileContent From(
		string content,
		RepositoryName sourceRepositoryName,
		RepositoryRootNamespace rootNamespace)
	{
		try
		{
			return new(
				content,
				sourceRepositoryName,
				rootNamespace);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<FileContent>(content, sourceRepositoryName, rootNamespace);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<FileContent>(developerMistake, content, sourceRepositoryName, rootNamespace);
		}
	}

	public FileContent AdaptRepositoryNameTo(RepositoryName? targetRepositoryName)
	=> targetRepositoryName switch
	{
		null => this,
		_ => new(
			_content
				.ReplaceCaseInsensitive(_sourceRepositoryName, targetRepositoryName)
				.ReplaceCaseInsensitive(((string)_sourceRepositoryName).ToKebabCase(), ((string)targetRepositoryName).ToKebabCase())
				.ReplaceCaseInsensitive(((string)_sourceRepositoryName).ToSnakeCase(), ((string)targetRepositoryName).ToSnakeCase())
				.ReplaceCaseInsensitive(((string)_sourceRepositoryName).ToSpaceSeparatedWords(), ((string)targetRepositoryName).ToSpaceSeparatedWords()),
			_sourceRepositoryName,
			_sourceRepositoryRootNamespace)
	};

	public FileContent AdaptRootNamespaceTo(RepositoryRootNamespace targetRepositoryRootNamespace)
	=> new(
		_content.ReplaceCaseInsensitive(_sourceRepositoryRootNamespace, targetRepositoryRootNamespace),
		_sourceRepositoryName,
		_sourceRepositoryRootNamespace);

	public FileContent AdaptApiLanguageTo(Language targetRepositoryApiLanguage)
	{
		return this;
	}

	public FileContent AdaptMessagesLanguageTo(Language targetRepositoryMessagesLanguage)
	{
		return this;
	}

	public FileContent AdaptDateOfWritingToToday()
	=> new(
		_content.Replace(SourceRepositoryDateOfWriting, DateTime.Now.ToString("yyyy-MM-dd")),
		_sourceRepositoryName,
		_sourceRepositoryRootNamespace);
}
