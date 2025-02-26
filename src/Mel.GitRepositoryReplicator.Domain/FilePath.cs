namespace Mel.GitRepositoryReplicator.Domain;

public record FilePath
{
	public override string ToString() => _path;
	public static implicit operator string(FilePath obj) => obj._path;

	readonly string _path;
	readonly RepositoryRootNamespace _sourceRepositoryRootNamespace;
	FilePath(string path, RepositoryRootNamespace sourceRepositoryRootNamespace)
	{
		_path = path switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<FilePath>(nameof(_path), path),
			_ => path
		};
		_sourceRepositoryRootNamespace = sourceRepositoryRootNamespace;
	}

	public static FilePath From(
		string path,
		RepositoryRootNamespace sourceRepositoryRootNamespace)
	{
		try
		{
			return new(
				path,
				sourceRepositoryRootNamespace);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<FilePath>(path, sourceRepositoryRootNamespace);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<FilePath>(developerMistake, path, sourceRepositoryRootNamespace);
		}
	}

	public FilePath Duplicate()
	=> From(_path, _sourceRepositoryRootNamespace);

	public FilePath AdaptRootNamespaceTo(RepositoryRootNamespace targetRepositoryRootNamespace)
	=> new(
		_path.Replace(_sourceRepositoryRootNamespace, targetRepositoryRootNamespace),
		_sourceRepositoryRootNamespace);
}
