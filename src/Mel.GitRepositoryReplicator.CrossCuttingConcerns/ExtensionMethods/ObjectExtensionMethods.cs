﻿using System.Reflection;
using Mel.GitRepositoryReplicator.CrossCuttingConcerns.Logging;

namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.ExtensionMethods;

public static class ObjectExtensionMethods
{
	public static string Render(this object? obj)
	=> ObjectRenderer.Render(obj);

	public static bool HasInstancePropertiesWithPublicGetter(this object obj)
	=> obj.GetType()
		.GetProperties(BindingFlags.Public | BindingFlags.Instance)
		.Where(property => property.HasAPublicGetter(out var _))
		.Any();

	public static bool HasUserDefinedConversions(this object obj, out MethodInfo[] converters)
	{
		var methods = obj.GetType().GetMethods().ToArray();
		var implicitConverters = methods.Where(mi => mi.Name == "op_Implicit");
		var explicitConverters = methods.Where(mi => mi.Name == "op_Explicit");

		converters = implicitConverters
			.Concat(explicitConverters)
			.ToArray();
		return converters.Any();
	}

	public static bool HasConstructorWhoseParametersAllHaveAMatchingPropertyOrField(this object obj, out PropertyInfo[] propertiesToRender, out FieldInfo[] fieldsToRender)
	{
		var type = obj.GetType();

		var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		var parametersOfEachConstructor = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
			.Select(ctor => ctor.GetParameters())
			.OrderByDescending(parameters => parameters.Length);

		foreach(var parametersOfOneConstructor in parametersOfEachConstructor)
		{
			var matchingProperties = new List<PropertyInfo>();
			var matchingFields = new List<FieldInfo>();
			foreach (var param in parametersOfOneConstructor)
			{
				var matchingProperty = properties.FirstOrDefault(propertyInfo => string.Equals(param.Name, propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase));
				if (matchingProperty != null)
				{
					matchingProperties.Add(matchingProperty);
					continue;
				}

				var matchingFieldName = fields.FirstOrDefault(field => string.Equals(param.Name, field.Name, StringComparison.InvariantCultureIgnoreCase));
				if (matchingFieldName != null)
				{
					matchingFields.Add(matchingFieldName);
					continue;
				}

				break;
			}

			var numberOfMatchesFound = matchingProperties.Count + matchingFields.Count;
			var isConstructorWhoseParametersAllHaveAMatchingPropertyOrField = (numberOfMatchesFound == parametersOfOneConstructor.Length);
			if (isConstructorWhoseParametersAllHaveAMatchingPropertyOrField)
			{
				propertiesToRender = matchingProperties.ToArray();
				fieldsToRender = matchingFields.ToArray();
				return true;
			}
		}

		propertiesToRender = Array.Empty<PropertyInfo>();
		fieldsToRender = Array.Empty<FieldInfo>();
		return false;
	}
}

