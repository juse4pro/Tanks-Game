using Godot;
using LiteNetLib.Utils;
using Shared;

namespace Server;

public class ServerCharacter : SharedCharacter, IServerActor
{
	public NetPlayer OwningPlayer;


	public ServerCharacter()
	{
		this.GodotResourceUid = ResourceUid.TextToId("uid://dlltarjrl4x3j");
	}


	public void Serialize(NetDataWriter writer)
	{
		writer.Put(this.OwningPlayer.Name);
		writer.Put(this.Rotation);
		writer.Put(this.Health);
	}
}