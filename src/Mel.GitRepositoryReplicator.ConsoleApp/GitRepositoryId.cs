namespace Mel.GitRepositoryReplicator.ConsoleApp;

record GitRepositoryId
{
	public override string ToString() => _gitUrl;
	public static implicit operator string(GitRepositoryId obj) => obj._gitUrl;

	readonly string _gitUrl;
	GitRepositoryId(string gitUrl)
	{
		_gitUrl = gitUrl switch
		{
			"" => throw ObjectConstructionException.WhenConstructingAMemberFor<GitRepositoryId>(nameof(_gitUrl), gitUrl),
			_ => gitUrl
		};
	}

	public static GitRepositoryId From(string gitUrl)
	{
		try
		{
			return new(gitUrl);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<GitRepositoryId>(gitUrl);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<GitRepositoryId>(developerMistake, gitUrl);
		}
	}

	public static readonly GitRepositoryId MelandelDotnetWebService = new("https://github.com/Melandel/dotnet-web-service.git");
}
