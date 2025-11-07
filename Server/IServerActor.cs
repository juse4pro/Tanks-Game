using LiteNetLib.Utils;

namespace Server;

public interface IServerActor
{
	public void Serialize(NetDataWriter writer);
}