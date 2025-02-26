using Mel.GitRepositoryReplicator.Application;

namespace Mel.GitRepositoryReplicator.Infrastructure.Git;

class GitClient : IGitClient
{
	public void InitRepository(FolderPath targetRepositoryPath)
	=> Git($"init --initial-branch=main {targetRepositoryPath}");

	public void Commit(FolderPath targetRepositoryPath, Domain.CodeBaseEvolutionStepDescription codeBaseEvolutionStepDescription)
	{
		Git("add .", from: targetRepositoryPath);
		Git($"commit -m \"{codeBaseEvolutionStepDescription}\"", from: targetRepositoryPath);
	}

	public FolderPath Clone(CodeRepositoryId repositoryId, string folderName)
	{
		var repositoryPath = FolderPath.From(Path.Combine(Environment.GetEnvironmentVariable("TMP")!, folderName));

		Git($"clone {repositoryId} {repositoryPath}");
		return repositoryPath;
	}

	public IReadOnlyCollection<CommitId> GetAllCommitIds(FolderPath repositoryPath)
	{
		var commitIdsFromMostRecentToOldest = Git("log --format=format:%H", repositoryPath);

		return commitIdsFromMostRecentToOldest
			.Reverse()
			.Select(stdOutLine => CommitId.FromSha1(stdOutLine))
			.ToArray();
	}

	public void Checkout(FolderPath repositoryPath, CommitId commitId)
	{
		Git($"checkout {commitId}", repositoryPath);
	}

	public Domain.CodeBaseEvolutionStepDescription GetCommitMessage(FolderPath repositoryPath, CommitId commitId)
	{
		var stdOut = Git($"log --format=%B -n 1 {commitId}", repositoryPath);
		var commitMessage = string.Join(Environment.NewLine, stdOut).TrimEnd();

		return Domain.CodeBaseEvolutionStepDescription.From(commitMessage);
	}

	IReadOnlyCollection<string> Git(string args, FolderPath? from = null)
	{
		var gitCommand = new System.Diagnostics.Process
		{
			StartInfo = new System.Diagnostics.ProcessStartInfo
			{
				FileName = "git.exe",
				Arguments = args,
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardOutput = true
			}
		};

		if (from != null)
		{
				gitCommand.StartInfo.WorkingDirectory = from;
		}

		List<string> stdOutput = new List<string>();
		gitCommand.OutputDataReceived += (sender, args) => {
			if (args.Data != null)
			{
				stdOutput.Add(args.Data);
			}
		};

		gitCommand.Start();
		gitCommand.BeginOutputReadLine();
		gitCommand.WaitForExit();

		return stdOutput;
	}
}
