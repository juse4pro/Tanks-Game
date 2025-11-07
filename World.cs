using Godot;

namespace Tanks;

public partial class World : Node
{
	public static World Instance;

	private Node GetWorldNode()
	{
		return this.GetTree().Root.GetNode("/root/Ingame/GameLayer/World");
	}


	public override void _EnterTree()
	{
		Instance = this;
	}


	public void AddToWorld(Node2D node)
	{
		this.GetWorldNode().AddChild(node);
	}
}