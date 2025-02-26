namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.ConstrainedTypes;

public interface ILanguageResolver
{
	Language Resolve(string str);
}
