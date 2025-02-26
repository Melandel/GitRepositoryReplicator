using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Mel.GitRepositoryReplicator.Domain;

public record CodeBase : IReadOnlyDictionary<FilePath, FileContent>
{
	public IEnumerator<KeyValuePair<FilePath, FileContent>> GetEnumerator() => ((IEnumerable<KeyValuePair<FilePath, FileContent>>)_files).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_files).GetEnumerator();
	public IEnumerable<FilePath> Keys => ((IReadOnlyDictionary<FilePath, FileContent>)_files).Keys;
	public IEnumerable<FileContent> Values => ((IReadOnlyDictionary<FilePath, FileContent>)_files).Values;
	public int Count => ((IReadOnlyCollection<KeyValuePair<FilePath, FileContent>>)_files).Count;
	public bool ContainsKey(FilePath key) => ((IReadOnlyDictionary<FilePath, FileContent>)_files).ContainsKey(key);
	public bool TryGetValue(FilePath key, [MaybeNullWhen(false)] out FileContent value) => ((IReadOnlyDictionary<FilePath, FileContent>)_files).TryGetValue(key, out value);
	public FileContent this[FilePath key]
	{
		get => ((IReadOnlyDictionary<FilePath, FileContent>)_files)[key];
		set => _files[key] = value;
	}

	public override string ToString() => _files.Render();
	public static implicit operator Dictionary<FilePath, FileContent>(CodeBase obj) => obj._files;

	readonly Dictionary<FilePath, FileContent> _files;
	readonly RepositoryRootNamespace _sourceRepositoryRootNamespace;

	CodeBase(Dictionary<FilePath, FileContent> files, RepositoryRootNamespace sourceRepositoryRootNamespace)
	{
		_files = files switch
		{
			null => throw ObjectConstructionException.WhenConstructingAMemberFor<CodeBase>(nameof(_files), files),
			_ => files
		};
		_sourceRepositoryRootNamespace = sourceRepositoryRootNamespace;
	}

	public static CodeBase From(Dictionary<string, string> fileContentsByRelativeFilepath)
	{
		try
		{
			var sourceRepositoryName = RepositoryName.From("DotnetWebService");
			var sourceRepositoryRootNamespace = RepositoryRootNamespace.From("Mel.DotnetWebService");
			var files = fileContentsByRelativeFilepath.ToDictionary(
				kvp => FilePath.From(kvp.Key, sourceRepositoryRootNamespace),
				kvp => FileContent.From(kvp.Value, sourceRepositoryName, sourceRepositoryRootNamespace));

			return new(
				files,
				sourceRepositoryRootNamespace);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<CodeBase>(fileContentsByRelativeFilepath);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<CodeBase>(developerMistake, fileContentsByRelativeFilepath);
		}
	}

	public void AdaptTo(
		RepositoryName? targetRepositoryName,
		RepositoryRootNamespace targetRepositoryRootNamespace,
		Language targetRepositoryApiLanguage,
		Language targetRepositoryMessagesLanguage,
		Language targetRepositoryDocumentationLanguage)
	{
		var filepaths = _files.Keys.ToArray();
		var alreadyTakenChargeOf = new List<string>();

		foreach (var filepath in filepaths)
		{
			if (alreadyTakenChargeOf.Contains(filepath))
			{
				continue;
			}

			_files[filepath] = _files[filepath]
				.AdaptRootNamespaceTo(targetRepositoryRootNamespace)
				.AdaptRepositoryNameTo(targetRepositoryName)
				.AdaptApiLanguageTo(targetRepositoryApiLanguage)
				.AdaptMessagesLanguageTo(targetRepositoryMessagesLanguage);

			if (filepath.HasMarkdownExtension)
			{
				if (filepath.IsWrittenIn(targetRepositoryDocumentationLanguage))
				{
					_files[filepath] = _files[filepath].AdaptDateOfWritingToToday();
				}
				else
				{
					_files.Remove(filepath);
					continue;
				}
			}

			var adaptedFilePath = filepath
				.Duplicate()
				.AdaptRootNamespaceTo(targetRepositoryRootNamespace);
			if (filepath.HasMarkdownExtension && filepath.IsWrittenIn(targetRepositoryDocumentationLanguage))
			{
				adaptedFilePath = adaptedFilePath.AdaptDocumentationLanguageTo(targetRepositoryDocumentationLanguage);
			}

			if (adaptedFilePath != filepath)
			{
				if (_files.ContainsKey(adaptedFilePath))
				{
					_files.Remove(adaptedFilePath);
				}
				_files.Add(adaptedFilePath, _files[filepath]);
				alreadyTakenChargeOf.Add(adaptedFilePath);

				_files.Remove(filepath);
				alreadyTakenChargeOf.Add(filepath);
			}
		}
	}
}
