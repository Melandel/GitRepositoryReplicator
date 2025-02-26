namespace Mel.GitRepositoryReplicator.ConsoleApp.ArgumentsParsing;

record ConsoleAppArguments
{
	readonly bool _displayHelp;
	public GitRepositoryId SourceId { get; }
	public FolderPath TargetRepositoryPath { get; }
	public GitRepositoryName TargetRepositoryName { get; }
	public GitRepositoryRootNamespace TargetRepositoryRootNamespace { get; }
	public Language TargetRepositoryApiLanguage { get; }
	public Language TargetRepositoryMessagesLanguage { get; }
	public Language TargetRepositoryDocumentationLanguage { get; }
	public Language TargetRepositoryCommitMessagesLanguage { get; }

	ConsoleAppArguments(
		bool displayHelp,
		GitRepositoryId sourceId,
		FolderPath targetRepositoryPath,
		GitRepositoryName targetRepositoryName,
		GitRepositoryRootNamespace targetRepositoryRootNamespace,
		Language targetRepositoryApiLanguage,
		Language targetRepositoryMessagesLanguage,
		Language targetRepositoryDocumentationLanguage,
		Language targetRepositoryCommitMessagesLanguage)
	{
		_displayHelp = displayHelp;
		SourceId = sourceId;
		TargetRepositoryPath = targetRepositoryPath;
		TargetRepositoryName = targetRepositoryName;
		TargetRepositoryRootNamespace = targetRepositoryRootNamespace;
		TargetRepositoryApiLanguage = targetRepositoryApiLanguage;
		TargetRepositoryMessagesLanguage = targetRepositoryMessagesLanguage;
		TargetRepositoryDocumentationLanguage = targetRepositoryDocumentationLanguage;
		TargetRepositoryCommitMessagesLanguage = targetRepositoryCommitMessagesLanguage;
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
		default!,
		default!);

	public static ConsoleAppArguments From(string[] args, ILanguageResolver languageResolver)
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

			var parameters = ConsoleAppParameters.From(joined);
			return new(
				displayHelp: false,
				GitRepositoryId.From(parameters["sourceId"]),
				FolderPath.From(parameters["targetRepositoryPath"]),
				GitRepositoryName.From(parameters["targetRepositoryName"]),
				GitRepositoryRootNamespace.From(parameters["targetRepositoryRootNamespace"]),
				languageResolver.Resolve(parameters["targetRepositoryApiLanguage"]),
				languageResolver.Resolve(parameters["targetRepositoryMessagesLanguage"]),
				languageResolver.Resolve(parameters["targetRepositoryDocumentationLanguage"]),
				languageResolver.Resolve(parameters["targetRepositoryCommitMessagesLanguage"]));
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
