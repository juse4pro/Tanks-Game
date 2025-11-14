using LiteNetLib.Utils;
using Shared.Messages.FromServer;

namespace Tanks.Messages.FromServer;

public class ActorDisappeared : ActorDisappearedMessage, IServerToClientMessage
{
	public void Deserialize(NetDataReader reader)
	{
		this.DisappearedSyncId = reader.GetGuid();
	}
}