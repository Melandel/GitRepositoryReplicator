namespace Mel.GitRepositoryReplicator.Application;

public record RepositoryRootNamespace
{
	public override string ToString() => _ns;
	public static implicit operator string(RepositoryRootNamespace obj) => obj._ns;

	readonly string _ns;
	RepositoryRootNamespace(string ns)
	{
		_ns = ns switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<RepositoryRootNamespace>(nameof(_ns), ns),
			_ => ns
		};
	}

	public static RepositoryRootNamespace From(string ns)
	{
		try
		{
			return new(ns);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<RepositoryRootNamespace>(ns);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<RepositoryRootNamespace>(developerMistake, ns);
		}
	}
}
