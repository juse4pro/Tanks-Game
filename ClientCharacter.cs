using Godot;
using LiteNetLib.Utils;
using Shared;

namespace Tanks;

public partial class ClientCharacter : SharedCharacter, IClientActor
{
	public string CharacterName;


	public void Deserialize(NetDataReader reader)
	{
		this.CharacterName = reader.GetString();
		this.Rotation = reader.GetFloat();
		this.Health = reader.GetInt();
	}
}