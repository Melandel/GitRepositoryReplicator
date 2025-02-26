using Microsoft.Extensions.DependencyInjection;
using Mel.GitRepositoryReplicator.ConsoleApp;
using Mel.GitRepositoryReplicator.Infrastructure;
using Mel.GitRepositoryReplicator.Application;
using Mel.GitRepositoryReplicator.Domain;
using Mel.GitRepositoryReplicator.ConsoleApp.Commands;
using Mel.GitRepositoryReplicator.CrossCuttingConcerns.Logging;
using Mel.GitRepositoryReplicator.ConsoleApp.ArgumentsParsing;

var serviceProvider = new ServiceCollection()
	.AddConsoleAppArgumentsParsing()
	.AddCommands()
	.AddConfigurations()
	.AddLogger()
	.AddInfrastructure()
	.AddUseCases()
	.AddDomainModel()
	.BuildServiceProvider();

using var logger = serviceProvider.GetRequiredService<ILoggerWithFinalPerformanceRecap>();

try
{
	var consoleAppArguments = ConsoleAppArguments.From(
		args,
		serviceProvider.GetRequiredService<ILanguageResolver>());

	serviceProvider
		.GetRequiredService<ICommandFactory>()
		.CreateCommandFrom(consoleAppArguments)
		.Execute();

	return 0;
}
catch (Exception ex)
{
	logger.Error(ex.Message);
	return 1;
}
