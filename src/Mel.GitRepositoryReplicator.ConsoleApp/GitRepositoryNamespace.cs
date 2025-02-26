namespace Mel.GitRepositoryReplicator.ConsoleApp;

record GitRepositoryRootNamespace
{
	public override string ToString() => _ns;
	public static implicit operator string(GitRepositoryRootNamespace obj) => obj._ns;

	readonly string _ns;

	GitRepositoryRootNamespace(string ns)
	{
		_ns = ns switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<GitRepositoryRootNamespace>(nameof(_ns), ns),
			_ => ns
		};
	}

	public static GitRepositoryRootNamespace SomeNamespace = new($"Test.{nameof(SomeNamespace)}");
	public static GitRepositoryRootNamespace From(string ns)
	{
		try
		{
			return new(ns);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<GitRepositoryRootNamespace>(ns);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<GitRepositoryRootNamespace>(developerMistake, ns);
		}
	}
}
