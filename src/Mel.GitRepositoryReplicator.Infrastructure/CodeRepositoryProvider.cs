using Mel.GitRepositoryReplicator.Application;
using Mel.GitRepositoryReplicator.Application.ServiceProvidersInterfaces;
using Mel.GitRepositoryReplicator.Domain; using Mel.GitRepositoryReplicator.Infrastructure.Filesystem;
using Mel.GitRepositoryReplicator.Infrastructure.Git;

namespace Mel.GitRepositoryReplicator.Infrastructure;

class CodeRepositoryProvider : ICodeRepositoryProvider
{
	readonly IGitClient _gitClient;
	readonly IFilesystem _filesystem;
	public CodeRepositoryProvider(IGitClient gitClient, IFilesystem filesystem)
	{
		_gitClient = gitClient;
		_filesystem = filesystem;
	}

	public CodeBaseEvolutionOverTime GetCodeBaseEvolutionOverTime(CodeRepositoryId sourceRepositoryId)
	{
		var tmpFolderName = $"GitRepositoryReplicator-{Guid.NewGuid()}";
		Console.WriteLine($"<SOURCE REPOSITORY> {sourceRepositoryId}");
		var tmpRepositoryPath =_gitClient.Clone(sourceRepositoryId, tmpFolderName);
		var commitIds = _gitClient.GetAllCommitIds(tmpRepositoryPath);

		var codeBaseEvolutionSteps = new List<CodeBaseEvolutionStep>();
		foreach(var (n, commitId) in commitIds.Index())
		{
			Console.Write($"read original commit {$"#{n+1}",3}: ");

			_gitClient.Checkout(tmpRepositoryPath, commitId);
			var codeBase = _filesystem.Read(tmpRepositoryPath);
			var stepDescription = _gitClient.GetCommitMessage(tmpRepositoryPath, commitId);
			Console.WriteLine(stepDescription.Truncate(160));

			var codeBaseEvolutionStep = CodeBaseEvolutionStep.From(codeBase, stepDescription);
			codeBaseEvolutionSteps.Add(codeBaseEvolutionStep);
		}

		_filesystem.DeleteFolder(tmpRepositoryPath);
		return CodeBaseEvolutionOverTime.From(codeBaseEvolutionSteps);
	}

	public CodeBaseEvolutionOverTime GetCodeBaseEvolutionOverTimeMock(CodeRepositoryId sourceRepositoryId)
	{
		return CodeBaseEvolutionOverTime.From(
			new[]
			{
				CodeBaseEvolutionStep.From(
					CodeBase.From(
						new Dictionary<string, string>
						{
							{ "some/path", "hello world" },
							{ "some/other/path", "hello, world!" },
						}),
					CodeBaseEvolutionStepDescription.From("refactored this")),
				CodeBaseEvolutionStep.From(
					CodeBase.From(
						new Dictionary<string, string>
						{
							{ "some/path", "hello world!" },
							{ "some/path2", "hello world!!!" },
							{ "some/other/path", "hello, world!!" },
						}),
					CodeBaseEvolutionStepDescription.From(@"🏘 architecture    (scope): foo

💌 Motivation: bar
🐾 Step for baz
"))
			});
	}
}
