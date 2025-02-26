namespace Mel.GitRepositoryReplicator.Domain;

public record FilePath
{
	public override string ToString() => _path;
	public static implicit operator string(FilePath obj) => obj._path;

	readonly string _path;
	FilePath(string path)
	{
		_path = path switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<FilePath>(nameof(_path), path),
			_ => path
		};
	}

	public static FilePath From(string path)
	{
		try
		{
			return new(path);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<FilePath>(path);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<FilePath>(developerMistake, path);
		}
	}
}
