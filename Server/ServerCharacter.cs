using Godot;
using LiteNetLib;
using LiteNetLib.Utils;
using Server.Messages.ServerToClient;
using Shared;

namespace Server;

public partial class ServerCharacter : SharedCharacter, IServerActor
{
	public const float Speed = 200;

	public NetPlayer OwningPlayer;


	public ServerCharacter()
	{
		this.GodotResourceUid = ResourceUid.TextToId("uid://dlltarjrl4x3j");
	}


	public void Serialize(NetDataWriter writer)
	{
		writer.Put(this.OwningPlayer.Name);
		writer.Put(this.Rotation);
		writer.Put(this.Health);
	}


	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (this.CurrentIntention.Move != 0)
			this.Position += Vector2.FromAngle(this.Rotation) * (this.CurrentIntention.Move * (float)delta * Speed);

		NetDataWriter writer = new();
		writer.Put((byte)MessageId.ActorUpdated);
		ActorUpdated actorUpdatedMessage = new()
		{
			SyncId = this.SyncId,
			Position = this.Position,
			Rotation = this.Rotation,
		};
		actorUpdatedMessage.Serialize(writer);
		GameServer.Instance.BroadcastMessage(writer, DeliveryMethod.Unreliable);
	}
}