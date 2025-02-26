using Mel.GitRepositoryReplicator.Domain;

namespace Mel.GitRepositoryReplicator.Infrastructure.Git;

class GitClient : IGitClient
{
	public void Commit(Application.FolderPath targetRepositoryPath, CodeBaseEvolutionStepDescription description)
	{
	}
}
