namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.ConstrainedTypes.DotnetNative;

public record NonEmptyGuid
{
	public override string ToString() => $"{_encapsulated}";
	public static implicit operator Guid(NonEmptyGuid obj) => obj._encapsulated;

	readonly Guid _encapsulated;
	NonEmptyGuid(Guid encapsulated)
	{
		_encapsulated = encapsulated switch
		{
			var v when v == Guid.Empty => throw ObjectConstructionException.WhenConstructingAMemberFor<NonEmptyGuid>(nameof(_encapsulated), encapsulated),
			_ => encapsulated
		};
	}

	public static NonEmptyGuid From(Guid encapsulated)
	{
		try
		{
			return new(encapsulated);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<NonEmptyGuid>(encapsulated);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<NonEmptyGuid>(developerMistake, encapsulated);
		}
	}
}
