namespace Mel.GitRepositoryReplicator.Domain;

public record CodeBaseEvolutionStep
{
	public CodeBase CodeBase { get; init; }
	public CodeBaseEvolutionStepDescription Description { get; init; }
	CodeBaseEvolutionStep(CodeBase codeBase, CodeBaseEvolutionStepDescription Description)
	{
		CodeBase = codeBase switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<CodeBaseEvolutionStep>(nameof(CodeBase), codeBase),
			_ => codeBase
		};
		this.Description = Description;
	}

	public static CodeBaseEvolutionStep From(CodeBase codeBase, CodeBaseEvolutionStepDescription codeBaseEvolutionStepDescription)
	{
		try
		{
			return new(codeBase, codeBaseEvolutionStepDescription);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<CodeBaseEvolutionStep>(codeBase, codeBaseEvolutionStepDescription);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<CodeBaseEvolutionStep>(developerMistake, codeBase, codeBaseEvolutionStepDescription);
		}
	}
}
