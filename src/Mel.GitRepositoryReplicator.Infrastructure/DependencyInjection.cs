using Mel.GitRepositoryReplicator.Application.ServiceProvidersInterfaces;
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
		container.AddTransient<IGitRepositoryProvider, GitRepositoryProvider>();
		container.AddTransient<IGitRepositoryPersister, GitRepositoryPersister>();
		return container;
	}
}
