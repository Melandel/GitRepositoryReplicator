using Mel.GitRepositoryReplicator.Application;

namespace Mel.GitRepositoryReplicator.Infrastructure.Filesystem;

class Filesystem : IFilesystem
{
	public void OverrideFolder(FolderPath targetRepositoryPath)
	{
		var dir = new DirectoryInfo(targetRepositoryPath);
		if (dir.Exists)
		{
			dir.Delete(recursive: true);
		}

		Directory.CreateDirectory(targetRepositoryPath);
	}

	public Task WriteAsync(string filepath, Domain.FileContent content)
	{
		var d = new FileInfo(filepath).Directory!;
		if (!d.Exists)
		{
			Directory.CreateDirectory(d.FullName);
		}
		using var outputFile = new StreamWriter(filepath);
		return outputFile.WriteAsync(content);
	}
}
