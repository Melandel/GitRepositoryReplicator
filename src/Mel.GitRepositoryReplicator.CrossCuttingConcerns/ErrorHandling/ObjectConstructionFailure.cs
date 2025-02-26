namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.ErrorHandling;

class ObjectConstructionFailure
{
	public Type ObjectUnderConstruction { get; }
	public string Name => _messages.First();
	public IReadOnlyCollection<string> Details
	=> _messages.Any()
		? _messages.Skip(1).ToArray()
		: Array.Empty<string>();

	readonly Stack<string> _messages;

	ObjectConstructionFailure(
		Type objectUnderConstruction,
		Stack<string> messages)
	{
		ObjectUnderConstruction = objectUnderConstruction;
		_messages = messages;
	}

	public static ObjectConstructionFailure FromConstructing(
		Type objectUnderConstructionType,
		string failureMessage)
	{
		var messages = new Stack<string>();
		messages.Push(failureMessage);

		return new(
			objectUnderConstructionType,
			messages);
	}

	public void AddDetails(string failureMessage)
	=> _messages.Push(failureMessage);
}
