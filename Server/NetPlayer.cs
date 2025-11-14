using LiteNetLib;

namespace Server;

public class NetPlayer
{
	public required NetPeer Peer { get; init; }
	public string Name { get; } = $"Player{Random.Shared.Next(10000, 99999)}";

	public ServerCharacter? PossessedCharacter { private set; get; }


	public override string ToString()
	{
		return $"{this.Name} ({this.Peer.Address}:{this.Peer.Port})";
	}


	public void PossessCharacter(ServerCharacter character)
	{
		if (this.PossessedCharacter is not null)
			throw new Exception("Net already possesses a character");

		this.PossessedCharacter = character;
		character.OwningPlayer = this;
	}
}