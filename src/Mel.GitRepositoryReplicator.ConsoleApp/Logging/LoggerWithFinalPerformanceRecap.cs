using System.Collections.Concurrent;
using System.Diagnostics;
using Mel.GitRepositoryReplicator.CrossCuttingConcerns.Logging;

namespace Mel.GitRepositoryReplicator.ConsoleApp.Logging;

class LoggerWithFinalPerformanceRecap : CrossCuttingConcerns.Logging.ILoggerWithFinalPerformanceRecap
{
	readonly ConcurrentBag<Performance> _measuredPerformances;
	readonly Stopwatch _stopWatch;
	readonly Serilog.ILogger _logger;
	public LoggerWithFinalPerformanceRecap(
		Serilog.ILogger logger)
	{
		_measuredPerformances = new ConcurrentBag<Performance>();
		_stopWatch = Stopwatch.StartNew();
		_logger = logger;
	}

	public void Error(string message) => _logger.Error(message);
	public void Error(string message, Exception exception) => _logger.Error(message, exception);

	public void Info(string message) => _logger.Information(message);
	public void Info(string message, Exception exception) => _logger.Information(message, exception);

	public void Warn(string message) => _logger.Warning(message);
	public void Warn(string message, Exception exception) => _logger.Warning(message, exception);

	public void AddToFinalPerformanceRecap(TimeSpan duration, string actionName)
	=> _measuredPerformances.Add(Performance.From(actionName, duration));

	public void Dispose() => LogFinalPerformanceRecap();
	public void LogFinalPerformanceRecap()
	{
		Info($"== Program finished in {LogFriendlyTimeSpan.Render(_stopWatch.Elapsed)} ==");
		foreach(var measuredPerformance in _measuredPerformances.OrderBy(p => p.ExecutionDate))
		{
			Info(measuredPerformance.Render());
		}
	}

	public void Info<T>(string messageTemplate, T propertyValue) => _logger.Information(messageTemplate, propertyValue);
	public void Info<T>(Exception? exception, string messageTemplate, T propertyValue) => _logger.Information(exception, messageTemplate, propertyValue);
	public void Info<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _logger.Information(messageTemplate, propertyValue0, propertyValue1);
	public void Info<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _logger.Information(exception, messageTemplate, propertyValue0, propertyValue1);
	public void Info<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
	public void Info<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _logger.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

	public void Warn<T>(string messageTemplate, T propertyValue) => _logger.Warning(messageTemplate, propertyValue);
	public void Warn<T>(Exception? exception, string messageTemplate, T propertyValue) => _logger.Warning(exception, messageTemplate, propertyValue);
	public void Warn<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _logger.Warning(messageTemplate, propertyValue0, propertyValue1);
	public void Warn<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1);
	public void Warn<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _logger.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
	public void Warn<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);


	public void Error<T>(string messageTemplate, T propertyValue) => _logger.Error(messageTemplate, propertyValue);
	public void Error<T>(Exception? exception, string messageTemplate, T propertyValue) => _logger.Error(exception, messageTemplate, propertyValue);
	public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _logger.Error(messageTemplate, propertyValue0, propertyValue1);
	public void Error<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _logger.Error(exception, messageTemplate, propertyValue0, propertyValue1);
	public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _logger.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
	public void Error<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _logger.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
}
