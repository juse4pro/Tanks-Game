using LiteNetLib;
using LiteNetLib.Utils;
using Shared;
using Shared.Messages;
using Shared.Messages.FromServer;

namespace Server;

public class GameServer
{
	private const int MaxPlayers = 250;
	private NetManager _manager;
	private Dictionary<NetPeer, NetPlayer> _players = [];


	public void Start()
	{
		EventBasedNetListener listener = new();
		listener.ConnectionRequestEvent += this.OnConnectionRequestEvent;
		listener.PeerConnectedEvent += this.OnPeerConnectedEvent;
		listener.PeerDisconnectedEvent += this.OnPeerDisconnectedEvent;
		listener.NetworkReceiveEvent += this.OnNetworkReceiveEvent;
		this._manager = new NetManager(listener);
		this._manager.Start(Shared.Settings.GamePort);
		Console.WriteLine($"Server running on port {this._manager.LocalPort}");
	}


	public void Stop()
	{
		foreach (NetPlayer player in this._players.Values)
		{
			player.Peer.Disconnect();
		}

		this._players.Clear();
	}


	public void Update(float deltaTime)
	{
		this._manager.PollEvents();
	}


	private void OnConnectionRequestEvent(ConnectionRequest request)
	{
		Console.WriteLine($"Connection request from {request.RemoteEndPoint}");
		if (this._players.Count >= MaxPlayers)
			request.Reject();

		request.AcceptIfKey(Shared.Settings.ConnectionKey);
	}


	private void OnPeerConnectedEvent(NetPeer peer)
	{
		NetPlayer newPlayer = new NetPlayer
		{
			Peer = peer
		};
		this._players.Add(peer, newPlayer);

		this.BroadcastSystemChatMessage($"Player connected {newPlayer}");
	}


	private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
	{
		NetPlayer player = this._players[peer];
		this._players.Remove(peer);

		this.BroadcastSystemChatMessage($"Player disconnected {player} ({disconnectInfo.Reason})");
	}


	private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel,
		DeliveryMethod deliveryMethod)
	{
	}


	public void BroadcastSystemChatMessage(string message)
	{
		Console.WriteLine(message);
		NetDataWriter writer = new();
		writer.Put((byte)MessageId.Chat);
		writer.PutMessage(new ChatMessage
		{
			Sender = "SYSTEM",
			Message = message
		});

		foreach (NetPlayer player in this._players.Values)
		{
			player.Peer.Send(writer, DeliveryMethod.ReliableOrdered);
		}
	}
}