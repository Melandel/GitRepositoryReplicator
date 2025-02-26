using Mel.GitRepositoryReplicator.Application.ServiceProvidersInterfaces;
using Mel.GitRepositoryReplicator.Infrastructure.Filesystem;
using Mel.GitRepositoryReplicator.Infrastructure.Git;
using Microsoft.Extensions.DependencyInjection;

namespace Mel.GitRepositoryReplicator.Infrastructure;

public static class DependencyInjection
{
	public static ServiceCollection AddInfrastructure(this ServiceCollection services)
	{
		return services
			.AddConfiguration()
			.AddServiceProvidersImplementations();
	}

	static ServiceCollection AddConfiguration(this ServiceCollection services)
	{
		services.AddSingleton<Configuration>(sp =>
			sp.GetRequiredService<IConfigurationProvider>().Provide());
		return services;
	}

	static ServiceCollection AddServiceProvidersImplementations(this ServiceCollection container)
	{
		container.AddTransient<ICodeRepositoryProvider, CodeRepositoryProvider>();
		container.AddTransient<ICodeRepositoryPersister, CodeRepositoryPersister>();
		container.AddTransient<IGitClient, GitClient>();
		container.AddTransient<IFilesystem, Mel.GitRepositoryReplicator.Infrastructure.Filesystem.Filesystem>();
		return container;
	}
}
