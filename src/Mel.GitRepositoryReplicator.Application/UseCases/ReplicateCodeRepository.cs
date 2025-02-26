using Mel.GitRepositoryReplicator.Application.ServiceProvidersInterfaces;

namespace Mel.GitRepositoryReplicator.Application.UseCases;

class ReplicateCodeRepository : IReplicateCodeRepository
{
	readonly ICodeRepositoryProvider _codeRepositoryProvider;
	readonly ICodeRepositoryPersister _codeRepositoryPersister;
	public ReplicateCodeRepository(
		ICodeRepositoryProvider codeRepositoryProvider,
		ICodeRepositoryPersister codeRepositoryPersister)
	{
		_codeRepositoryProvider = codeRepositoryProvider;
		_codeRepositoryPersister = codeRepositoryPersister;
	}

	public ReplicateCodeRepositoryOutput Process(ReplicateCodeRepositoryInput input)
	{
		var codeBaseEvolutionOverTime = _codeRepositoryProvider.GetCodeBaseEvolutionOverTime(input.SourceRepositoryId);

		codeBaseEvolutionOverTime.AdaptTo(
			input.TargetRepositoryName.ToDomainRepositoryName(),
			Domain.RepositoryRootNamespace.From(input.TargetRepositoryRootNamespace),
			input.TargetRepositoryApiLanguage,
			input.TargetRepositoryMessagesLanguage,
			input.TargetRepositoryDocumentationLanguage,
			input.TargetRepositoryCommitMessagesLanguage);

		_codeRepositoryPersister.Overwrite(
			codeBaseEvolutionOverTime,
			input.TargetRepositoryName,
			input.TargetRepositoryPath);

		return new ReplicateCodeRepositoryOutput();
	}
}
