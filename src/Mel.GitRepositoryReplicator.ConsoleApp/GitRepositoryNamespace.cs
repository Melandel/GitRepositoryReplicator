namespace Mel.GitRepositoryReplicator.ConsoleApp;

record GitRepositoryNamespace
{
	public override string ToString() => _ns;
	public static implicit operator string(GitRepositoryNamespace obj) => obj._ns;

	readonly string _ns;

	GitRepositoryNamespace(string ns)
	{
		_ns = ns switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<GitRepositoryNamespace>(nameof(_ns), ns),
			_ => ns
		};
	}

	public static GitRepositoryNamespace SomeNamespace = new(nameof(SomeNamespace));
	public static GitRepositoryNamespace From(string ns)
	{
		try
		{
			return new(ns);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<GitRepositoryNamespace>(ns);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<GitRepositoryNamespace>(developerMistake, ns);
		}
	}
}
