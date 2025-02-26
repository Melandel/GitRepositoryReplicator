namespace Mel.GitRepositoryReplicator.Domain;

public record RepositoryNamespace
{
	public override string ToString() => _ns;
	public static implicit operator string(RepositoryNamespace obj) => obj._ns;

	readonly string _ns;
	RepositoryNamespace(string ns)
	{
		_ns = ns switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<RepositoryNamespace>(nameof(_ns), ns),
			_ => ns
		};
	}

	public static RepositoryNamespace From(string ns)
	{
		try
		{
			return new(ns);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<RepositoryNamespace>(ns);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<RepositoryNamespace>(developerMistake, ns);
		}
	}
}
