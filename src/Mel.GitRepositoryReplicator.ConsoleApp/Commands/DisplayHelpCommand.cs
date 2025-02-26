namespace Mel.GitRepositoryReplicator.ConsoleApp.Commands;

class DisplayHelpCommand : Command
{
	public static readonly string Help = $@"";

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
