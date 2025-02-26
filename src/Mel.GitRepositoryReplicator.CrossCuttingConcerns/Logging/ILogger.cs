namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.Logging;

public interface ILogger
{
	void Info(string message);
	void Info<T>(string messageTemplate, T propertyValue);
	void Info<T>(Exception? exception, string messageTemplate, T propertyValue);
	void Info<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
	void Info<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);
	void Info<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
	void Info<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);

	void Warn(string message);
	void Warn<T>(string messageTemplate, T propertyValue);
	void Warn<T>(Exception? exception, string messageTemplate, T propertyValue);
	void Warn<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
	void Warn<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);
	void Warn<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
	void Warn<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);

	void Error(string message);
	void Error<T>(string messageTemplate, T propertyValue);
	void Error<T>(Exception? exception, string messageTemplate, T propertyValue);
	void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
	void Error<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);
	void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
	void Error<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
}
