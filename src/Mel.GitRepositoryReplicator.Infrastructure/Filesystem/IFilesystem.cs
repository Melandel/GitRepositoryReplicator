namespace Mel.GitRepositoryReplicator.Infrastructure.Filesystem;

interface IFilesystem
{
	bool AlreadyHasDirectory(FolderPath targetRepositoryPath);
	void DeleteFolder(FolderPath targetRepositoryPath);
	void ClearFolderExceptTheDotGitFolder(FolderPath targetRepositoryPath);
	void Write(Domain.CodeBase codeBase, FolderPath folder);
	Task WriteAsync(string filepath, Domain.FileContent content);
	Domain.CodeBase Read(FolderPath tmpRepositoryPath);
}
