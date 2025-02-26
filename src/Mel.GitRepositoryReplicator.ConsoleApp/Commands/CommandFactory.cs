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
		{ FailedToParse: true } => new DisplayHelpCommand(consoleAppArguments),
		_ => _serviceProvider.GetRequiredService<ReplicateGitRepositoryCommand>(),
	};
}
