using Mel.GitRepositoryReplicator.ConsoleApp.ArgumentsParsing;

namespace Mel.GitRepositoryReplicator.ConsoleApp.Commands;

class DisplayHelpCommand : Command
{
	public static readonly string Help = $@"this is the help command.";

	readonly ConsoleAppArguments _consoleAppArguments;
	public DisplayHelpCommand(ConsoleAppArguments consoleAppArguments)
	{
		_consoleAppArguments = consoleAppArguments;
	}

	public override void Execute()
	{
		Console.WriteLine(Help);
	}
}
