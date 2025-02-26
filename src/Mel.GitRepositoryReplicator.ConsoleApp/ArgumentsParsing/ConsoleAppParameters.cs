namespace Mel.GitRepositoryReplicator.ConsoleApp.ArgumentsParsing;

class ConsoleAppParameters
{
	readonly List<ConsoleAppParameter> _encapsulated;
	ConsoleAppParameters(List<ConsoleAppParameter> encapsulated)
	{
		_encapsulated = encapsulated;
	}

	public string this[string key]
	{
		get
		{
			var match = _encapsulated.FirstOrDefault(p => string.Equals(key, p.Name, StringComparison.InvariantCultureIgnoreCase));
			return match switch
			{
				null => throw new ArgumentOutOfRangeException(),
				_ => match.Value
			};
		}
	}

	public static ConsoleAppParameters From(string args)
	{
		try
		{
				Console.WriteLine($"args = \"{args}\"");
			var parameters = new List<ConsoleAppParameter>();
			foreach(var str in args.Split("--", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
			{
				var sep = str.IndexOf(' ');
				var parameterName = str[..sep];
				var parameterValue = str[sep..].Trim();
				parameters.Add(new ConsoleAppParameter(parameterName, parameterValue));
			}
			return new(parameters);
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<ConsoleAppParameters>(args);
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<ConsoleAppParameters>(developerMistake, args);
		}
	}
}
