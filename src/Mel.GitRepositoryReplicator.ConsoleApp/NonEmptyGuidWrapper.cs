namespace Mel.GitRepositoryReplicator.ConsoleApp;

record NonEmptyGuidWrapper
{
	public override string ToString() => $"{_encapsulated}";
	public static implicit operator NonEmptyGuid(NonEmptyGuidWrapper obj) => obj._encapsulated;

	readonly NonEmptyGuid _encapsulated;
	NonEmptyGuidWrapper(NonEmptyGuid encapsulated)
	{
		_encapsulated = encapsulated switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<NonEmptyGuidWrapper>(nameof(_encapsulated), encapsulated),
			_ => encapsulated
		};
	}

	public static NonEmptyGuidWrapper From(Guid encapsulated)
	{
		try
		{
			return new(NonEmptyGuid.From(encapsulated));
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<NonEmptyGuidWrapper>(encapsulated);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<NonEmptyGuidWrapper>(developerMistake, encapsulated);
		}
	}
}
