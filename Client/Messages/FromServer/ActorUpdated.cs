using LiteNetLib.Utils;
using Shared;
using Shared.Messages.FromServer;

namespace Tanks.Messages.FromServer;

public class ActorUpdated : ActorUpdatedMessage, IServerToClientMessage
{
	public void Deserialize(NetDataReader reader)
	{
		this.SyncId = reader.GetGuid();
		this.Position = reader.GetVector();
		this.Rotation = reader.GetFloat();
	}
}