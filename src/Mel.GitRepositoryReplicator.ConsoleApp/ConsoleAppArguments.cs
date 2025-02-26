namespace Mel.GitRepositoryReplicator.ConsoleApp;

record ConsoleAppArguments
{
	readonly bool _displayHelp;
	public GitRepositoryId SourceId { get; }
	public FolderPath TargetRepositoryPath { get; }
	public GitRepositoryName TargetRepositoryName { get; }
	public GitRepositoryNamespace TargetRepositoryNamespace { get; }
	public Language TargetRepositoryApiLanguage { get; }
	public Language TargetRepositoryMessagesLanguage { get; }
	public Language TargetRepositoryDocumentationLanguage { get; }

	ConsoleAppArguments(
		bool displayHelp,
		GitRepositoryId sourceId,
		FolderPath targetRepositoryPath,
		GitRepositoryName targetRepositoryName,
		GitRepositoryNamespace targetRepositoryNamespace,
		Language targetRepositoryApiLanguage,
		Language targetRepositoryMessagesLanguage,
		Language targetRepositoryDocumentationLanguage)
	{
		_displayHelp = displayHelp;
		SourceId = sourceId;
		TargetRepositoryPath = targetRepositoryPath;
		TargetRepositoryName = targetRepositoryName;
		TargetRepositoryNamespace = targetRepositoryNamespace;
		TargetRepositoryApiLanguage = targetRepositoryApiLanguage;
		TargetRepositoryMessagesLanguage = targetRepositoryMessagesLanguage;
		TargetRepositoryDocumentationLanguage = targetRepositoryDocumentationLanguage;
	}

	public bool TriggersHelpDisplay
	=> _displayHelp;

	public static readonly ConsoleAppArguments DisplayHelp
	= new(
		displayHelp: true,
		default!,
		default!,
		default!,
		default!,
		default!,
		default!,
		default!);

	public static ConsoleAppArguments From(string[] args)
	{
		try
		{
			var joined = string.Join(" ", args).Trim();

			if (joined == "")
			{
				return ConsoleAppArguments.DisplayHelp;
			}

			if (joined.Contains("--help") || joined.EndsWith(" -h"))
			{
				return ConsoleAppArguments.DisplayHelp;
			}

			if (joined.Contains("--version") || joined.EndsWith(" -v"))
			{
				return ConsoleAppArguments.DisplayHelp;
			}

			var sourceId = GitRepositoryId.MelandelDotnetWebService;
			var targetRepositoryPath = FolderPath.LocalProjectCalledFoo;
			var targetRepositoryName = GitRepositoryName.ReplicatedRepository;
			var targetRepositoryNamespace = GitRepositoryNamespace.SomeNamespace;
			var targetRepositoryApiLanguage = Language.From("en");
			var targetRepositoryMessagesLanguage = Language.From("en");
			var targetRepositoryDocumentationLanguage = Language.From("en");

			return new(
				displayHelp: false,
				sourceId,
				targetRepositoryPath,
				targetRepositoryName,
				targetRepositoryNamespace,
				targetRepositoryApiLanguage,
				targetRepositoryMessagesLanguage,
				targetRepositoryDocumentationLanguage);
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
