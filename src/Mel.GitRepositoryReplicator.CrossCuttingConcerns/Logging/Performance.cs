namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.Logging;

public class Performance
{
	public string ActionName { get; }
	public DateTime ExecutionDate { get; }
	public TimeSpan ExecutionDuration => _executionDuration;
	readonly LogFriendlyTimeSpan _executionDuration;

	Performance(
		string actionName,
		DateTime executionDate,
		LogFriendlyTimeSpan executionDuration)
	{
		ActionName = actionName switch
		{
			var action when String.IsNullOrWhiteSpace(action) => throw ObjectConstructionException.WhenConstructingAMemberFor<Performance>(nameof(ActionName), actionName),
			var action when action.EndsWith(".") => throw ObjectConstructionException.WhenConstructingAMemberFor<Performance>(nameof(ActionName), actionName, "ne doit pas se terminer par un point"),
			_ => actionName
		};

		this.ExecutionDate = executionDate switch
		{
			var date when date == default(DateTime) => throw ObjectConstructionException.WhenConstructingAMemberFor<Performance>(nameof(Performance.ExecutionDate), executionDate),
			_ => executionDate
		};

		_executionDuration = executionDuration;
	}

	public static Performance From(
		string actionName,
		TimeSpan executionDuration)
	=> new Performance(
		actionName.TrimEnd('.'),
		DateTime.Now,
		executionDuration);

	public string Render() => $"{_executionDuration,8} spent on {ActionName}.";
}
