using Godot;
using LiteNetLib.Utils;

namespace Shared;

public class SharedProjectile : SharedActor
{
	public Vector2 Velocity;


	public void Serialize(NetDataWriter writer)
	{
		writer.Put(this.Velocity);
	}


	public void Deserialize(NetDataReader reader)
	{
		this.Velocity = reader.GetVector();
	}
}