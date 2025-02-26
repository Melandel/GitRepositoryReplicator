using Mel.GitRepositoryReplicator.Domain;

namespace Mel.GitRepositoryReplicator.Application.ServiceProvidersInterfaces;

public interface ICodeRepositoryPersister
{
	void Overwrite(
		CodeBaseEvolutionOverTime codeBaseEvolutionOverTime,
		RepositoryName? targetRepositoryName,
		FolderPath targetRepositoryPath);
}
