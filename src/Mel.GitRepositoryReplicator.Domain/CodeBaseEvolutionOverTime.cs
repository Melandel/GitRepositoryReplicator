using System.Collections;

namespace Mel.GitRepositoryReplicator.Domain;

public record CodeBaseEvolutionOverTime : IReadOnlyCollection<CodeBaseEvolutionStep>
{
	public IEnumerator<CodeBaseEvolutionStep> GetEnumerator() => ((IEnumerable<CodeBaseEvolutionStep>)_steps).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => _steps.GetEnumerator();

	public override string ToString() => _steps.Render();

	readonly CodeBaseEvolutionStep[] _steps;

	public int Count => ((IReadOnlyCollection<CodeBaseEvolutionStep>)_steps).Count;

	CodeBaseEvolutionOverTime(CodeBaseEvolutionStep[] steps)
	{
		_steps = steps switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<CodeBaseEvolutionOverTime>(nameof(_steps), steps),
			_ => steps
		};
	}

	public static CodeBaseEvolutionOverTime From(IEnumerable<CodeBaseEvolutionStep> steps)
	{
		try
		{
			return new(steps.ToArray());
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<CodeBaseEvolutionOverTime>(steps);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<CodeBaseEvolutionOverTime>(developerMistake, steps);
		}
	}

	public void AdaptTo(
		RepositoryName? targetRepositoryName,
		RepositoryRootNamespace targetRepositoryRootNamespace,
		Language targetRepositoryApiLanguage,
		Language targetRepositoryMessagesLanguage,
		Language targetRepositoryDocumentationLanguage,
		Language targetRepositoryCommitMessagesLanguage)
	{
		foreach(var step in _steps)
		{
			step.AdaptTo(
				targetRepositoryName,
				targetRepositoryRootNamespace,
				targetRepositoryApiLanguage,
				targetRepositoryMessagesLanguage,
				targetRepositoryDocumentationLanguage,
				targetRepositoryCommitMessagesLanguage);
		}
	}
}
