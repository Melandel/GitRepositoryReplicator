using Mel.GitRepositoryReplicator.Application.UseCases;

namespace Mel.GitRepositoryReplicator.ConsoleApp.Commands;

class ReplicateGitRepositoryCommand : Command
{
	readonly GitRepositoryId _sourceRepositoryId;
	readonly FolderPath _targetRepositoryPath;
	readonly GitRepositoryName _targetRepositoryName;
	readonly GitRepositoryNamespace _targetRepositoryNamespace;
	readonly Language _targetRepositoryApiLanguage;
	readonly Language _targetRepositoryMessagesLanguage;
	readonly Language _targetRepositoryDocumentationLanguage;
	readonly IReplicateCodeRepository _replicateGitRepository;

	public ReplicateGitRepositoryCommand(
		GitRepositoryId sourceRepositoryId,
		FolderPath targetRepositoryPath,
		GitRepositoryName targetRepositoryName,
		GitRepositoryNamespace targetRepositoryNamespace,
		Language targetRepositoryApiLanguage,
		Language targetRepositoryMessagesLanguage,
		Language targetRepositoryDocumentationLanguage,
		IReplicateCodeRepository replicateGitRepository)
	{
		_sourceRepositoryId = sourceRepositoryId;
		_targetRepositoryName = targetRepositoryName;
		_targetRepositoryNamespace = targetRepositoryNamespace;
		_targetRepositoryApiLanguage = targetRepositoryApiLanguage;
		_targetRepositoryMessagesLanguage = targetRepositoryMessagesLanguage;
		_targetRepositoryDocumentationLanguage = targetRepositoryDocumentationLanguage;
		_targetRepositoryPath = targetRepositoryPath;
		_replicateGitRepository = replicateGitRepository;
	}

	public override void Execute()
	{
		var input = new ReplicateCodeRepositoryInput(
			Application.CodeRepositoryId.From(_sourceRepositoryId),
			Application.FolderPath.From(_targetRepositoryPath),
			Application.RepositoryName.From(_targetRepositoryName),
			Application.RepositoryNamespace.From(_targetRepositoryNamespace),
			Application.Language.From(_targetRepositoryApiLanguage switch
			{
				var l when l == Language.French => Application.Language.French,
				var l when l == Language.English => Application.Language.English,
				_ => throw new NotImplementedException()
			}),
			Application.Language.From(_targetRepositoryApiLanguage switch
			{
				var l when l == Language.French => Application.Language.French,
				var l when l == Language.English => Application.Language.English,
				_ => throw new NotImplementedException()
			}),
			Application.Language.From(_targetRepositoryDocumentationLanguage switch
			{
				var l when l == Language.French => Application.Language.French,
				var l when l == Language.English => Application.Language.English,
				_ => throw new NotImplementedException()
			}));

		var output = _replicateGitRepository.Process(input);
	}
}
