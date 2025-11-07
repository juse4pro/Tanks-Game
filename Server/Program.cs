using Godot;

namespace Server;

public partial class Program : Node
{
	private GameServer _server;


	public override void _EnterTree()
	{
		this._server = new GameServer();
		this._server.Start();
	}


	public override void _Process(double delta)
	{
		this._server.Update((float)delta);
	}
}