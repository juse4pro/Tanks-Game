namespace Shared;

public static class SyncService
{
	private static readonly Dictionary<Guid, SharedActor> ActorRegistry = [];


	public static Guid GetNewId()
	{
		return Guid.NewGuid();
	}


	public static SharedActor GetActorById(Guid id)
	{
		return ActorRegistry.TryGetValue(id, out SharedActor? actor)
			? actor
			: throw new KeyNotFoundException($"No actor found with id {id}");
	}


	public static bool TryGetActorById(Guid id, out SharedActor? actor)
	{
		return ActorRegistry.TryGetValue(id, out actor);
	}


	public static void RegisterActor(SharedActor actor)
	{
		if (!ActorRegistry.TryAdd(actor.SyncId, actor))
			throw new InvalidOperationException($"Actor with id {actor.SyncId} already registered");
	}


	public static void UnregisterActor(SharedActor actor)
	{
		if (!ActorRegistry.Remove(actor.SyncId))
			throw new InvalidOperationException($"Actor with id {actor.SyncId} is unrecognized");
	}


	public static void Clear()
	{
		ActorRegistry.Clear();
	}
}