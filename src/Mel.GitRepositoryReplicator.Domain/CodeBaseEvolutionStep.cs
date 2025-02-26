namespace Mel.GitRepositoryReplicator.Domain;

public record CodeBaseEvolutionStep
{
	public CodeBase CodeBase { get; init; }
	public CodeBaseEvolutionStepDescription Description { get; private set; }
	CodeBaseEvolutionStep(CodeBase codeBase, CodeBaseEvolutionStepDescription description)
	{
		CodeBase = codeBase switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<CodeBaseEvolutionStep>(nameof(CodeBase), codeBase),
			_ => codeBase
		};
		Description = description;
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

	public void AdaptTo(
		RepositoryName? targetRepositoryName,
		RepositoryRootNamespace targetRepositoryRootNamespace,
		Language targetRepositoryApiLanguage,
		Language targetRepositoryMessagesLanguage,
		Language targetRepositoryDocumentationLanguage,
		Language targetRepositoryCommitMessagesLanguage)
	{
		CodeBase.AdaptTo(
			targetRepositoryName,
			targetRepositoryRootNamespace,
			targetRepositoryApiLanguage,
			targetRepositoryMessagesLanguage,
			targetRepositoryDocumentationLanguage);

		Description.AdaptTo(targetRepositoryCommitMessagesLanguage);
	}
}
