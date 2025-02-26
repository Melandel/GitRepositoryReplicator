using Mel.GitRepositoryReplicator.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Mel.GitRepositoryReplicator.Application;

public static class DependencyInjection
{
	public static ServiceCollection AddUseCases(this ServiceCollection services)
	{
		services
			.AddConfiguration()
			.AddTransient<IReplicateCodeRepository, ReplicateCodeRepository>();
		return services;
	}

	static ServiceCollection AddConfiguration(this ServiceCollection services)
	{
		services.AddSingleton<Configuration>(sp =>
			sp.GetRequiredService<IConfigurationProvider>().Provide());
		return services;
	}
}
