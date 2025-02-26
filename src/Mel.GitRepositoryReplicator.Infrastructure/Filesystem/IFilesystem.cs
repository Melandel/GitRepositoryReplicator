using Mel.GitRepositoryReplicator.Application;

namespace Mel.GitRepositoryReplicator.Infrastructure.Filesystem;

public interface IFilesystem
{
	void OverrideFolder(FolderPath targetRepositoryPath);
	Task WriteAsync(string filepath, Domain.FileContent content);
}
