namespace Mel.GitRepositoryReplicator.ConsoleApp.Configuration;

class ConfigurationProvider
	: Infrastructure.IConfigurationProvider,
	Application.IConfigurationProvider,
	Domain.IConfigurationProvider
{
	Infrastructure.Configuration Infrastructure.IConfigurationProvider.Provide()
	{
		return new Infrastructure.Configuration();
	}

	Application.Configuration Application.IConfigurationProvider.Provide()
	{
		return new Application.Configuration();
	}

	Domain.Configuration Domain.IConfigurationProvider.Provide()
	{
		return new Domain.Configuration();
	}
}
