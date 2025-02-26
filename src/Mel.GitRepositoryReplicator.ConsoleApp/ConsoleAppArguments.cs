namespace Mel.GitRepositoryReplicator.ConsoleApp;

record ConsoleAppArguments
{
	public bool FailedToParse { get; init; }

	public override string ToString() => $"{SourceId}";
	public static implicit operator GitRepositoryId(ConsoleAppArguments obj) => obj.SourceId;

	public GitRepositoryId SourceId { get; init; }
	public FileSystemPath TargetFolderPath { get; init; }

	ConsoleAppArguments(
		bool failedToParse,
		GitRepositoryId sourceId,
		FileSystemPath targetFolderPath)
	{
		FailedToParse = failedToParse;
		SourceId = sourceId;
		TargetFolderPath = targetFolderPath;
	}

	public static ConsoleAppArguments From(string[] args)
	{
		try
		{
			var failedToParse = false;

			var sourceId = GitRepositoryId.MelandelDotnetWebService;
			var targetFolderPath = FileSystemPath.LocalProjectCalledFoo;

			return new(failedToParse, sourceId, targetFolderPath);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<ConsoleAppArguments>(args);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<ConsoleAppArguments>(developerMistake, args);
		}
	}
}
