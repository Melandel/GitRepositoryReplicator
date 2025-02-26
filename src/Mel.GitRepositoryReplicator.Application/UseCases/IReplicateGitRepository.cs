namespace Mel.GitRepositoryReplicator.Application.UseCases;

public interface IReplicateGitRepository
{
	ReplicateGitRepositoryOutput Process(ReplicateGitRepositoryInput input);
}
