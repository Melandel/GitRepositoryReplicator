using Mel.GitRepositoryReplicator.Domain;

namespace Mel.GitRepositoryReplicator.Application.ServiceProvidersInterfaces;

public interface ICodeRepositoryProvider
{
	CodeBaseEvolutionOverTime GetCodeBaseEvolutionOverTime(CodeRepositoryId sourceRepositoryId);
}
