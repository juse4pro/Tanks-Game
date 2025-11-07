using Godot;
using System;
using LiteNetLib;
using Shared;
using Shared.Messages;
using Shared.Messages.FromServer;

public partial class Net : Node
{
	public static Net Instance;

	[Signal]
	public delegate void ConnectionStatusChangedEventHandler(bool wasSuccessful);

	[Signal]
	public delegate void ChatMessageEventHandler(string sender, string message);

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
		this.EmitSignalConnectionStatusChanged(true);
	}


	private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
	{
		GD.Print($"Disconnected! ({disconnectInfo.Reason})");
		this.EmitSignalConnectionStatusChanged(false);
	}


	private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte _, DeliveryMethod __)
	{
		MessageId messageId = (MessageId)reader.GetByte();
		switch (messageId)
		{
			case MessageId.Chat:

				ChatMessage message = reader.GetMessage<ChatMessage>();
				this.EmitSignalChatMessage(message.Sender, message.Message);
				break;
			default:
				GD.PushError($"Received unknown message ID {messageId}");
				break;
		}

		reader.Recycle();
	}
}