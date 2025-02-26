using Mel.GitRepositoryReplicator.Application.ServiceProvidersInterfaces;
using Mel.GitRepositoryReplicator.Domain;
using Mel.GitRepositoryReplicator.Infrastructure.Filesystem;
using Mel.GitRepositoryReplicator.Infrastructure.Git;

namespace Mel.GitRepositoryReplicator.Infrastructure;

class CodeRepositoryPersister : ICodeRepositoryPersister
{
	readonly IGitClient _gitClient;
	readonly IFilesystem _filesystem;
	public CodeRepositoryPersister(IGitClient gitClient, IFilesystem filesystem)
	{
		_gitClient = gitClient;
		_filesystem = filesystem;
	}

	public void Overwrite(
		CodeBaseEvolutionOverTime codeBaseEvolutionOverTime,
		Application.RepositoryName targetRepositoryName,
		Application.FolderPath targetRepositoryPath)
	{
		_filesystem.OverrideFolder(targetRepositoryPath);

		foreach (var step in codeBaseEvolutionOverTime)
		{
			var writeOperations = new List<Task>();
			var codebase = step.CodeBase;
			foreach ((var filename, var content) in codebase)
			{
				var filepath = Path.Combine(targetRepositoryPath, filename);

				var writeOperation = _filesystem.WriteAsync(filepath, content);
				writeOperations.Add(writeOperation);
			}

			Task.WaitAll(writeOperations);
			_gitClient.Commit(targetRepositoryPath, step.Description);
		}
	}
}
