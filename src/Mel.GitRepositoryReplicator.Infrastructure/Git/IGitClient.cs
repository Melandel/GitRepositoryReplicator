using Mel.GitRepositoryReplicator.Domain;

namespace Mel.GitRepositoryReplicator.Infrastructure.Git;

public interface IGitClient
{
	void Commit(Application.FolderPath targetRepositoryPath, CodeBaseEvolutionStepDescription description);
}
