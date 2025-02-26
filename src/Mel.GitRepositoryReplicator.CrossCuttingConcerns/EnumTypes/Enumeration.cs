using System.Reflection;

namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.EnumTypes;

public abstract class Enumeration
{
	public string Name { get; }
	public string Code { get; }

	protected Enumeration(string name, string code)
	{
		Name = name;
		Code = code;
	}

	public static TEnum FromString<TEnum>(string str) where TEnum : Enumeration
	{
		var all = GetAll<TEnum>();
		var firstByName = all.FirstOrDefault(x => string.Equals(x.Name, str, StringComparison.InvariantCultureIgnoreCase));
		if (firstByName != null)
		{
			return firstByName;
		}
		var firstByCode = all.FirstOrDefault(x => string.Equals(x.Code, str, StringComparison.InvariantCultureIgnoreCase));
		if (firstByCode != null)
		{
			return firstByCode;
		}

		throw ObjectConstructionException.WhenConstructingAnInstanceOf<TEnum>($"Found no {typeof(TEnum).Name} matching the name or code {str}");
	}

	public static IReadOnlyCollection<TEnum> GetAll<TEnum>() where TEnum : Enumeration
	=> typeof(TEnum)
		.GetMembers(BindingFlags.Static | BindingFlags.Public)
		.OfType<FieldInfo>()
		.Where(f => typeof(TEnum).IsAssignableFrom(f.FieldType))
		.Select(f => (TEnum)f.GetValue(null)!)
		.ToArray();
}
