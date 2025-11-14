using Godot;
using LiteNetLib.Utils;

namespace Shared;

public class SharedCharacter : SharedActor
{
	public const float Speed = 200f;
	public int Health;
	public Intention CurrentIntention;
}