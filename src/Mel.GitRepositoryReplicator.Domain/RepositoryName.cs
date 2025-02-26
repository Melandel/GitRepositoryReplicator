namespace Mel.GitRepositoryReplicator.Domain;

public record RepositoryName
{
	public override string ToString() => _name;
	public static implicit operator string(RepositoryName obj) => obj._name;

	readonly string _name;
	RepositoryName(string name)
	{
		_name = name switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<RepositoryName>(nameof(_name), name),
			_ => name
		};
	}

	public static RepositoryName From(string name)
	{
		try
		{
			return new(name);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<RepositoryName>(name);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<RepositoryName>(developerMistake, name);
		}
	}
}
