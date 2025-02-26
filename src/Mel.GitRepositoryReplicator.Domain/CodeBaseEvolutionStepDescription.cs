namespace Mel.GitRepositoryReplicator.Domain;

public record CodeBaseEvolutionStepDescription
{
	public override string ToString() => _description;
	public static implicit operator string(CodeBaseEvolutionStepDescription obj) => obj._description;

	readonly string _description;
	CodeBaseEvolutionStepDescription(string description)
	{
		_description = description switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<CodeBaseEvolutionStepDescription>(nameof(_description), description),
			_ => description
		};
	}

	public static CodeBaseEvolutionStepDescription From(string description)
	{
		try
		{
			return new(description.TrimEnd());
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<CodeBaseEvolutionStepDescription>(description);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<CodeBaseEvolutionStepDescription>(developerMistake, description);
		}
	}

	public void AdaptTo(Language targetRepositoryDocLanguage)
	{
	}

	public string Truncate(int size)
	{
		var withoutNewLines = string.Join(" ", _description.Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x)));

		return (withoutNewLines.Length < size)
			? withoutNewLines
			: $"{withoutNewLines[..size]}…";
	}
}
