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
		Application.RepositoryName? targetRepositoryName,
		FolderPath targetRepositoryPath)
	{
			Console.WriteLine($"<TARGET FOLDER> {targetRepositoryPath}");
		_filesystem.DeleteFolder(targetRepositoryPath);
		_gitClient.InitRepository(targetRepositoryPath);

		foreach (var (n, step) in codeBaseEvolutionOverTime.Index())
		{
			ReplaceCodeBase(targetRepositoryPath, step.CodeBase);
			Commit(targetRepositoryPath, step.Description);
			Console.WriteLine($"pushed adapted commit {$"#{n+1}",3}: {step.Description.Truncate(160)}");
		}
	}

	void ReplaceCodeBase(FolderPath targetRepositoryPath, CodeBase codeBase)
	{
		_filesystem.ClearFolderExceptTheDotGitFolder(targetRepositoryPath);
		_filesystem.Write(codeBase, targetRepositoryPath);
	}

	void Commit(FolderPath targetRepositoryPath, CodeBaseEvolutionStepDescription stepDescription)
	{
		_gitClient.Commit(targetRepositoryPath, stepDescription);
	}
}
