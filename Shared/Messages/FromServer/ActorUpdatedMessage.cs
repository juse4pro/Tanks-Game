using Godot;

namespace Shared.Messages.FromServer;

public class ActorUpdatedMessage
{
	public Guid SyncId;
	public Vector2 Position;
	public float Rotation;
}