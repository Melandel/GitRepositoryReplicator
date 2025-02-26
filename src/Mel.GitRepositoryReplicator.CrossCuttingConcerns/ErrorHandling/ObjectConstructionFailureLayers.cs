using System.Collections;

namespace Mel.GitRepositoryReplicator.CrossCuttingConcerns.ErrorHandling;

record ObjectConstructionFailureLayers : IEnumerable<ObjectConstructionFailure>
{
	ObjectConstructionFailure _latestFailure;
	readonly Stack<ObjectConstructionFailure> _failuresExceptLatest;
	ObjectConstructionFailureLayers(
		ObjectConstructionFailure latestfailure,
		Stack<ObjectConstructionFailure> failuresExceptLatest)
	{
		_latestFailure = latestfailure;
		_failuresExceptLatest = failuresExceptLatest;
	}

	public static ObjectConstructionFailureLayers InitializeWith(
		Type objectUnderConstructionType,
		string failureMessage)
	{
		try
		{
			var failure = ObjectConstructionFailure.FromConstructing(
				objectUnderConstructionType,
				failureMessage);

			return new(
				failure,
				new Stack<ObjectConstructionFailure>());
		}
		catch (ObjectConstructionException objectConstructionException)
		{
			objectConstructionException.EnrichWithInformationAbout<ObjectConstructionFailureLayers>(new object[] { objectUnderConstructionType, failureMessage });
			throw;
		}
		catch (Exception developerMistake)
		{
			throw ObjectConstructionException.WhenConstructingAnInstanceOf<ObjectConstructionFailureLayers>(developerMistake, new object[] { objectUnderConstructionType, failureMessage });
		}
	}

	public void AddLayer(Type objectUnderConstructionType, string failureMessage)
	{
		if (objectUnderConstructionType == _latestFailure.ObjectUnderConstruction)
		{
			_latestFailure.AddDetails(failureMessage);
		}
		else
		{
			_failuresExceptLatest.Push(_latestFailure);
			_latestFailure = ObjectConstructionFailure.FromConstructing(objectUnderConstructionType, failureMessage);
		}
	}

	public IEnumerator<ObjectConstructionFailure> GetEnumerator()
	{
		var enumerable = new List<ObjectConstructionFailure>();
		enumerable.Add(_latestFailure);
		enumerable.AddRange(_failuresExceptLatest);
		return ((IEnumerable<ObjectConstructionFailure>)enumerable).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		var enumerable = new List<ObjectConstructionFailure>();
		enumerable.Add(_latestFailure);
		enumerable.AddRange(_failuresExceptLatest);
		return ((IEnumerable)enumerable).GetEnumerator();
	}
}
