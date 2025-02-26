namespace Mel.GitRepositoryReplicator.Application;

public record CodeRepositoryId
{
	public override string ToString() => _url;
	public static implicit operator string(CodeRepositoryId obj) => obj._url;

	readonly string _url;
	CodeRepositoryId(string url)
	{
		_url = url switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<CodeRepositoryId>(nameof(_url), url),
			_ => url
		};
	}

	public static CodeRepositoryId From(string url)
	{
		try
		{
			return new(url);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<CodeRepositoryId>(url);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<CodeRepositoryId>(developerMistake, url);
		}
	}
}
