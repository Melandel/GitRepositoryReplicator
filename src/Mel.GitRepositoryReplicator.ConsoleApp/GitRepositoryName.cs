namespace Mel.GitRepositoryReplicator.ConsoleApp;

record GitRepositoryName
{
	public override string ToString() => _name;
	public static implicit operator string(GitRepositoryName obj) => obj._name;

	readonly string _name;


	GitRepositoryName(string name)
	{
		_name = name switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<GitRepositoryName>(nameof(_name), name),
			_ => name
		};
	}

	public static readonly GitRepositoryName ReplicatedRepository = new(nameof(ReplicatedRepository));
	public static GitRepositoryName From(string name)
	{
		try
		{
			return new(name);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<GitRepositoryName>(name);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<GitRepositoryName>(developerMistake, name);
		}
	}
}
