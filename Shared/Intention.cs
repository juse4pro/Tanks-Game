using System.Diagnostics.CodeAnalysis;
using LiteNetLib.Utils;

namespace Shared;

public struct Intention : INetSerializable
{
	/// <summary>
	/// 0 means "I don't want to move"
	/// 1 means "I want to move forward"
	/// -1 means "I want to move backwards"
	/// </summary>
	public float Move;

	/// <summary>
	/// 0 means "I don't want to turn"
	/// 1 means "I want to turn clockwise"
	/// -1 means "I want to turn counter-clockwise"
	/// </summary>
	public float Turn;

	/// <summary>
	/// True if the player wants to shoot.
	/// </summary>
	public bool Shoot;


	public override string ToString()
	{
		return $"Move: {this.Move}, Turn: {this.Turn}, Shoot: {this.Shoot}";
	}


	public override bool Equals(object? obj)
	{
		if (obj is Intention otherIntention)
			return this == otherIntention;
		return false;
	}


	public override int GetHashCode()
	{
		return HashCode.Combine(this.Move, this.Turn, this.Shoot);
	}


	public static bool operator ==(Intention left, Intention right)
	{
		const float tolerance = 0.0001f;
		return Math.Abs(left.Move - right.Move) < tolerance
		       && Math.Abs(left.Turn - right.Turn) < tolerance
		       && left.Shoot == right.Shoot;
	}


	public static bool operator !=(Intention left, Intention right)
	{
		return !(left == right);
	}


	public void Serialize(NetDataWriter writer)
	{
		writer.Put(this.Move);
		writer.Put(this.Turn);
		writer.Put(this.Shoot);
	}


	public void Deserialize(NetDataReader reader)
	{
		this.Move = reader.GetFloat();
		this.Turn = reader.GetFloat();
		this.Shoot = reader.GetBool();
	}
}