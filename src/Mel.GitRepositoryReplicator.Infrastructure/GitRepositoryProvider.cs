using Mel.GitRepositoryReplicator.Application;
using Mel.GitRepositoryReplicator.Application.ServiceProvidersInterfaces;
using Mel.GitRepositoryReplicator.Domain;

namespace Mel.GitRepositoryReplicator.Infrastructure;

class CodeRepositoryProvider : ICodeRepositoryProvider
{
	public CodeBaseEvolutionOverTime GetCodeBaseEvolutionOverTime(CodeRepositoryId sourceRepositoryId)
	{
		return CodeBaseEvolutionOverTime.From(new[]
		{
			CodeBaseEvolutionStep.From(
				CodeBase.From(new Dictionary<FilePath, FileContent>
				{
					{ FilePath.From("some/path"), FileContent.From("hello world") },
					{ FilePath.From("some/other/path"), FileContent.From("hello, world!") },
				}),
				CodeBaseEvolutionStepDescription.From("refactored this"))
		});
	}
}
