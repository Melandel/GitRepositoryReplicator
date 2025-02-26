using Mel.GitRepositoryReplicator.Application.UseCases;

namespace Mel.GitRepositoryReplicator.ConsoleApp.Commands;

class ReplicateGitRepositoryCommand : Command
{
	readonly GitRepositoryId _sourceRepositoryId;
	readonly FolderPath _targetRepositoryPath;
	readonly GitRepositoryName _targetRepositoryName;
	readonly GitRepositoryRootNamespace _targetRepositoryRootNamespace;
	readonly Language _targetRepositoryApiLanguage;
	readonly Language _targetRepositoryMessagesLanguage;
	readonly Language _targetRepositoryDocumentationLanguage;
	readonly Language _targetRepositoryCommitMessagesLanguage;
	readonly IReplicateCodeRepository _replicateGitRepository;

	public ReplicateGitRepositoryCommand(
		GitRepositoryId sourceRepositoryId,
		FolderPath targetRepositoryPath,
		GitRepositoryName targetRepositoryName,
		GitRepositoryRootNamespace targetRepositoryRootNamespace,
		Language targetRepositoryApiLanguage,
		Language targetRepositoryMessagesLanguage,
		Language targetRepositoryDocumentationLanguage,
		Language targetRepositoryCommitMessagesLanguage,
		IReplicateCodeRepository replicateGitRepository)
	{
		_sourceRepositoryId = sourceRepositoryId;
		_targetRepositoryName = targetRepositoryName;
		_targetRepositoryRootNamespace = targetRepositoryRootNamespace;
		_targetRepositoryApiLanguage = targetRepositoryApiLanguage;
		_targetRepositoryMessagesLanguage = targetRepositoryMessagesLanguage;
		_targetRepositoryDocumentationLanguage = targetRepositoryDocumentationLanguage;
		_targetRepositoryPath = targetRepositoryPath;
		_targetRepositoryCommitMessagesLanguage = targetRepositoryCommitMessagesLanguage;
		_replicateGitRepository = replicateGitRepository;
	}

	public override void Execute()
	{
		var sourceCodeRepositoryId = Application.CodeRepositoryId.From(_sourceRepositoryId);
		var targetRepositoryPath = _targetRepositoryPath;
		var targetRepositoryName = Application.RepositoryName.From(_targetRepositoryName);
		var targetRepositoryRootNamespace = Application.RepositoryRootNamespace.From(_targetRepositoryRootNamespace);
		var targetRepositoryApiLanguage = _targetRepositoryApiLanguage;
		var targetRepositoryMessagesLanguage = _targetRepositoryMessagesLanguage;
		var targetRepositoryDocumentationLanguage = _targetRepositoryDocumentationLanguage;
		var targetRepositoryCommitMessagesLanguage = _targetRepositoryCommitMessagesLanguage;

		var input = new ReplicateCodeRepositoryInput(
			sourceCodeRepositoryId,
			targetRepositoryPath,
			targetRepositoryName,
			targetRepositoryRootNamespace,
			targetRepositoryApiLanguage,
			targetRepositoryApiLanguage,
			targetRepositoryDocumentationLanguage,
			targetRepositoryCommitMessagesLanguage);

		var output = _replicateGitRepository.Process(input);
	}
}
