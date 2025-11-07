using Godot;
using LiteNetLib.Utils;

namespace Shared;

public static class SerializationHelper
{
	public static void Put(this NetDataWriter writer, Vector2 vector)
	{
		writer.Put(vector.X);
		writer.Put(vector.Y);
	}


	public static Vector2 GetVector(this NetDataReader reader)
	{
		return new Vector2(reader.GetFloat(), reader.GetFloat());
	}
}