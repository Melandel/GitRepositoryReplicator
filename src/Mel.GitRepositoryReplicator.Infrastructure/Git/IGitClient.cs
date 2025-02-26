using Mel.GitRepositoryReplicator.Application;
using Mel.GitRepositoryReplicator.Domain;

namespace Mel.GitRepositoryReplicator.Infrastructure.Git;

interface IGitClient
{
	void InitRepository(FolderPath targetRepositoryPath);
	void Commit(FolderPath targetRepositoryPath, CodeBaseEvolutionStepDescription description);
	FolderPath Clone(CodeRepositoryId sourceRepositoryId, string folderName);
	IReadOnlyCollection<CommitId> GetAllCommitIds(FolderPath tmpRepositoryPath);
	void Checkout(FolderPath tmpRepositoryPath, CommitId commitId);
	CodeBaseEvolutionStepDescription GetCommitMessage(FolderPath tmpRepositoryPath, CommitId commitId);
}
