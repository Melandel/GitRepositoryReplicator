namespace Mel.GitRepositoryReplicator.Domain;

public record FileContent
{
	public override string ToString() => _content;
	public static implicit operator string(FileContent obj) => obj._content;

	readonly string _content;
	FileContent(string content)
	{
		_content = content switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<FileContent>(nameof(_content), content),
			_ => content
		};
	}

	public static FileContent From(string content)
	{
		try
		{
			return new(content);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<FileContent>(content);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<FileContent>(developerMistake, content);
		}
	}
}
