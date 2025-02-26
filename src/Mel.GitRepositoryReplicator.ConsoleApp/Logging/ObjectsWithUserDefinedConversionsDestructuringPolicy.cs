using System.Diagnostics.CodeAnalysis;
using Mel.GitRepositoryReplicator.CrossCuttingConcerns.ExtensionMethods;
using Serilog.Core;
using Serilog.Events;

namespace Mel.GitRepositoryReplicator.ConsoleApp.Logging;

class ObjectsWithUserDefinedConversionsDestructuringPolicy : Serilog.Core.IDestructuringPolicy
{
	public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, [NotNullWhen(true)] out LogEventPropertyValue? result)
	{
		result = null;
		if (value.GetType().GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
		{
			return false;
		}

		if (value.GetType().Namespace!.StartsWith("System") && !value.GetType().Namespace!.Contains("Collection"))
		{
			result =  new ScalarValue(value);
			return true;
		}

		if (value.HasInstancePropertiesWithPublicGetter())
		{
			return false;
		}

		if (value.HasUserDefinedConversions(out var converters))
		{
			var converted = converters.First().Invoke(null, new[] { value });
			if (TryDestructure(converted!, propertyValueFactory, out var res))
			{
				result = res ?? new ScalarValue(null);
				return true;
			}
		}

		return false;
	}
}
