using Microsoft.Extensions.DependencyInjection;

namespace Mel.GitRepositoryReplicator.Domain;

public static class DependencyInjection
{
	public static ServiceCollection AddDomainModel(this ServiceCollection services)
	{
		return services.AddConfiguration();
	}

	static ServiceCollection AddConfiguration(this ServiceCollection services)
	{
		services.AddSingleton<Configuration>(sp =>
			sp.GetRequiredService<IConfigurationProvider>().Provide());
		return services;
	}
}
