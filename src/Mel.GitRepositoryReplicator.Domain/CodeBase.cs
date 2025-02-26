using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Mel.GitRepositoryReplicator.Domain;

public record CodeBase : IReadOnlyDictionary<FilePath, FileContent>
{
	public IEnumerable<FilePath> Keys => ((IReadOnlyDictionary<FilePath, FileContent>)_files).Keys;
	public IEnumerable<FileContent> Values => ((IReadOnlyDictionary<FilePath, FileContent>)_files).Values;
	public int Count => ((IReadOnlyCollection<KeyValuePair<FilePath, FileContent>>)_files).Count;
	public FileContent this[FilePath key] => ((IReadOnlyDictionary<FilePath, FileContent>)_files)[key];

	public override string ToString() => _files.Render();
	public static implicit operator Dictionary<FilePath, FileContent>(CodeBase obj) => obj._files;

	readonly Dictionary<FilePath, FileContent> _files;
	CodeBase(Dictionary<FilePath, FileContent> files)
	{
		_files = files switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<CodeBase>(nameof(_files), files),
			_ => files
		};
	}

	public static CodeBase From(Dictionary<FilePath, FileContent> files)
	{
		try
		{
			return new(files);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<CodeBase>(files);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<CodeBase>(developerMistake, files);
		}
	}

	public bool ContainsKey(FilePath key)
	{
		return ((IReadOnlyDictionary<FilePath, FileContent>)_files).ContainsKey(key);
	}

	public bool TryGetValue(FilePath key, [MaybeNullWhen(false)] out FileContent value)
	{
		return ((IReadOnlyDictionary<FilePath, FileContent>)_files).TryGetValue(key, out value);
	}

	public IEnumerator<KeyValuePair<FilePath, FileContent>> GetEnumerator()
	{
		return ((IEnumerable<KeyValuePair<FilePath, FileContent>>)_files).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)_files).GetEnumerator();
	}
}
