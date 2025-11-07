using LiteNetLib.Utils;

namespace Server;

public interface IServerToClientMessage
{
	public void Serialize(NetDataWriter writer);
}