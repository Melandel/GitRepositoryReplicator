using System.Collections;

namespace Mel.GitRepositoryReplicator.Domain;

public record CodeBaseEvolutionOverTime : IReadOnlyCollection<CodeBaseEvolutionStep>
{
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
		RepositoryName targetRepositoryName,
		RepositoryNamespace targetRepositoryNamespace,
		Language TargetRepositoryApiLanguage,
		Language TargetRepositoryMessagesLanguage,
		Language TargetRepositoryDocLanguage)
	{
	}

	public IEnumerator<CodeBaseEvolutionStep> GetEnumerator()
	{
		return ((IEnumerable<CodeBaseEvolutionStep>)_steps).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _steps.GetEnumerator();
	}
}
