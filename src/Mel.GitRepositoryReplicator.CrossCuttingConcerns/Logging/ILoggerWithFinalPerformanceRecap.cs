namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.Logging;

public interface ILoggerWithFinalPerformanceRecap : ILogger, IDisposable
{
	void AddToFinalPerformanceRecap(TimeSpan duration, string actionName);
	void LogFinalPerformanceRecap();
}
