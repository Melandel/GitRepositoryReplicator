using Mel.GitRepositoryReplicator.Application;
using Mel.GitRepositoryReplicator.Domain;

namespace Mel.GitRepositoryReplicator.Infrastructure.Git;

interface IGitClient
{
	void InitRepository(Application.FolderPath targetRepositoryPath);
	void Commit(Application.FolderPath targetRepositoryPath, CodeBaseEvolutionStepDescription description);
	Application.FolderPath Clone(CodeRepositoryId sourceRepositoryId, string folderName);
	IReadOnlyCollection<CommitId> GetAllCommitIds(Application.FolderPath tmpRepositoryPath);
	void Checkout(Application.FolderPath tmpRepositoryPath, CommitId commitId);
	CodeBaseEvolutionStepDescription GetCommitMessage(Application.FolderPath tmpRepositoryPath, CommitId commitId);
}
