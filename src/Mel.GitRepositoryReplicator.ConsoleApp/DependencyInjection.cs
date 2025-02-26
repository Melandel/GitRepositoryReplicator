using Mel.GitRepositoryReplicator.ConsoleApp.ArgumentsParsing;
using Mel.GitRepositoryReplicator.ConsoleApp.Commands;
using Mel.GitRepositoryReplicator.ConsoleApp.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Mel.GitRepositoryReplicator.ConsoleApp;

static class DependencyInjection
{
	public static ServiceCollection AddCommands(this ServiceCollection services)
	{
		services.AddTransient<ICommandFactory, CommandFactory>();
		services.AddTransient<ReplicateGitRepositoryCommand>();
		return services;
	}

	public static ServiceCollection AddConfigurations(this ServiceCollection container)
	{
		container.AddSingleton<Mel.GitRepositoryReplicator.Infrastructure.IConfigurationProvider, Configuration.ConfigurationProvider>();
		container.AddSingleton<Application.IConfigurationProvider, Configuration.ConfigurationProvider>();
		container.AddSingleton<Domain.IConfigurationProvider, Configuration.ConfigurationProvider>();
		return container;
	}

	public static ServiceCollection AddLogger(this ServiceCollection services)
	{
		var serilogLogger = new LoggerConfiguration()
			.WriteTo.Console()
			.Destructure.With<ObjectsWithUserDefinedConversionsDestructuringPolicy>()
			.CreateLogger();

		var logger = new Logging.LoggerWithFinalPerformanceRecap(serilogLogger);

		services.AddSingleton<CrossCuttingConcerns.Logging.ILogger>(logger);
		services.AddSingleton<CrossCuttingConcerns.Logging.ILoggerWithFinalPerformanceRecap>(logger);
		return services;
	}

	public static ServiceCollection AddConsoleAppArgumentsParsing(this ServiceCollection services)
	{
		services.AddSingleton<ILanguageResolver, LanguageResolver>();
		return services;
	}
}
