namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.ConstrainedTypes;

public record FolderPath
{
	public override string ToString() => _path.Replace("\\", "/");
	public static implicit operator string(FolderPath obj) => obj._path;

	readonly string _path;

	FolderPath(string path)
	{
		_path = path switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<FolderPath>(nameof(_path), path),
			_ => path
		};
	}

	public static readonly FolderPath LocalProjectCalledFoo = FolderPath.From(@$"{Environment.GetEnvironmentVariable("HOMEDRIVE")}{Environment.GetEnvironmentVariable("HOMEPATH")}\Desktop\projects\foo\");
	public static FolderPath From(string path)
	{
		try
		{
			return new(path);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<FolderPath>(path);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<FolderPath>(developerMistake, path);
		}
	}
}

