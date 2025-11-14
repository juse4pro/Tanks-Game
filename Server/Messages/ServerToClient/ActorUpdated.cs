using LiteNetLib.Utils;
using Shared;
using Shared.Messages.FromServer;

namespace Server.Messages.ServerToClient;

public class ActorUpdated : ActorUpdatedMessage, IServerToClientMessage
{
	public void Serialize(NetDataWriter writer)
	{
		writer.Put(this.SyncId);
		writer.Put(this.Position);
		writer.Put(this.Rotation);
	}
}