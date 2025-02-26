using Mel.GitRepositoryReplicator.ConsoleApp.ArgumentsParsing;

namespace Mel.GitRepositoryReplicator.ConsoleApp.Commands;

interface ICommandFactory
{
	Command CreateCommandFrom(ConsoleAppArguments consoleAppArguments);
}
