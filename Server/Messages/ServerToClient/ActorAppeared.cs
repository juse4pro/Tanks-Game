using LiteNetLib.Utils;
using Shared;
using Shared.Messages.FromServer;

namespace Server.Messages.ServerToClient;

public class ActorAppeared : ActorAppearedMessage, IServerToClientMessage
{
	public void Serialize(NetDataWriter writer)
	{
		writer.Put(this.Actor.GodotResourceUid);
		writer.Put(this.Actor.SyncId);

		if (this.Actor is not IServerActor serverActor)
			throw new Exception("Actor is not a server actor");

		writer.Put(this.Actor.Position);
		serverActor.Serialize(writer);
	}
}