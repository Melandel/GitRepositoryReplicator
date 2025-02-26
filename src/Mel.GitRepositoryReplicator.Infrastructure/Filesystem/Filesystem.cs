namespace Mel.GitRepositoryReplicator.Infrastructure.Filesystem;

class Filesystem : IFilesystem
{
	public bool AlreadyHasDirectory(FolderPath targetRepositoryPath)
	=> Directory.Exists(targetRepositoryPath);

	public void DeleteFolder(FolderPath targetRepositoryPath)
	{
		if (!Directory.Exists(targetRepositoryPath))
		{
			return;
		}

		new DirectoryInfo(targetRepositoryPath)
			.AllowOperationsOnReadonlyAndHiddenElements()
			.Delete(recursive: true);
	}

	public void ClearFolderExceptTheDotGitFolder(FolderPath targetRepositoryPath)
	{
		Directory.GetDirectories(targetRepositoryPath)
			.Where(dirPath => !dirPath.EndsWith(".git"))
			.Select(FolderPath.From)
			.ToList()
			.ForEach(DeleteFolder);

		Directory.GetFiles(targetRepositoryPath)
			.Select(dirPath => new FileInfo(dirPath) { Attributes = FileAttributes.Normal })
			.ToList()
			.ForEach(file => file.Delete());
	}

	public void Write(Domain.CodeBase codeBase, FolderPath folder)
	{
		var writeOperations = new List<Task>();
		foreach ((var filename, var content) in codeBase)
		{
			writeOperations.Add(
				CreateFileWritingTask(folder, filename, content));
		}
		Task.WaitAll(writeOperations);
	}

	Task CreateFileWritingTask(FolderPath targetRepositoryPath, Domain.FilePath filename, Domain.FileContent content)
	=> WriteAsync(Path.Combine(targetRepositoryPath, filename), content);

	public async Task WriteAsync(string filepath, Domain.FileContent content)
	{
		var d = new FileInfo(filepath).Directory!;
		if (!d.Exists)
		{
			Directory.CreateDirectory(d.FullName);
		}
		using var outputFile = new StreamWriter(filepath);
		await outputFile.WriteAsync(content);
	}

	public Domain.CodeBase Read(FolderPath repositoryPath)
	{
		var files = new Dictionary<string, string>();
		ReadFolderExceptGitSubFolder(repositoryPath, files);

		return Domain.CodeBase.From(files);
	}

	void ReadFolderExceptGitSubFolder(FolderPath repositoryPath, Dictionary<string, string> files)
	=> ReadFolderExceptGitFolder(repositoryPath, files, repositoryParentFolder: repositoryPath);

	void ReadFolderExceptGitFolder(FolderPath repositoryPath, Dictionary<string, string> files, FolderPath repositoryParentFolder)
	{
		Directory.GetFiles(repositoryPath)
			.ToList()
			.ForEach(file => files.Add(
					key: Path.GetRelativePath(repositoryParentFolder, file),
					value: File.ReadAllText(file)));

		Directory.GetDirectories(repositoryPath)
			.Where(subdir => !subdir.EndsWith(".git"))
			.ToList()
			.ForEach(subdir => ReadFolderExceptGitFolder(FolderPath.From(subdir), files, repositoryParentFolder));
	}
}
