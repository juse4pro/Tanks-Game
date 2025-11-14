using LiteNetLib.Utils;

namespace Tanks;

public interface IClientToServerMessage
{
	public void Serialize(NetDataWriter writer);
}