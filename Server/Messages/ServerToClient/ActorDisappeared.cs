using LiteNetLib.Utils;
using Shared.Messages.FromServer;

namespace Server.Messages.ServerToClient;

public class ActorDisappeared : ActorDisappearedMessage, IServerToClientMessage
{
	public void Serialize(NetDataWriter writer)
	{
		writer.Put(this.DisappearedSyncId);
	}
}