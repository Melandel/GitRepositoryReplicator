namespace Mel.GitRepositoryReplicator.Domain;

public record CodeBaseEvolutionStepDescription
{
	public override string ToString() => _description;
	public static implicit operator string(CodeBaseEvolutionStepDescription obj) => obj._description;

	string _description;
	const string SeparatorBetweenEnglishAndFrenchTexts = "-fr-";
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

	public CodeBaseEvolutionStepDescription AdaptTo(Language targetRepositoryCommitMessagesLanguage)
	{
		_description = _description.Split(SeparatorBetweenEnglishAndFrenchTexts) switch
		{
			[var englishText, var frenchText] when targetRepositoryCommitMessagesLanguage == Language.English => englishText.Trim(),
			[var englishText, var frenchText] when targetRepositoryCommitMessagesLanguage == Language.French => frenchText.Trim(),
			_ => _description.Trim()
		};
		return this;
	}

	public string Truncate(int size)
	{
		var withoutNewLines = string.Join(" ", _description.Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x)));

		return (withoutNewLines.Length < size)
			? withoutNewLines
			: $"{withoutNewLines[..size]}…";
	}
}
