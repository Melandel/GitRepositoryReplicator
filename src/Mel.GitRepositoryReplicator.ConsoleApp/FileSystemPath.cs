namespace Mel.GitRepositoryReplicator.ConsoleApp;

record FileSystemPath
{
	public override string ToString() => _path;
	public static implicit operator string(FileSystemPath obj) => obj._path;

	readonly string _path;
	FileSystemPath(string path)
	{
		_path = path switch
		{
			"" => throw ObjectConstructionException.WhenConstructingAMemberFor<FileSystemPath>(nameof(_path), path),
			_ => path
		};
	}

	public static FileSystemPath From(string path)
	{
		try
		{
			return new(path);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<FileSystemPath>(path);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<FileSystemPath>(developerMistake, path);
		}
	}

	public static readonly FileSystemPath LocalProjectCalledFoo = FileSystemPath.From(@"C:\Users\tranm\Desktop\projects\foo\");
}
