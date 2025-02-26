namespace Mel.GitRepositoryReplicator.Infrastructure.ExtensionMethods;

static class DirectoryInfoExtensionMethods
{
	public static DirectoryInfo AllowOperationsOnReadonlyAndHiddenElements(this DirectoryInfo directory)
	{
		directory.Attributes = FileAttributes.Normal;
		foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
		{
			info.Attributes = FileAttributes.Normal;
		}

		return directory;
	}

}
