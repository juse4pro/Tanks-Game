using Godot;
using System;
using LiteNetLib;

public partial class Net : Node
{
	public static Net Instance;

	private NetManager _manager;


	public override void _EnterTree()
	{
		Instance = this;
	}


	public override void _PhysicsProcess(double delta)
	{
		this._manager?.PollEvents();
	}


	public void Join(string ip, int port)
	{
		GD.Print($"Trying to connect to {ip}:{port}...");
		EventBasedNetListener listener = new();
		listener.PeerConnectedEvent += this.OnPeerConnectedEvent;
		listener.PeerDisconnectedEvent += this.OnPeerDisconnectedEvent;
		listener.NetworkReceiveEvent += this.OnNetworkReceiveEvent;
		this._manager = new NetManager(listener);
		this._manager.Start();
		this._manager.Connect(ip, port, Shared.Settings.ConnectionKey);
	}


	private void OnPeerConnectedEvent(NetPeer peer)
	{
		GD.Print($"Connected to {peer.Address}:{peer.Port}");
	}


	private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
	{
		GD.Print($"Disconnected! ({disconnectInfo.Reason})");
	}


	private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte _, DeliveryMethod __)
	{
	}
}