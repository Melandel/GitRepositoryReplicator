namespace Mel.GitRepositoryReplicator.Application.UseCases;

public interface IReplicateCodeRepository
{
	ReplicateCodeRepositoryOutput Process(ReplicateCodeRepositoryInput input);
}
