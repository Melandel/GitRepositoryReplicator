namespace Mel.GitRepositoryReplicator.ConsoleApp.Commands;

interface ICommandFactory
{
	Command CreateCommandFrom(ConsoleAppArguments consoleAppArguments);
}
