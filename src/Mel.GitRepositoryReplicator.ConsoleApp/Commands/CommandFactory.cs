using Mel.GitRepositoryReplicator.Application.UseCases;
using Mel.GitRepositoryReplicator.ConsoleApp.ArgumentsParsing;
using Microsoft.Extensions.DependencyInjection;

namespace Mel.GitRepositoryReplicator.ConsoleApp.Commands;

class CommandFactory : ICommandFactory
{
	readonly IServiceProvider _serviceProvider;
	public CommandFactory(IServiceProvider serviceProvider)
	=> _serviceProvider = serviceProvider;

	public Command CreateCommandFrom(ConsoleAppArguments consoleAppArguments)
	=> consoleAppArguments switch
	{
		var args when args == ConsoleAppArguments.DisplayHelp => new DisplayHelpCommand(consoleAppArguments),
		_ => new ReplicateGitRepositoryCommand(
			consoleAppArguments.SourceId,
			consoleAppArguments.TargetRepositoryPath,
			consoleAppArguments.TargetRepositoryName,
			consoleAppArguments.TargetRepositoryRootNamespace,
			consoleAppArguments.TargetRepositoryApiLanguage,
			consoleAppArguments.TargetRepositoryMessagesLanguage,
			consoleAppArguments.TargetRepositoryDocumentationLanguage,
			consoleAppArguments.TargetRepositoryCommitMessagesLanguage,
			_serviceProvider.GetRequiredService<IReplicateCodeRepository>()),
	};
}
