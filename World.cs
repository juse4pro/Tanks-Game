using System;
using Godot;

namespace Tanks;

public partial class World : Node
{
	public static World Instance;

	private Node GetWorldNode()
	{
		Node world = this.GetTree().CurrentScene.FindChild("GameWorld");
		if (world == null)
			throw new Exception("BONK");
		return world;
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