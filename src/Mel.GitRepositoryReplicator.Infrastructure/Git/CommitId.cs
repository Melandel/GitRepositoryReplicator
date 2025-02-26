namespace Mel.GitRepositoryReplicator.Infrastructure.Git;

record CommitId
{
	public override string ToString() => _sha1;
	public static implicit operator string(CommitId obj) => obj._sha1;

	readonly string _sha1;
	CommitId(string sha1)
	{
		_sha1 = sha1 switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<CommitId>(nameof(_sha1), sha1),
			_ => sha1
		};
	}

	public static CommitId FromSha1(string sha1)
	{
		try
		{
			return new(sha1);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<CommitId>(sha1);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<CommitId>(developerMistake, sha1);
		}
	}
}
