using LiteNetLib.Utils;

namespace Tanks;

public interface IServerToClientMessage
{
	public void Deserialize(NetDataReader reader);
}