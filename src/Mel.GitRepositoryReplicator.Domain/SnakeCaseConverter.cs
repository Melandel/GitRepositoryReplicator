using System.Text;

namespace Mel.GitRepositoryReplicator.Domain;

class SnakeCaseConverter
{
	public string? Convert(string? str)
	{
		if (String.IsNullOrEmpty(str))
		{
			return str;
		}

		StringBuilder builder = new StringBuilder();
		for (var i = 0; i < str.Length; i++)
		{
			builder.Append(
				str[i] switch
				{
					var c when char.IsLower(c) => c,
					var c when i == 0 => char.ToLower(c),
					var c => str[i-1] switch
					{
						var p when char.IsDigit(c) && !char.IsDigit(p) && char.IsLower(p) => $"_{c}",
						var p when char.IsDigit(c) && !char.IsDigit(p) => c,
						var p when char.IsDigit(c) && char.IsDigit(p) => c,
						var p when char.IsLower(p) => $"_{char.ToLower(c)}",
						var p when i+1 == str.Length => char.ToLower(c),
						var p => str[i+1] switch
						{
							var n when char.IsUpper(n) => char.ToLower(c),
						 _ => $"_{char.ToLower(c)}"
						}
					},
				});
		}
		return builder.ToString();
	}
}
