using Godot;
using Shared;

namespace Server;

public class ActorCollection
{
	private readonly List<SharedActor> _pendingActors = [];
	private readonly List<SharedActor> _actors = [];


	public void Update(float delta)
	{
		this._actors.AddRange(this._pendingActors);
		foreach (SharedActor pendingActor in this._pendingActors)
		{
			pendingActor._EnterTree();
			pendingActor.EmitSignal(Node.SignalName.TreeEntered);
			pendingActor.EmitSignal(Node.SignalName.Ready);
			pendingActor._Ready();
		}

		this._pendingActors.Clear();

		foreach (SharedActor actor in this._actors)
		{
			actor._PhysicsProcess(delta);
			actor._Process(delta);
		}


		this._actors.RemoveAll(actor =>
		{
			if (actor.IsQueuedForDeletion())
			{
				actor._ExitTree();
				actor.EmitSignal(Node.SignalName.TreeExiting);
				actor.EmitSignal(Node.SignalName.TreeExited);
				return true;
			}
			else
			{
				return false;
			}
		});
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