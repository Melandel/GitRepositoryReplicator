namespace Mel.GitRepositoryReplicator.Application.ExtensionMethods.cs;

static class RepositoryNameExtensionMethods
{
	public static Domain.RepositoryName? ToDomainRepositoryName(this RepositoryName? repositoryName)
	=> repositoryName switch
	{
		null => null,
		_ => Domain.RepositoryName.From(repositoryName)
	};
}
