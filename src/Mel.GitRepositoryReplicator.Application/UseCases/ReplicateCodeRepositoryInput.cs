namespace Mel.GitRepositoryReplicator.Application.UseCases;

public record ReplicateCodeRepositoryInput(
	CodeRepositoryId SourceRepositoryId,
	FolderPath TargetRepositoryPath,
	RepositoryName TargetRepositoryName,
	RepositoryNamespace TargetRepositoryNamespace,
	Language TargetRepositoryApiLanguage,
	Language TargetRepositoryMessagesLanguage,
	Language TargetRepositoryDocumentationLanguage);
