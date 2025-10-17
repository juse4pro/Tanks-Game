using LiteNetLib;

namespace Server;

public class NetPlayer
{
	public required NetPeer Peer { get; init; }
	public string Name { get; } = $"Player{Random.Shared.Next(10000, 99999)}";


	public override string ToString()
	{
		return $"{this.Name} ({this.Peer.Address}:{this.Peer.Port})";
	}
}