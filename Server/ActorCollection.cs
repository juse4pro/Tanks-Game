using Shared;

namespace Server;

public class ActorCollection
{
	private readonly List<SharedActor> _pendingActors = [];
	private readonly List<SharedActor> _actors = [];


	public void Update(float delta)
	{
		this._actors.AddRange(this._pendingActors);
		this._pendingActors.Clear();

		foreach (SharedActor actor in this._actors)
			actor._Process(delta);

		this._actors.RemoveAll(actor => actor.IsQueuedForDeletion());
	}


	public void Add(SharedActor actor)
	{
		this._pendingActors.Add(actor);
	}


	public IEnumerable<SharedActor> GetAllActors()
	{
		foreach (SharedActor pendingActor in this._pendingActors)
			yield return pendingActor;
		
		foreach (SharedActor actor in this._actors)
			yield return actor;
	}
}