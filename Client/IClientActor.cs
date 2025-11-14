using LiteNetLib.Utils;

namespace Tanks;

public interface IClientActor
{
	public void Deserialize(NetDataReader reader);
}