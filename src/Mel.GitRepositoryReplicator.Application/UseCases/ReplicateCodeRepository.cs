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
			Domain.RepositoryName.From(input.TargetRepositoryName),
			Domain.RepositoryNamespace.From(input.TargetRepositoryNamespace),
			Domain.Language.From(input.TargetRepositoryApiLanguage),
			Domain.Language.From(input.TargetRepositoryMessagesLanguage),
			Domain.Language.From(input.TargetRepositoryDocumentationLanguage));

		_codeRepositoryPersister.Overwrite(
			codeBaseEvolutionOverTime,
			input.TargetRepositoryName,
			input.TargetRepositoryPath);

		return new ReplicateCodeRepositoryOutput();
	}
}
